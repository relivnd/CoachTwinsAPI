using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CoachTwins.Models.Users;
using CoachTwinsAPI.Auth;
using CoachTwinsApi.Db.Entities;
using CoachTwinsApi.Db.Repository;
using CoachTwinsApi.Db.Repository.Contract;
using CoachTwinsClassLibrary.Models.Dashboard;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Student = CoachTwinsApi.Db.Entities.Student;
using User = CoachTwinsApi.Db.Entities.User;
using CoachTwinsApi;

namespace CoachTwinsAPI.Controllers.Dashboard
{
    [Route("dashboard")]
    [ApiController]
    public class DashboardController : BaseController
    {
        public DashboardController(IStudentRepository studentRepository, ICoachRepository coachRepository, IUserRepository userRepository, IMapper mapper, IAuthRepository authRepository, AuthStore authStore, IPortalUserRepository portalRepo, IMatchingRepository matchingRepo) : base(studentRepository, coachRepository, userRepository, mapper, authRepository, authStore, portalRepo, matchingRepo)
        {
        }

        [HttpGet]
        [LoginRequired(LoginType.Both)]
        public async Task<IActionResult> Actions()
        {
            var user = await GetCurrentUser<User>();

            switch (user)
            { 
                case Coach c:
                {
                    var matches = c.Matches.Where(m => m.isAMatchForCoach == null).ToList();
                    var unseenAppointmentCount = matches.SelectMany(m => m.Appointments)
                        .Count(a => !a.LastChangeSeenByReceiver && a.Creator.Id != user.Id);
                    var unseenMessageCount = matches.ToDictionary(m => m.StudentId, m => m.Chat?.UnseenMessagesCoach ?? 0);

                    if (matches.Any(match => match.Appointments.OrderBy(m => m.Start).FirstOrDefault(a => !a.Canceled && a.Accepted == true)?.End < DateTime.UtcNow))
                    {
                        return Ok(new DashboardResponse()
                        {
                            Action = "validate_match",
                            AppointmentNotifications = unseenAppointmentCount,
                            UnseenMessagesCount = unseenMessageCount
                        });
                    }

                    return Ok(new DashboardResponse()
                    {
                        AppointmentNotifications = unseenAppointmentCount,
                        UnseenMessagesCount = unseenMessageCount
                    });
                }
                case Student s:
                {
                    var match = s.Match;

                    if (match is not { isAMatchForCoachee: null })
                        return Ok(new DashboardResponse());
                    
                    var unseenAppointmentCount = match.Appointments.Count(a => !a.LastChangeSeenByReceiver && a.Creator.Id != user.Id);
                    var unseenMessageCount = new Dictionary<Guid, int> { [match.CoachId] = match.Chat?.UnseenMessagesCoachee ?? 0 };

                    var firstAppointment = match.Appointments.OrderBy(m => m.Start).FirstOrDefault(a => !a.Canceled && a.Accepted == true);

                    if (firstAppointment != null && firstAppointment.End < DateTime.UtcNow)
                        return Ok(new DashboardResponse
                        {
                            Action = "validate_match",
                            AppointmentNotifications = unseenAppointmentCount,
                            UnseenMessagesCount = unseenMessageCount
                        });

                    return Ok(new DashboardResponse()
                    {
                        AppointmentNotifications = unseenAppointmentCount,
                        UnseenMessagesCount = unseenMessageCount
                    });
                }
                default:
                    return Forbid();
            }
        }

        [HttpGet("coachees")]
        [LoginRequired(LoginType.Both)]
        public async Task<IActionResult> GetCoachees()
        {
            var coach = await GetCurrentUser<Coach>();

            if (coach == null)
                return Forbid();
            
            if (coach?.Matches == null)
                return NotFound();

            var coachees = coach.Matches
                .Select(x => Mapper.Map<Coachee>(x.Student))
                .ToList();

            return Ok(coachees);
        }

        [HttpGet("coach")]
        public async Task<IActionResult> GetCoach()
        {
            var student = await GetCurrentUser<Student>();

            if (student == null)
                return Forbid();
            
            if (student.Match?.Coach == null)
                return NotFound();

            return Ok(Mapper.Map<StudentCoach>(student.Match?.Coach));
        }
    }
}