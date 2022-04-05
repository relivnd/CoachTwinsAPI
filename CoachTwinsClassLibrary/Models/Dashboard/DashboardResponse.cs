using System;
using System.Collections.Generic;

namespace CoachTwinsClassLibrary.Models.Dashboard
{
    public class DashboardResponse
    {
        public string? Action { get; set; }
        
        public int AppointmentNotifications { get; set; }
        public Dictionary<Guid, int> UnseenMessagesCount { get; set; }
    }
}