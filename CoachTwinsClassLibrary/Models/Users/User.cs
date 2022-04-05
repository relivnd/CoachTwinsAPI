using System;
using System.Collections.Generic;

namespace CoachTwins.Models.Users
{
    /// <summary>
    /// Model representing a generic user of Coach Twins.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Identifier
        /// </summary>
        public Guid Id { get; set; }
        
        /// <summary>
        /// The first name of the user.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The last name of the user.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// the e-mail address of the user, e.g. "s1234567@student.windesheim.nl".
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// A short description of the user for the purpose of introductions.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The year in which the user started.
        /// </summary>
        public int StartingYear { get; set; }

        /// <summary>
        /// The age of the user.
        /// </summary>
        public int Age { get; set; }
        
        public string PreviousEducation { get; set; }
        
        public string Gender { get; set; }

        /// <summary>
        /// The user's profile picture' GUID.
        /// </summary>
        public Guid ProfilePicture { get; set; }
        
        /// <summary>
        /// Checker for whether the user has set up their profile.
        /// </summary>
        public bool IsProfileSetup { get; set; }
        
        /// <summary>
        /// Checker for whether the user has accepted the privacy policy.
        /// </summary>
        public bool IsPrivacyPolicyAccepted { get; set; }
        
        /// <summary>
        /// The program in which the user is currently enrolled.
        /// </summary>
        public string UniversityProgram { get; set; }

        /// <summary>
        /// The characteristics of the coach/ the characteristics a student is looking for.
        /// </summary>
        public IList<string> Characteristics { get; set; }
        
        /// <summary>
        /// The hobbies of the user.
        /// </summary>
        public IList<string> Interests { get; set; }
        
        /// <summary>
        /// The special needs that a coach can handle/a student has.
        /// </summary>
        public IList<string> SpecialNeeds { get; set; }
    }
}
