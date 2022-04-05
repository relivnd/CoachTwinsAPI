using System;

namespace CoachTwins.Models.Matching
{
    public class ProfileData
    {
        public string Description { get; set; }
        
        public DateTime BirthDate { get; set; }
        
        public byte[] ProfilePicture { get; set; }
        public string Gender { get; set; }
        public string PreviousEducation { get; set; }
    }
}