using CoachTwins.Models.Matching;
using System.Collections.Generic;

namespace CoachTwinsMobileApp.ClassLibrary.Models.Profile
{
    public class ProfileMatchingCriteriaSetupRequest
    {
        public IList<MatchingCriteria> MatchingCriteria { get; set; }
    }
}