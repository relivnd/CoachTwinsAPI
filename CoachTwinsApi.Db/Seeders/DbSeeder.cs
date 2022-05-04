using System;
using System.Collections.Generic;
using System.Linq;
using CoachTwinsApi.Db.Entities;
using CoachTwinsApi.Db.Extensions;

namespace CoachTwinsApi.Db.Seeders
{

    /// <summary>
    /// Seed the database for first-time usage. 
    /// </summary>
    public class DbSeeder
    {
        private readonly CoachTwinsDbContext _context;

        public DbSeeder(CoachTwinsDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Initialize the database with seeding data.
        /// </summary>
        public void Initialize()
        {
            _context.RemoveRange(_context.Students);
            _context.RemoveRange(_context.Administrators); 
            _context.RemoveRange(_context.CoachRequests);
            _context.RemoveRange(_context.AuthTokens);
            _context.SaveChanges();
            var tom = new Student()
            {
                FirstName="Tom",
                LastName="Bouderij",
                Email="tom@windesheim.nl",
                Password="test".Sha256(),
                studentType=StudentType.Coachee
            };
            var lenny = new Student()
            {
                FirstName = "lenny",
                LastName = "Visser",
                Email = "lenny@windesheim.nl",
                Password = "test".Sha256(),
                studentType = StudentType.Coachee
            };
            var karol = new Student()
            {
                FirstName = "Karol",
                LastName = "Kowalski",
                Email = "karol@windesheim.nl",
                Password = "test".Sha256(),
                studentType = StudentType.Coachee
            };
            var artur = new Student()
            {
                FirstName = "Artur",
                LastName = "Milosz",
                Email = "artur@windesheim.nl",
                Password = "test".Sha256(),
                studentType = StudentType.Coachee
            };
            var tobias = new Student()
            {
                FirstName = "Tobias",
                LastName = "Relivnd",
                Email = "tobias@windesheim.nl",
                Password = "test".Sha256(),
                studentType = StudentType.Coachee
            };
            List<Student> randomstudents = new();
            for(var i = 0; i < 50; i++)
            {
                randomstudents.Add(new()
                {
                    FirstName="Rand"+i,
                    LastName = "Randomson",
                    Email=@$"{i}@windesheim.nl",
                    Password="test".Sha256(),
                    studentType=StudentType.Coachee
                });
            }
            _context.Students.Add(tom);
            _context.Students.Add(lenny);
            _context.Students.Add(artur);
            _context.Students.Add(karol);
            _context.Students.Add(tobias);
            _context.AddRange(randomstudents);
            _context.Administrators.Add(new() {
            FirstName="Leonard",
            LastName="Visser",
            Email="leonard@windesheim.nl",
            Password="test".Sha256()
            });
            _context.SaveChanges();

        }
    }
}