using System;
using System.Collections.Generic;

namespace CoachTwinsApi.Db.Entities
{
    public enum CriteriaEvaluationType
    {
        MatchAll,
        MatchOne,
        CustomMatch
    }
    
    public class Criteria
    {
        public string Category { get; set; }
        
        public CriteriaEvaluationType CriteriaEvaluationType { get; set; }
        
        public virtual IEnumerable<MatchingCriteria> MatchingCriteria { get; set; }
    }
}