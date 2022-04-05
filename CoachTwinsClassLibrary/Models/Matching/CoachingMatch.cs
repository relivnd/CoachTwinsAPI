using System;
using CoachTwins.Models.Appointments;
using CoachTwins.Models.Chatting;
using CoachTwins.Models.Users;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CoachTwins.Models.Matching
{
    /// <summary>
    /// Model representing the match between a student and coach.
    /// </summary>
    public class CoachingMatch
    {
        public Guid Id { get; set; }

        /// <summary>
        /// The student matched with the student coach.
        /// </summary>
        public Student Student { get; set; }

        /// <summary>
        /// The student coach matched with the student.
        /// </summary>
        public StudentCoach StudentCoach { get; set; }

        /// <summary>
        /// The appointments between the student and student coach.
        /// </summary>
        public ObservableCollection<Appointment> Appointments { get; set; } = new ObservableCollection<Appointment>();
    }
}
