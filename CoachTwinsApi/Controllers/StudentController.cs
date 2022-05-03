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
using CoachTwinsApi.Db.ApiModels.login;

namespace CoachTwinsApi.Controllers
{
    [System.Web.Mvc.Route("Student")]
    public partial class StudentController : BaseController
    {
      [LoginRequired]
      [HttpPost("BecomeCoach")]
      public async Task<ActionResult<GenericResponse>> SendCoachRequest()
        {
            var student = await GetCurrentStudent();
            await coachRequestRepo.PerformCoachRequest(student.Id);
            return new GenericResponse()
            {
                message = "Request has been filed. Please wait for approval by an admin"
            };
        }
    }
}