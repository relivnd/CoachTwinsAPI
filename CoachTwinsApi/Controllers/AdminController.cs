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
using System.Collections.Generic;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;

namespace CoachTwinsApi.Controllers
{
    [System.Web.Mvc.Route("Admin/auth")]
    public class AdminController : BaseController
    {
        public AdminController(IStudentRepository userRepository, IAuthRepository authRepo, IAdminRepository adminRepo, ICoachRequestRepo coachRequestRepo) : base(userRepository, authRepo, adminRepo, coachRequestRepo)
        {
        }


        /// <summary>
        /// Logs in in the admin portal and provides a brand new auth token
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("admin/login")]
        public async Task<ActionResult<LoginResponse>> LoginAdministrator(LoginRequest request)
        {
            if (request.email is null || request.password is null)
            {
                return BadRequest("Email or password not provided");
            }
            var user = (await adminRepo.TryFind(s => s.Email.ToLower() == request.email.ToLower())).result;

            if (user is null)
            {
                return Unauthorized("Invalid email and password");
            }
            var token = new AuthToken()
            {
                Active = true,
                ActiveGuid = user.Id,
                Value = Extensions.StringExtensions.RandomString(30),
                ValidThru = DateTime.Now.AddDays(7),
                UserType = UserType.Administrator
            };
            var loginResponse = new LoginResponse()
            {
                authToken = token.Value,
                userId = user.Id,
            };
            await authRepo.Create(token);
            return loginResponse;
        }

        /// <summary>
        /// Gets all coach requests. Pending, accepted and rejected
        /// </summary>
        /// <returns></returns>
        [HttpGet("admin/coachRequests")]
        public async Task<ActionResult<IEnumerable<CoachRequest>>> GetCoachRequests()
        {
            return Ok(await coachRequestRepo.GetCoachRequests());
        }


        [HttpPost("admin/approveCoachRequest")]
        public async Task<ActionResult<GenericResponse>> ApproveCoachRequest(Guid id)
        {

            try
            {
                var success = await coachRequestRepo.Approve(id);
                return Ok("Request has been approved, student is now a coach");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("admin/denyCoachRequest")]
        public async Task<ActionResult<GenericResponse>> DenyCoachRequest(Guid id)
        {

            try
            {
                var success = await coachRequestRepo.Reject(id);
                return Ok("Request has been rejected, student remains a coachee");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}