using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CoachTwins.Models.Users;
using CoachTwinsAPI.Auth;
using CoachTwinsApi.Db.Repository;
using CoachTwinsApi.Db.Repository.Contract;
using CoachTwinsAPI.Logic.Matching;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web;
using Student = CoachTwinsApi.Db.Entities.Student;
using CoachTwinsApi;

namespace CoachTwinsAPI.Controllers
{
    [LoginRequired]
    [ApiController]
    [Route("student")]
    public class StudentController: BaseController
    {
        public StudentController(IStudentRepository studentRepository, ICoachRepository coachRepository, IUserRepository userRepository, IMapper mapper, IAuthRepository authRepository, AuthStore authStore, IPortalUserRepository portalRepo, IMatchingRepository matchingRepo) : base(studentRepository, coachRepository, userRepository, mapper, authRepository, authStore, portalRepo, matchingRepo)
        {
        }

        [HttpGet("matched")]
        public async Task<ActionResult<bool>> IsMatched()
        {
            var user = await GetCurrentUser<Student>();

            if (user == null)
                return Forbid();
            
            return Ok(new
            {
                Matched = user.Match != null
            });
        }

     
    }
}