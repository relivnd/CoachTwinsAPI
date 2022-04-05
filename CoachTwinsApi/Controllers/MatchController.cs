using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CoachTwins.Models.Matching;
using CoachTwins.Models.Matching.Requests;
using CoachTwins.Models.Users;
using CoachTwinsAPI.Auth;
using CoachTwinsApi.Db.Entities;
using CoachTwinsApi.Db.Repository;
using CoachTwinsApi.Db.Repository.Contract;
using CoachTwinsApi.Logic.Mail;
using CoachTwinsAPI.Logic.Matching;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Student = CoachTwinsApi.Db.Entities.Student;
using User = CoachTwinsApi.Db.Entities.User;
using CoachTwinsApi;

namespace CoachTwinsAPI.Controllers
{
    [LoginRequired]
    [ApiController]
    [Route("match")]
    public class MatchController: BaseController
    {
        public MatchController(IStudentRepository studentRepository, ICoachRepository coachRepository, IUserRepository userRepository, IMapper mapper, IAuthRepository authRepository, AuthStore authStore, IPortalUserRepository portalRepo, IMatchingRepository matchingRepo) : base(studentRepository, coachRepository, userRepository, mapper, authRepository, authStore, portalRepo, matchingRepo)
        {
        }


        /// <summary>
        /// Get 3 possible matches
        /// </summary>
        /// <param name="matchingService"></param>
        /// <param name="mapper"></param>
        /// <param name="studentRepository"></param>
        /// <param name="criteriaRepository"></param>
        /// <returns>The matches</returns>
        [HttpGet("potential")]
        public async Task<IEnumerable<StudentCoach>> GetPotentialMatches([FromServices]MatchingService matchingService)
        {
            var user = await GetCurrentUser<Student>();
            var coaches = await matchingService.GetCoachesWithMatchingCharacteristics(user);

            return coaches.Select(u => Mapper.Map<StudentCoach>(u));
        }
        
        [HttpGet("validate")]
        public async Task<ActionResult> GetValidateMatch()
        {
            var user = await GetCurrentUser<User>();

            return user switch
            {
                Student s => Ok(Mapper.Map<CoachingMatch>(s.Match)),
                Coach c => Ok(Mapper.Map<CoachingMatch>(c.Matches.FirstOrDefault(m => m.Appointments.Any(a => a.End < DateTime.UtcNow && a.Canceled == false && a.Accepted == true) && m.isAMatch == null))),
                _ => Forbid()
            };
        }
        
        [HttpPost("validate")]
        public async Task<ActionResult> ValidateMatch([FromBody]ValidateMatchRequest request, [FromServices]MatchingService matchingService, [FromServices]IMailer mailer)
        {
            var user = await GetCurrentUser<User>();
            var match = matchingService.GetMatchFromUser(user, request.MatchId);

            if (match == null)
                return NotFound();

            if (!request.IsAMatch)
            {
                try
                {
                    if (user is Student)
                        await mailer.SendMail(user, "", "notify",
                            $"{match.Student.FirstName} {match.Student.LastName} unmatched from {match.Coach.FirstName} {match.Coach.LastName} after their first appointment");
                    if (user is Coach)
                        await mailer.SendMail(user, "", "notify",
                            $"{match.Coach.FirstName} {match.Coach.LastName} unmatched from {match.Student.FirstName} {match.Student.LastName} after their first appointment");
                }
                catch (Exception e)
                {
                    // mail failed, continue anyway. Implement mail queue later
                }

                await matchingRepository.Delete(match);
            }
            else
            {
                switch (user)
                {
                    case Student:
                        match.isAMatchForCoachee = true;
                        break;
                    case Coach:
                        match.isAMatchForCoach = true;
                        break;
                }
                
                await matchingRepository.Update(match);
                
                if (match.isAMatchForCoach == true && match.isAMatchForCoachee == true)
                    await mailer.SendMail(user, "", "notify", 
                        $"The match between {match.Coach.FirstName} {match.Coach.LastName} and {match.Student.FirstName} {match.Student.LastName} is confirmed after their first appointment!");
            }

            return Ok();
        }
        
        /// <summary>
        /// Match student and coach
        /// </summary>
        /// <param name="coachModel"></param>
        /// <param name="coachRepository"></param>
        /// <param name="studentRepository"></param>
        /// <param name="matchingRepository"></param>
        [HttpPost]
        public async Task<ActionResult> Match(StudentCoach coachModel)
        {
            var user = await GetCurrentUser<Student>();

            if (user == null)
                return Forbid();

            if (user.Match != null)
                return BadRequest();
            
            var coach = await CoachRepository.Get(coachModel.Id);

            var match = new Match()
            {
                Coach = coach,
                Student = user,
            };

            await matchingRepository.Create(match);

            coach.AvailableForMatching = false;
            await CoachRepository.Update(coach);

            return Ok();
        }

        [HttpGet("")]
        public async Task<ActionResult> GetMatches()
        {
            var user = await GetCurrentUser<User>();

            return user switch
            {
                Coach c => Ok(c.Matches.Select(Mapper.Map<CoachingMatch>)),
                Student s => Ok(new List<CoachingMatch>() { Mapper.Map<CoachingMatch>(s.Match) }),
                _ => Forbid()
            };
        }
    }
}