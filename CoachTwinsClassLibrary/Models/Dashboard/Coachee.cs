using CoachTwins.Models.Matching;
using System;

namespace CoachTwinsClassLibrary.Models.Dashboard
{
    public class Coachee
    {
        
        /// <summary>
        /// The coachee's identification.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The coachee's first name.
        /// </summary>
        public string FirstName { get; set; }
        
        /// <summary>
        /// The coachee's last name.
        /// </summary>
        public string LastName { get; set; }

        public CoachingMatch Match { get; set; }
        
        /// <summary>
        /// The coachee's profile picture.
        /// </summary>
        public byte[] ProfilePicture { get; set; }
        
        public int UnreadMessages { get; set; }
        
    }
}