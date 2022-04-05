using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoachTwinsApi.Db;
using CoachTwinsApi.Db.Entities;
using CoachTwinsApi.Db.Repository;
using CoachTwinsApi.Db.Repository.Contract;
using CoachTwinsAPI.Logic.Matching;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using Match = CoachTwinsApi.Db.Entities.Match;

namespace CoachTwinsApi.Test
{
    [TestFixture]
    public class MatchingServiceTest
    {
        private Coach CreateCoachWithCriteria(IList<(Criteria, string, bool)> competences, int matchCount = 0)
        {
            return new Coach()
            {
                Id = Guid.NewGuid(),
                Matches = Enumerable.Repeat(new Match(), matchCount).ToList(),
                MatchingCriteria = competences.Select(c => new MatchingCriteria()
                {
                    Criteria = c.Item1,
                    Value = c.Item2,
                    Prefer = c.Item3,
                    Id = Guid.NewGuid()
                }).ToList()
            };
        }
        
        private Student CreateStudentWithCriteria(IList<(Criteria, string, bool)> competences)
        {
            return new Student()
            {
                MatchingCriteria = competences.Select(c => new MatchingCriteria()
                {
                    Criteria = c.Item1,
                    Value = c.Item2,
                    Prefer = c.Item3,
                    Id = Guid.NewGuid()
                }).ToList()
            };
        }
        
        [Test]
        public async Task TestGetBestCoach()
        {
            var mockRepository = new Mock<ICoachRepository>();
            var mockCriteriaRepository = new Mock<ICriteriaRepository>();

            var competenceCriteria = new Criteria()
            {
                Category = "Competence",
                CriteriaEvaluationType = CriteriaEvaluationType.MatchAll
            };
            
            var coaches = new List<Coach>()
            {
                CreateCoachWithCriteria(new List<(Criteria, string, bool)>
                {
                    (competenceCriteria, "Voetbal", true),
                    (competenceCriteria, "Lezen", true),
                }),
                CreateCoachWithCriteria(new List<(Criteria, string, bool)>
                {
                    (competenceCriteria, "Hockey", true),
                    (competenceCriteria, "Voetbal", true),
                }),
                CreateCoachWithCriteria(new List<(Criteria, string, bool)>
                {
                    (competenceCriteria, "Wandelen", true),
                    (competenceCriteria, "Gamen", true),
                }),
                CreateCoachWithCriteria(new List<(Criteria, string, bool)>
                {
                    (competenceCriteria, "Gamen", true),
                    (competenceCriteria, "Lezen", true),
                }),
            };

            var student = CreateStudentWithCriteria(new List<(Criteria, string, bool)>
            {
                (competenceCriteria, "Lezen", true),
                (competenceCriteria, "Gamen", true),
                (competenceCriteria, "Voetbal", false),
            });

            mockCriteriaRepository.Setup(r => r.GetAll())
                .ReturnsAsync(new List<Criteria> { competenceCriteria });
            mockRepository.Setup(r => r.GetCoachesWithMatchingCharacteristics(It.IsAny<Student>())).ReturnsAsync(coaches);
            
            var test = (await new MatchingService(mockRepository.Object, mockCriteriaRepository.Object)
                .GetCoachesWithMatchingCharacteristics(student)).ToList();
            
            Assert.AreEqual(test[0].Id, coaches[3].Id);
            Assert.AreEqual(test[1].Id, coaches[2].Id);
        }

