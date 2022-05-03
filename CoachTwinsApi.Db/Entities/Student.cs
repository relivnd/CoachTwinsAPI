﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachTwinsApi.Db.Entities
{
    public enum StudentType
    {
        Coach, Coachee
    }
    public class Student
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public StudentType studentType { get; set; }
        public bool IsCoach => studentType == StudentType.Coach;
        public bool ISCoachee => studentType == StudentType.Coachee;
    }
}
