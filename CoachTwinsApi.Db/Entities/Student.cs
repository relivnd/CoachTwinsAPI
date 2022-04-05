using System;

namespace CoachTwinsApi.Db.Entities
{
    public class Student : User
    {
        public virtual Match? Match { get; set; }
    }
}