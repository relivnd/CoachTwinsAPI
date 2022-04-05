using System;

namespace CoachTwinsAPI.Models.Admin
{
    public class AdminUser
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
        public bool isACoach { get; set; }
    }
}