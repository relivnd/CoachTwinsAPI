using System.Collections.Generic;

namespace CoachTwins.Models.Matching.Requests
{
    public class StudentProfileSetupRequest: ProfileSetupRequest
    {
        public ProfileData ProfileData { get; set; }
    }
}