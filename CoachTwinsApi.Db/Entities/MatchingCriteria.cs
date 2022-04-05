using System;
using CoachTwinsApi.Db.Attribute;

namespace CoachTwinsApi.Db.Entities
{
    public class MatchingCriteria
    {
        public Guid Id { get; set; }
        public virtual User User { get; set; }
        public bool? Prefer { get; set; }
        public virtual Criteria Criteria { get; set; }
        
        [DontEncrypt]
        public string Value { get; set; }
    }
}