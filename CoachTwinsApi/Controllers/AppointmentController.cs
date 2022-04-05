using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CoachTwins.Models.Appointments;
using CoachTwinsAPI.Auth;
using CoachTwinsApi.Db.Entities;
using CoachTwinsApi.Db.Repository;
using CoachTwinsApi.Db.Repository.Contract;
using CoachTwinsApi.Graph;
using CoachTwinsAPI.Logic.Matching;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Appointment = CoachTwins.Models.Appointments.Appointment;
using CoachTwinsApi;

namespace CoachTwinsAPI.Controllers
{
    [LoginRequired]
    [ApiController]
    [Route("appointment")]
    public class AppointmentController: BaseController
    {
        public AppointmentController(IStudentRepository studentRepository, ICoachRepository coachRepository, IUserRepository userRepository, IMapper mapper, IAuthRepository authRepository, AuthStore authStore, IPortalUserRepository portalRepo, IMatchingRepository matchingRepo) : base(studentRepository, coachRepository, userRepository, mapper, authRepository, authStore, portalRepo, matchingRepo)
        {
        }

        [HttpGet("{matchId:guid}")]
        public async Task<ActionResult> GetAppointments([FromServices] MatchingService matchingService, [FromServices] IMatchingRepository matchingRepository, Guid matchId)
        {
            var user = await GetCurrentUser<User>();

            if (user == null)
                return Forbid();

            var match = matchingService.GetMatchFromUser(user, matchId);

            if (match == null)
                return Forbid();

            var appointments = match.Appointments;

            foreach (var appointment in appointments.Where(a => !a.LastChangeSeenByReceiver))
            {
                appointment.LastChangeSeenByReceiver = true;
            }

            await matchingRepository.Update(match);

            return Ok(appointments.Where(a => a.End >= DateTime.UtcNow).Select(a => Mapper.Map<Appointment>(a)));
        }

        [HttpPost("{appointmentId:guid}/reply")]
        public async Task<ActionResult> Reply([FromServices] IAppointmentRepository appointmentRepository, [FromBody] AppointmentReplyRequest appointmentReplyRequest, Guid appointmentId)
        {
            var user = await GetCurrentUser<User>();

            if (user == null)
                return Forbid();

            var appointment = await appointmentRepository.Get(appointmentId);

            if (appointment == null)
                return NotFound();

            if (appointment.Creator.Id == user.Id || (appointment.Match.Student.Id != user.Id && appointment.Match.Coach.Id != user.Id))
                return BadRequest();

            appointment.Accepted = appointmentReplyRequest.Accept;
            appointment.LastChangeSeenByReceiver = false;
            await appointmentRepository.Update(appointment);

            return Ok();
        }
        
        [HttpPost("{appointmentId:guid}/cancel")]
        public async Task<ActionResult> Cancel([FromServices] IAppointmentRepository appointmentRepository, Guid appointmentId)
        {
            var user = await GetCurrentUser<User>();

            if (user == null)
                return Forbid();

            var appointment = await appointmentRepository.Get(appointmentId);

            if (appointment == null)
                return NotFound();

            if (appointment.Match.Student.Id != user.Id && appointment.Match.Coach.Id != user.Id)
                return BadRequest();

            appointment.Canceled = true;
            appointment.LastChangeSeenByReceiver = false;
            await appointmentRepository.Update(appointment);

            return Ok();
        }

        [HttpPost("create/{matchId:guid}")]
        public async Task<ActionResult> CreateAppointment([FromServices] IMatchingRepository matchingRepository, [FromServices] IAppointmentRepository appointmentRepository, [FromServices] AgendaManager agendaManager, Appointment appointment, Guid matchId)
        {
            var user = await GetCurrentUser<User>();

            if (user == null)
                return Forbid();
            
            var match = await matchingRepository.Get(matchId);

            if ((user is Coach && match.Coach?.Id != user.Id) || (user is Student && match.Student?.Id != user.Id))
                return BadRequest();
            
            if (appointment.Start > appointment.End || (appointment.End - appointment.Start).Days > 1)
                return BadRequest();
            
            if (appointment.Start < DateTime.UtcNow)
                return BadRequest();

            var appointmentEntity = new CoachTwinsApi.Db.Entities.Appointment()
            {
                Start = appointment.Start,
                End = appointment.End,
                Location = appointment.Location,
                Creator = user,
                Accepted = null,
                LastChangeSeenByReceiver = false
            };
            
            if (appointmentRepository.GetAllOverlapping(appointmentEntity).Count > 0)
                return BadRequest(new
                {
                    reason = "overlap"
                });
            
            match.Appointments.Add(appointmentEntity);
            await matchingRepository.Update(match);
            
            return Ok();
        }
    }
}