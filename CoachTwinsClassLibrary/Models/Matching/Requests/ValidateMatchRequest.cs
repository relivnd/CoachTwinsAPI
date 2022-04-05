using System;

namespace CoachTwins.Models.Matching.Requests
{
    public class ValidateMatchRequest
    {
        public bool AppointmentWasHeld { get; set; }
        public bool IsAMatch { get; set; }
        public Guid MatchId { get; set; }
    }
}