using System;
using CoachTwinsApi.Db.Attribute;

namespace CoachTwinsApi.Db.Entities
{
    public class Appointment
    {
        public Guid Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Location { get; set; }
        public bool? Accepted { get; set; }
        public bool Canceled { get; set; }
        public virtual User Creator { get; set; }
        public virtual Match Match { get; set; }
        public Guid MatchId { get; set; }
        
        public bool LastChangeSeenByReceiver { get; set; }
    }
}