        [Test]
        public async Task TestAgeMatcher()
        {
            var mockRepository = new Mock<ICoachRepository>();
            var mockCriteriaRepository = new Mock<ICriteriaRepository>();
            var ageCriteria = new Criteria()
            {
                Category = "AgePreference",
                CriteriaEvaluationType = CriteriaEvaluationType.CustomMatch
            };

            var coaches = new List<Coach>
            {
                CreateCoachWithCriteria(new List<(Criteria, string, bool)>
                {
                    (ageCriteria, "Older", true)
                }),
                CreateCoachWithCriteria(new List<(Criteria, string, bool)>
                {
                    (ageCriteria, "Younger", true)
                }),
                CreateCoachWithCriteria(new List<(Criteria, string, bool)>
                {
                    (ageCriteria, "Same Age", true)
                }),
                CreateCoachWithCriteria(new List<(Criteria, string, bool)>
                {
                    (ageCriteria, "Older", true)
                }),
                CreateCoachWithCriteria(new List<(Criteria, string, bool)>
                {
                    (ageCriteria, "Younger", true)
                }),
            };
            
            coaches[0].BirthDate = DateTime.Now.AddYears(-14);
            coaches[1].BirthDate = DateTime.Now.AddYears(-23);
            coaches[2].BirthDate = DateTime.Now.AddYears(-18);
            coaches[3].BirthDate = DateTime.Now.AddYears(-16);
            coaches[4].BirthDate = DateTime.Now.AddYears(-19);
            
            var student = CreateStudentWithCriteria(new List<(Criteria, string, bool)>
            {
                (ageCriteria, "Younger", true)
            });
            
            student.BirthDate = DateTime.Now.AddYears(-18);
            
            mockCriteriaRepository.Setup(r => r.GetAll())
                .ReturnsAsync(new List<Criteria> { ageCriteria });
            mockRepository.Setup(r => r.GetCoachesWithMatchingCharacteristics(It.IsAny<Student>())).ReturnsAsync(coaches);
            
            var test = (await new MatchingService(mockRepository.Object, mockCriteriaRepository.Object)
                .GetCoachesWithMatchingCharacteristics(student)).ToList();
            
            Assert.AreEqual(test[0].Id, coaches[0].Id);
        }

        [Test]
        public async Task TestGenderMatcher()
        {
            var mockRepository = new Mock<ICoachRepository>();
            var mockCriteriaRepository = new Mock<ICriteriaRepository>();
            var criteria = new Criteria()
            {
                Category = "GenderPreference",
                CriteriaEvaluationType = CriteriaEvaluationType.CustomMatch
            };

            var coaches = new List<Coach>
            {
                CreateCoachWithCriteria(new List<(Criteria, string, bool)>
                {
                    (criteria, "Female", true)
                }),
                CreateCoachWithCriteria(new List<(Criteria, string, bool)>
                {
                    (criteria, "Male", true)
                }),
                CreateCoachWithCriteria(new List<(Criteria, string, bool)>
                {
                    (criteria, "Female", true)
                }),
                CreateCoachWithCriteria(new List<(Criteria, string, bool)>
                {
                    (criteria, "Male", true)
                }),
            };
            
            coaches[0].Gender = "Female";
            coaches[1].Gender = "Male";
            coaches[2].Gender = "Male";
            coaches[3].Gender = "Female";
            
            var student = CreateStudentWithCriteria(new List<(Criteria, string, bool)>
            {
                (criteria, "Male", true)
            });
            
            student.Gender = "Male";
            
            mockCriteriaRepository.Setup(r => r.GetAll())
                .ReturnsAsync(new List<Criteria> { criteria });
            mockRepository.Setup(r => r.GetCoachesWithMatchingCharacteristics(It.IsAny<Student>())).ReturnsAsync(coaches);
            
            var test = (await new MatchingService(mockRepository.Object, mockCriteriaRepository.Object)
                .GetCoachesWithMatchingCharacteristics(student)).ToList();
            
            Assert.AreEqual(test[0].Id, coaches[1].Id);
        }

