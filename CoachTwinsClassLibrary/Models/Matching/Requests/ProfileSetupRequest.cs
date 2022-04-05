using System.Collections.Generic;

namespace CoachTwins.Models.Matching.Requests
{
    public class ProfileSetupRequest
    {
        public IList<MatchingCriteria> MatchingCriteria { get; set; }
    }
}