using CoachTwinsApi.Db.Entities;
using CoachTwinsApi.Logic.Mail;
using Microsoft.AspNetCore.Mvc;

namespace CoachTwinsAPI.Controllers
{
    public class TestController: ControllerBase
    {
        [HttpGet("test")]
        public ActionResult Get([FromServices] IMailer mailer)
        {
            mailer.SendMail(new User() {Email = "local@localhost"},"test", "notify", "test");
            return Ok();
        }
    }
}