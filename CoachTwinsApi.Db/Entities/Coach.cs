using System;
using System.Collections.Generic;

namespace CoachTwinsApi.Db.Entities
{
    public class Coach : User
    {
        public virtual IList<Match> Matches { get; set; }
        
        public bool IsMatchingProfileSetup { get; set; }
        
        public bool AvailableForMatching { get; set; }
    }
}