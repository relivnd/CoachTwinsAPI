using CoachTwins.Models.Matching;
using System.Collections.Generic;

namespace CoachTwins.Models.Users
{
    /// <summary>
    /// Model representing a student.
    /// </summary>
    public class Student : User
    {
        public CoachingMatch Match { get; set; }
    }
}
