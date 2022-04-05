using System;
using System.Threading.Tasks;
using AutoMapper;
using CoachTwins.Models.Matching.Requests;
using CoachTwinsAPI.Auth;
using CoachTwinsApi.Db.Entities;
using CoachTwinsApi.Db.Repository;
using CoachTwinsApi.Db.Repository.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web;
using CoachTwinsApi;

namespace CoachTwinsAPI.Controllers.Profile.Setup
{
    [LoginRequired]
    [ApiController]
    [Route("profile/setup/coaching")]
    public class CoachSetupController : BaseController
    {
        public CoachSetupController(IStudentRepository studentRepository, ICoachRepository coachRepository, IUserRepository userRepository, IMapper mapper, IAuthRepository authRepository, AuthStore authStore, IPortalUserRepository portalRepo, IMatchingRepository matchingRepo) : base(studentRepository, coachRepository, userRepository, mapper, authRepository, authStore, portalRepo, matchingRepo)
        {
        }

        [HttpGet("status")]
        public async Task<IActionResult> IsCoachingProfileSetup()
        {
            var coach = await GetCurrentUser<Coach>();
            if (coach == null)
                return NotFound();
            
            var statusResult = new {Status = coach.IsMatchingProfileSetup};

            return Ok(statusResult);
        }

        [HttpPost]
        public async Task SetupCoachingProfile(CoachMatchingProfileSetupRequest request, [FromServices] ICriteriaRepository criteriaRepository)
        {
            if (request == null)
                return;
            
            var coach = await GetCurrentUser<Coach>();
            if (coach == null || coach.IsMatchingProfileSetup)
                return;

            foreach (var criterion in request.MatchingCriteria)
            {
                var criteria = await criteriaRepository.Get(criterion.Key);

                if (criteria == null)
                    continue;

                coach.MatchingCriteria.Add(new MatchingCriteria()
                {
                    Criteria = criteria,
                    Prefer = criterion.Prefer,
                    Value = criterion.Value
                });
            }

            coach.IsMatchingProfileSetup = true;

            await CoachRepository.Update(coach);
        }

      
    }
}