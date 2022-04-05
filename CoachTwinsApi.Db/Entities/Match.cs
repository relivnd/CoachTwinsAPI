using System;
using System.Collections.Generic;

namespace CoachTwinsApi.Db.Entities
{
    public class Match
    {
        public Guid Id { get; set; }
        public DateTime? MatchedOn { get; set; }
        public virtual Student Student { get; set; }
        public Guid StudentId { get; set; }
        public virtual Coach Coach { get; set; }
        public Guid CoachId { get; set; }
        public virtual IList<Appointment> Appointments { get; set; }
        
        public virtual Chat Chat { get; set; }

        public bool? isAMatch
        {
            get
            {
                if (isAMatchForCoach == false || isAMatchForCoachee == false)
                    return false;
                if (isAMatchForCoach == true && isAMatchForCoachee == true)
                    return true;

                return null;
            }
        }
        
        public bool? isAMatchForCoachee { get; set; }
        public bool? isAMatchForCoach { get; set; }
    }
}