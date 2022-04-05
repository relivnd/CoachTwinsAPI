using CoachTwins.Models.Matching;
using System.Collections.Generic;

namespace CoachTwins.Models.Users
{
    /// <summary>
    /// Model representing a student coach.
    /// </summary>
    public class StudentCoach : User
    {

        /// <summary>
        /// A check to determine whether the student coach is accepting new students for coaching.
        /// </summary>
        public bool AvailableForMatching { get; set; }
        
        /// <summary>
        /// A checker to determine whether the student coach's matching profile has been set up.
        /// </summary>
        public bool IsMatchingProfileSetup { get; set; }
        
        /// <summary>
        /// All the student coaching matches the student coach has with students.
        /// </summary>
        public IList<CoachingMatch> CoachingMatches { get; set; }
    }
}
