using CoachTwinsApi.ApiModels;
using CoachTwinsApi.Db.Repository.Contract;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Web.Mvc;
using CoachTwinsApi.Db.Extensions;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using CoachTwinsApi.Db.ApiModels;
using CoachTwinsApi.Db.Entities;
using System;

namespace CoachTwinsApi.Controllers
{
    [System.Web.Mvc.Route("Student/auth")]
    public partial class StudentController : BaseController
    {
        public StudentController(IStudentRepository userRepository, IAuthRepository authRepo, IAdminRepository adminRepo, ICoachRequestRepo coachRequestRepo) : base(userRepository, authRepo, adminRepo, coachRequestRepo)
        {
        }


        /// <summary>
        /// Adds a new student (coachee) to the database. For developemnt purposes only
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public async Task<ActionResult<StudentPostResponse>> RegisterStudent(StudentPostRequest request)
        {
            if (request.email is null)
            {
                return BadRequest("No email provided");
            }
            if (request.password is null || request.password.Length < 7)
            {
                return BadRequest("Password must be at least 7 characters");
            }
            var email = request.email;
            if ((await studentRepo.TryFind(s => s.Email.ToLower() == email.ToLower())).succes)
            {
                return BadRequest("Student already registered");
            }
            await studentRepo.Create(new Student()
            {
                Email = email,
                Password = request.password.Sha256()
            });
            return new StudentPostResponse()
            {
                message = "Account created"
            };
        }
        /// <summary>
        /// Logs in as student (coach or coachee) and provides a brand new authtoken for the app to use
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> LoginStudent(LoginRequest request)
        {
            if (request.email is null || request.password is null)
            {
                return BadRequest("Email or password not provided");
            }
            var user = (await studentRepo.TryFind(s => s.Email == request.email && s.Password == request.password.Sha256())).result;
            if (user is null)
            {
                return Unauthorized("Incorrect email and password");
            }
            var token = new AuthToken()
            {
                Active = true,
                ActiveGuid = user.Id,
                Value = Extensions.StringExtensions.RandomString(30),
                ValidThru = DateTime.Now.AddDays(7),
                UserType= UserType.Student
            };
            var loginResponse = new LoginResponse()
            {
                authToken = token.Value,
                userId = user.Id,
            };
            await authRepo.Create(token);
            return loginResponse;
        }

    }
}