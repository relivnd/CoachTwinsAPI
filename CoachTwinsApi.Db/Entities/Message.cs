using System;
using System.Collections.Generic;

namespace CoachTwinsApi.Db.Entities
{
    public class Message
    {
        public Guid Id { get; set; }
        public DateTime SendAt { get; set; }
        public virtual User Source { get; set; }
        public string Text { get; set; }
        public virtual Chat Chat { get; set; }
    }
}