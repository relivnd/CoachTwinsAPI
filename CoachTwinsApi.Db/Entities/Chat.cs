using System;
using System.Collections.Generic;

namespace CoachTwinsApi.Db.Entities
{
    public class Chat
    {
        public Guid Id { get; set; }
        public int UnseenMessagesCoach { get; set; }
        public int UnseenMessagesCoachee { get; set; }
        public virtual IList<Message> Messages { get; set; } = new List<Message>();
        
        public virtual Match Match { get; set; }
        public Guid MatchId { get; set; }
    }
}