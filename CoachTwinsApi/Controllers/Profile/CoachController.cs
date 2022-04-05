using System;
using System.Threading.Tasks;
using AutoMapper;
using CoachTwinsAPI.Auth;
using CoachTwinsApi.Db.Entities;
using CoachTwinsApi.Db.Repository;
using CoachTwinsApi.Db.Repository.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web;
using CoachTwinsApi;

namespace CoachTwinsAPI.Controllers.Profile
{
    [LoginRequired]
    [Route("coach")]
    [ApiController]
    public class CoachController : BaseController
    {
        public CoachController(IStudentRepository studentRepository, ICoachRepository coachRepository, IUserRepository userRepository, IMapper mapper, IAuthRepository authRepository, AuthStore authStore, IPortalUserRepository portalRepo, IMatchingRepository matchingRepo) : base(studentRepository, coachRepository, userRepository, mapper, authRepository, authStore, portalRepo, matchingRepo)
        {
        }

        [HttpGet("matching/reenter")]
        public async Task<IActionResult> ReEnterMatchingProcedure()
        {
            var coach = await GetCurrentUser<Coach>();
            if (coach == null)
                return BadRequest();

            if (coach.AvailableForMatching)
                return Ok();

            coach.AvailableForMatching = true;

            await CoachRepository.Update(coach);
            
            return Ok(true);
        }

        
    }
}