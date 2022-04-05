using System;
using CoachTwins.Models.Users;

namespace CoachTwins.Models.Appointments
{
    /// <summary>
    /// Model representing an appointment between a student coach and coachee.
    /// </summary>
    public class Appointment
    {
        public Guid Id { get; set; }
        
        /// <summary>
        /// The starting time of the appointment.
        /// </summary>
        public DateTime Start { get; set; }

        /// <summary>
        /// The ending time of the appointment.
        /// </summary>
        public DateTime End { get; set; }

        /// <summary>
        /// The location of the appointment
        /// </summary>
        public string Location { get; set; }
        
        public bool? Accepted { get; set; }
        
        public bool Canceled { get; set; }
        
        public User Creator { get; set; }

        public bool IsOwner { get; set; }

    }
}