        [Test]
        public async Task TestEducationMatcher()
        {
            var mockRepository = new Mock<ICoachRepository>();
            var mockCriteriaRepository = new Mock<ICriteriaRepository>();
            var criteria = new Criteria()
            {
                Category = "PreviousEducationPreference",
                CriteriaEvaluationType = CriteriaEvaluationType.CustomMatch
            };

            var coaches = new List<Coach>
            {
                CreateCoachWithCriteria(new List<(Criteria, string, bool)>
                {
                    (criteria, "Havo", true)
                }),
                CreateCoachWithCriteria(new List<(Criteria, string, bool)>
                {
                    (criteria, "MBO", true)
                }),
                CreateCoachWithCriteria(new List<(Criteria, string, bool)>
                {
                    (criteria, "Mavo", true)
                }),
                CreateCoachWithCriteria(new List<(Criteria, string, bool)>
                {
                    (criteria, "Havo", true)
                }),
                CreateCoachWithCriteria(new List<(Criteria, string, bool)>
                {
                    (criteria, "VMBO", true)
                }),
            };

            coaches[0].PreviousEducation = "Havo";
            coaches[1].PreviousEducation = "MBO";
            coaches[2].PreviousEducation = "Mavo";
            coaches[3].PreviousEducation = "Havo";
            coaches[4].PreviousEducation = "VMBO";
            
            var student = CreateStudentWithCriteria(new List<(Criteria, string, bool)>
            {
                (criteria, "MBO", true)
            });
            student.PreviousEducation = "MBO";

            mockCriteriaRepository.Setup(r => r.GetAll())
                .ReturnsAsync(new List<Criteria> { criteria });
            mockRepository.Setup(r => r.GetCoachesWithMatchingCharacteristics(It.IsAny<Student>())).ReturnsAsync(coaches);
            
            var test = (await new MatchingService(mockRepository.Object, mockCriteriaRepository.Object)
                .GetCoachesWithMatchingCharacteristics(student)).ToList();
            
            Assert.AreEqual(test[0].Id, coaches[1].Id);
        }
        
        [Test]
        public async Task TestPrioritizeCoachesWithoutCoachee()
        {
            var mockRepository = new Mock<ICoachRepository>();
            var mockCriteriaRepository = new Mock<ICriteriaRepository>();

            var competenceCriteria = new Criteria()
            {
                Category = "Competence",
                CriteriaEvaluationType = CriteriaEvaluationType.MatchAll
            };
            
            var coaches = new List<Coach>()
            {
                CreateCoachWithCriteria(new List<(Criteria, string, bool)>
                {
                    (competenceCriteria, "Voetbal", true),
                    (competenceCriteria, "Lezen", true),
                }),
                CreateCoachWithCriteria(new List<(Criteria, string, bool)>
                {
                    (competenceCriteria, "Hockey", true),
                    (competenceCriteria, "Voetbal", true),
                }),
                CreateCoachWithCriteria(new List<(Criteria, string, bool)>
                {
                    (competenceCriteria, "Wandelen", true),
                    (competenceCriteria, "Gamen", true),
                }),
                CreateCoachWithCriteria(new List<(Criteria, string, bool)>
                {
                    (competenceCriteria, "Gamen", true),
                    (competenceCriteria, "Lezen", true),
                }, 1),
            };

            var student = CreateStudentWithCriteria(new List<(Criteria, string, bool)>
            {
                (competenceCriteria, "Lezen", true),
                (competenceCriteria, "Gamen", true),
                (competenceCriteria, "Voetbal", false),
            });

            mockCriteriaRepository.Setup(r => r.GetAll())
                .ReturnsAsync(new List<Criteria> { competenceCriteria });
            mockRepository.Setup(r => r.GetCoachesWithMatchingCharacteristics(It.IsAny<Student>())).ReturnsAsync(coaches);
            
            var test = (await new MatchingService(mockRepository.Object, mockCriteriaRepository.Object)
                .GetCoachesWithMatchingCharacteristics(student)).ToList();
            
            Assert.AreEqual(test[0].Id, coaches[2].Id);
            Assert.IsTrue(test.All(c => c.Id != coaches[3].Id));
        }
    }
}