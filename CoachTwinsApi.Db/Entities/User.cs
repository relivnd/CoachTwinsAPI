using System;
using System.Collections.Generic;
using CoachTwinsApi.Db.Attribute;
using CoachTwinsApi.Db.Extensions;

namespace CoachTwinsApi.Db.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        
        [DontEncrypt]
        public string Email { get; set; }

        [DontEncrypt]
        public string Password { get; set; } = "pass123".Sha256();
        
        [DontEncrypt]

        public string FirstName { get; set; }
        
        [DontEncrypt]
        public string LastName { get; set; }
        
        [DontEncrypt]
        public DateTime BirthDate { get; set; }
        
        [DontEncrypt]
        public string Gender { get; set; }
        
        [DontEncrypt]
        public int StartingYear { get; set; }
        
        [DontEncrypt]
        public string PreviousEducation { get; set; }
        
        [DontEncrypt]
        public string UniversityProgram { get; set; }
        
        [DontEncrypt]
        public string Description { get; set; }
        
        public bool IsProfileSetup { get; set; }
        
        public bool IsPrivacyPolicyAccepted { get; set; }
        
        public virtual Guid ProfilePicture { get; set; }

        public virtual IList<MatchingCriteria> MatchingCriteria { get; set; } = new List<MatchingCriteria>();
    }
}