using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CoachTwinsAPI.Auth;
using CoachTwinsApi.Db.Entities;
using CoachTwinsApi.Db.Repository;
using CoachTwinsApi.Db.Repository.Contract;
using CoachTwinsApi.Graph;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web;
using ApiUser = CoachTwins.Models.Users.User;
using ApiStudent = CoachTwins.Models.Users.Student;
using ApiCoach = CoachTwins.Models.Users.StudentCoach;
using Student = CoachTwinsApi.Db.Entities.Student;
using RegisterResponse = CoachTwins.Models.Users.RegisterResponse;
using User = CoachTwinsApi.Db.Entities.User;
using CoachTwinsApi;
using CoachTwinsAPI.Extensions;

namespace CoachTwinsAPI.Controllers
{
    [LoginRequired]
    [Route("user")]
    [ApiController]
    public class LoginController : BaseController
    {
        public LoginController(IStudentRepository studentRepository, ICoachRepository coachRepository, IUserRepository userRepository, IMapper mapper, IAuthRepository authRepository, AuthStore authStore, IPortalUserRepository portalRepo, IMatchingRepository matchingRepo) : base(studentRepository, coachRepository, userRepository, mapper, authRepository, authStore, portalRepo, matchingRepo)
        {
        }




        /// <summary>
        /// Register the user after login
        /// </summary>
        /// <returns>The user</returns>
        [HttpGet("register")]
        public async Task<ActionResult> Register([FromServices] AgendaManager agendaManager)
        {
            var user = HttpContext.User;
            var id = user.GetObjectId();

            if (id == null)
                return BadRequest();

            var firstname = user.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname")?.Value;
            var lastname = user.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname")?.Value;
            var email = user.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value;

            var currentUser = await UserRepository.Get(Guid.Parse(id));

            if (currentUser != null)
            {
                currentUser.Email = email;
                currentUser.FirstName = firstname;
                currentUser.LastName = lastname;

                await UserRepository.Update(currentUser);

                return Ok(new RegisterResponse
                {
                    Coach = currentUser is Coach ? Mapper.Map<ApiCoach>(currentUser) : null,
                    Student = currentUser is Student ? Mapper.Map<ApiStudent>(currentUser) : null
                });
            }

            var student = new Student()
            {
                Id = Guid.Parse(id),
                Email = email,
                FirstName = firstname,
                LastName = lastname
            };

            await StudentRepository.Create(student);

            return Ok(new RegisterResponse()
            {
                Student = Mapper.Map<ApiStudent>(student)
            });
        }

        /// <summary>
        /// Get user role
        /// </summary>
        /// <returns>Object with the user role</returns>
        [HttpGet("role")]
        public async Task<ActionResult> GetRoleByUserId()
        {
            var user = await GetCurrentUser<User>();

            return user switch
            {
                null => Forbid(),
                Coach => Ok(new { Role = "Coach" }),
                _ => Ok(new { Role = "Student" })
            };
        }

        /// <summary>
        /// REST API action for accepting the privacy policy.
        /// </summary>
        /// <returns></returns>
        [HttpGet("privacy/agree")]
        public async Task<ActionResult<bool>> UserAcceptPrivacyPolicy()
        {
            var user = await GetCurrentUser<User>();
            if (user == null)
                return BadRequest();

            user.IsPrivacyPolicyAccepted = true;

            await UserRepository.Update(user);

            return Ok(true);
        }

        /// <summary>
        /// REST API action for rejecting the privacy policy.
        /// </summary>
        /// <returns></returns>
        [HttpPost("privacy/deny")]
        public async Task<ActionResult<bool>> UserDenyPrivacyPolicy()
        {
            var user = await GetCurrentUser<User>();

            // user doesn't exist, nothing to clean up
            if (user == null)
                return Ok();

            if (user is Student s)
            {
                await StudentRepository.Delete(s);

            }
            else if (user is Coach c)
            {
                await CoachRepository.Delete(c);
            }

            return Ok();
        }

        /// <summary>
        /// REST api action for getting the coach by their token.
        /// </summary>
        /// <returns>The user by its token</returns>
        [HttpGet("login/coach")]
        public async Task<ActionResult<ApiCoach>> GetStudentCoachByAuthToken()
        {
            var dbUser = await GetCurrentUser<User>();

            if (dbUser is Coach dbCoach)
            {
                var mapped = Mapper.Map<ApiCoach>(dbCoach);
                return mapped;
            }
            return Forbid();

        }

        /// <summary>
        /// REST api action for getting the student by their token.
        /// </summary>
        /// <returns>The user by its token</returns>
        [HttpGet("login/student")]
        public async Task<ActionResult<ApiStudent>> GetStudentByAuthToken()
        {
            var dbUser = await GetCurrentUser<User>();

            if (dbUser is Student dbStudent)
            {
                    var mapped = Mapper.Map<ApiStudent>(dbStudent);
                    return mapped;
            }

            return Forbid();
        }

        /// <summary>
        /// REST api action for getting a coach's profile by their guid
        /// </summary>
        /// <returns>The coach by its id</returns>
        [HttpGet("details/coach")]
        [LoginRequired]
        public async Task<ActionResult<ApiCoach>> GetCoachDetailsById(Guid id)
        {
            var dbUser = await CoachRepository.Get(id);

            if (dbUser is Coach dbCoach)
            {
                var mapped = Mapper.Map<ApiCoach>(dbCoach);
                return mapped;
            }
            return NotFound();
        }

        /// <summary>
        /// REST api action for getting a students's profile by their guid
        /// </summary>
        /// <returns>The coach by its id</returns>
        [HttpGet("details/student")]
        [LoginRequired]
        public async Task<ActionResult<ApiStudent>> GetStudentDetailsById(Guid id)
        {
            var dbUser = await StudentRepository.Get(id);

            if (dbUser is Student dbStudent)
            {
                var mapped = Mapper.Map<ApiStudent>(dbStudent);
                return mapped;
            }
            return NotFound();
        }

        /// <summary>
        /// Gets the user profile picture as byte[] string from the Picture GUID provided in the profile
        /// </summary>
        /// <param name="id"></param>
        /// <returns>string or 204 no content</returns>
        [HttpGet("profilePicture")]
        [LoginRequired(LoginType.Both)]
        
        public async Task<byte[]?> GetProfilePicture(Guid id)
        {
            var pic = await UserRepository.GetProfilePicture(id);
            return  pic;
        }

        /// <summary>
        /// Gets the user profile picture as byte[] string from the user's GUID provided in the profile
        /// </summary>
        /// <param name="id"></param>
        /// <returns>string or 204 no content</returns>
        [HttpGet("profilePictureByUserId")]
        [LoginRequired(LoginType.Both)]

        public async Task<byte[]?> GetProfilePictureByUserId(Guid id)
        {
            var pic = await UserRepository.GetProfilePictureByUserId(id);
            return pic;
        }
    }
}
