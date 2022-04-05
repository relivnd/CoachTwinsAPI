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
        private readonly EntityEncryptor _encryptor;

        public DbSeeder(CoachTwinsDbContext context, EntityEncryptor encryptor)
        {
            _context = context;
            _encryptor = encryptor;
        }

        /// <summary>
        /// Initialize the database with seeding data.
        /// </summary>
        public void Initialize()
        {
            var criterias = new List<Criteria>
            {
                new()
                {
                    Category = "GenderPreference",
                    CriteriaEvaluationType = CriteriaEvaluationType.CustomMatch
                },
                new()
                {
                    Category = "Affinity",
                    CriteriaEvaluationType = CriteriaEvaluationType.MatchAll
                },
                new()
                {
                    Category = "Hobbies",
                    CriteriaEvaluationType = CriteriaEvaluationType.MatchAll
                },
                new()
                {
                    Category = "Competences",
                    CriteriaEvaluationType = CriteriaEvaluationType.MatchAll
                },
                new()
                {
                    Category = "AgePreference",
                    CriteriaEvaluationType = CriteriaEvaluationType.CustomMatch
                },
            };
            _context.AddRange(criterias);
            _context.SaveChanges();

            var rand = new Random();
            var firstnames = new List<string>()
            {
                "John",
                "Patrick",
                "Jan",
                "Youri",
                "Henk",
                "Klaas",
                "Pieter",
                "Hans",
                "Harry",
                "Ron",
                "Jane",
                "Walter",
                "Bob",
                "Karen",
                "Jan",
                "Erik"
            };

            var lastnames = new List<string>()
            {
                "Doe",
                "Pietersen",
                "Klazen",
                "de Vries",
                "van den Berg",
                "van Dijk",
                "Bakker",
                "Jansen",
                "Visser",
                "Polskiman",
                "de Wit",
                "Zwart",
                "Wit",
                "Witte",
                "van Veen",
                "Veenstra"
            };

            var competences = new List<string>()
            {
                "Patience",
                "Calmness",
                "Respectful",
                "Strictness",
                "Planning and structure",
                "Humour",
                "Introverted",
                "Extroverted",
                "Trustworthiness"
            };

            var hobbies = new List<string>()
            {
                "Sports",
                "Gaming",
                "Watching series",
                "Hanging out with friends",
                "Musical instruments",
                "Musical styles",
                "Scouts"
            };

            var affinities = new List<string>()
            {
                "Autism",
                "Dyslexia",
                "ADHD",
                "Psychological issues",
                "Physical issues",
                "Fear of failure",
                "NT2",
                "Caregiver",
                "Highly intelligent"
            };

            var genderList = new List<string>()
            {
                "Male",
                "Female",
                "Other"
            };

            var agePreference = new List<string>()
            {
                "Younger",
                "Older",
                "Close to my age"
            };

            var coaches = new List<Coach>();

            for (var i = 0; i < 100; i++)
            {
                var fn = firstnames[rand.Next(0, firstnames.Count)];
                var ln = lastnames[rand.Next(0, lastnames.Count)];
                var coach = new Coach()
                {
                    Description = "I am a testing coach!",
                    Email = $"s{rand.Next(0, 999999)}@student.windesheim.nl",
                    FirstName = fn,
                    LastName = ln,
                    StartingYear = 2016 + rand.Next(3, 5),
                    BirthDate = DateTime.Now.AddDays(-5840 - rand.Next(1, 2190)),
                    Gender = genderList[rand.Next(0, genderList.Count)],
                    UniversityProgram = "HBO-ICT",
                    AvailableForMatching = true,
                    MatchingCriteria = new List<MatchingCriteria>()
                };

                coach.MatchingCriteria.Add(new MatchingCriteria()
                {
                    Criteria = criterias[4],
                    Prefer = true,
                    Value = agePreference[rand.Next(0, agePreference.Count)]
                });

                var tempAffinities = new List<string>(affinities);
                var tempCompetences = new List<string>(competences);
                var tempHobbies = new List<string>(hobbies);
                var tempGenders = new List<string>(genderList);

                var nr = rand.Next(1, 3);

                for (var x = 0; x < nr; x++)
                {
                    var affIndex = rand.Next(0, tempAffinities.Count);
                    coach.MatchingCriteria.Add(new MatchingCriteria()
                    {
                        Criteria = criterias[1],
                        Prefer = true,
                        Value = tempAffinities[affIndex]
                    });
                    tempAffinities.RemoveAt(affIndex);

                    var genderIndex = rand.Next(0, tempGenders.Count);
                    coach.MatchingCriteria.Add(new MatchingCriteria()
                    {
                        Criteria = criterias[0],
                        Prefer = true,
                        Value = tempGenders[genderIndex]
                    });
                    tempGenders.RemoveAt(genderIndex);
                }

                nr = rand.Next(2, 5);
                for (var x = 0; x < nr; x++)
                {
                    var competenceIndex = rand.Next(0, tempCompetences.Count);
                    coach.MatchingCriteria.Add(new MatchingCriteria()
                    {
                        Criteria = criterias[3],
                        Prefer = true,
                        Value = tempCompetences[competenceIndex]
                    });
                    tempCompetences.RemoveAt(competenceIndex);
                    var hobbyIndex = rand.Next(0, tempHobbies.Count);
                    coach.MatchingCriteria.Add(new MatchingCriteria()
                    {
                        Criteria = criterias[2],
                        Prefer = true,
                        Value = tempHobbies[hobbyIndex]
                    });
                    tempHobbies.RemoveAt(hobbyIndex);
                }

                coaches.Add(coach);
            }

            var studentsCoaching = new List<Student>()
            {
                new()
                {
                    FirstName = "Tom", LastName = "Bouderij",
                    Email = "s3478973@student.windesheim.nl", UniversityProgram = "HBO-ICT",
                    Description = "Hi im Tom im from Inholland good luck from Haarlem!",
                    BirthDate = DateTime.Now.AddYears(-24.2)
                },
                new()
                {
                    FirstName = "Karol", LastName = "Kowalski",
                    Email = "s1129161@student.windesheim.nl", UniversityProgram = "HBO-ICT",
                    Description = "Why hello there thank u for looking at my profile page I'm Karol I'm 34 years old and I come from Poland",
                     BirthDate = DateTime.Now.AddYears(-18.1)
                },
                 new()
                {
                    FirstName = "Artur", LastName = "Arturson",
                    Email = "s5543623@student.windesheim.nl", UniversityProgram = "HBO-ICT",
                    Description = "I am Artur I like football and programming. I'm from Poland",
                     BirthDate = DateTime.Now.AddYears(-21.55)
                },
                  new()
                {
                    FirstName = "Tobias", LastName = "Arturson",
                    Email = "s43589@student.windesheim.nl", UniversityProgram = "HBO-ICT",
                    Description = "Hi im Tobias I'm from Switzerland I like good swiss cheese and web development.",
                     BirthDate = DateTime.Now.AddYears(-22.4)
                },
                   new()
                {
                    FirstName = "Robin", LastName = "Pleziertje",
                    Email = "s696969@student.windesheim.nl", UniversityProgram = "HBO-ICT",
                    Description = "It student looking for a good time. hmu on instagram. I like [REDACTED BY COACHTWIN ABUSE API] and cats.",
                     BirthDate = DateTime.Now.AddYears(-18.1)
                },
                    new()
                {
                    FirstName = "Lenny", LastName = "Visser",
                    Email = "s345546@student.windesheim.nl", UniversityProgram = "HBO-ICT",
                    Description = "Hello I'm Lenny I study at Windesheim. I'm good at react and making Mobile Solution. I'm a bit autistic (see profile) so be patient please.",
                     BirthDate = DateTime.Now.AddYears(-18.1)
                },
            };

            _context.Users.AddRange(studentsCoaching);
            _context.Users.AddRange(coaches);

            _context.SaveChanges();
        }
    }
}