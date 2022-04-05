using System.Net.Http;
using System.Threading.Tasks;
using CoachTwinsApi.Db.Entities;
using Microsoft.Graph;
using User = CoachTwinsApi.Db.Entities.User;

namespace CoachTwinsApi.Graph
{
    public class AgendaManager
    {
        private readonly GraphServiceClient _client;
        
        public AgendaManager(GraphServiceClient client)
        {
            // needs to be filled with credentials still
            _client = client;
        }

        public void AcceptItem(Appointment appointment)
        {
            _client.Me.Events[appointment.Id.ToString()].Accept().Request().PostAsync();
        }

        public async Task CreateItem(Appointment appointment)
        {
            var subject = (User)(appointment.Creator is Coach ? appointment.Match.Student : appointment.Match.Coach);
            await _client.Me.Calendar.Events.Request().AddAsync(new Event
            {
                Subject = $"Coaching session",
                Start = DateTimeTimeZone.FromDateTime(appointment.Start),
                End = DateTimeTimeZone.FromDateTime(appointment.End),
                Location = new Location
                {
                    DisplayName = appointment.Location
                },
                Attendees = new[]
                {
                    new Attendee
                    {
                        EmailAddress = new EmailAddress
                        {
                            Name = $"{subject.FirstName} {subject.LastName}",
                            Address = subject.Email
                        },
                        Type = AttendeeType.Required
                    }
                }
            });
        }
    }
}