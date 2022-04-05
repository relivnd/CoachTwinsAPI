using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoachTwinsApi.Db.Entities;
using CoachTwinsApi.Db.Repository;
using CoachTwinsApi.Db.Repository.Contract;

namespace CoachTwinsAPI.Logic.Matching
{
    public class MatchingService
    {
        private ICoachRepository _coachRepository;
        private ICriteriaRepository _criteriaRepository;

        public MatchingService(ICoachRepository coachRepository, ICriteriaRepository criteriaRepository)
        {
            _coachRepository = coachRepository;
            _criteriaRepository = criteriaRepository;
        }

        public Match GetMatchFromUser(User user, Guid matchId)
        {
            return user switch
            {
                Coach c => c.Matches.FirstOrDefault(m => m.Id == matchId),
                Student s => s.Match,
                _ => null
            };
        }

        /// <summary>
        /// Get 3 coaches with matching criteria
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Coach>> GetCoachesWithMatchingCharacteristics(Student student)
        {
            var studentDesiredCriteria = student
                .MatchingCriteria
                .Where(c => c.Prefer ?? false)
                .ToLookup(c => 
                    c.Criteria.Category,
                    c => c.Value
                );

            var studentAvoidCriteria = student
                .MatchingCriteria
                .Where(c => !c.Prefer ?? false)
                .ToLookup(
                    c => c.Criteria.Category,
                    c => c.Value
                );

            var criteriaList = await _criteriaRepository.GetAll();
            var possible = (await _coachRepository.GetCoachesWithMatchingCharacteristics(student)).ToList();
            
            var sortedList = new List<Tuple<float, Coach>>();
            

            // loop through the coaches
            foreach (var possibleCoach in possible)
            {
                // keep track of a score that determines how suited the coach is
                var coachScore = 0f;

                // create lookups for desired and undesired criteria, keyed by the criteria category
                var coachDesiredCriteria = possibleCoach.MatchingCriteria.Where(mc => mc.Prefer == true).ToLookup(mc => mc.Criteria.Category, mc => mc.Value);
                var coachUndesiredCriteria = possibleCoach.MatchingCriteria.Where(mc => mc.Prefer == false).ToLookup(mc => mc.Criteria.Category, mc => mc.Value);

                // lower the score based on how many matches the coach has
                coachScore -= possibleCoach.Matches.Count * 6;
                
                foreach (var criteria in criteriaList)
                {
                    // keep score how well the current criteria matches
                    var criteriaScore = 0f;
                    
                    // get the values of the current criteria for the coach and student
                    var coachDesiredValues = coachDesiredCriteria[criteria.Category].ToList();
                    var coachUndesiredValues = coachUndesiredCriteria[criteria.Category].ToList();
                    var desiredValues = studentDesiredCriteria[criteria.Category].ToList();
                    var undesiredValues = studentAvoidCriteria[criteria.Category].ToList();

                    switch (criteria.CriteriaEvaluationType)
                    {
                        case CriteriaEvaluationType.MatchAll:
                            // + the amount of options that both desire / the total amount of options the student desires
                            criteriaScore += desiredValues.Sum(desired => coachDesiredValues.Any(c => desired == c) ? 1 : 0) / (!desiredValues.Any() ? 1f : desiredValues.Count);
                            // - the amount of options the the student does not desire, but the coach does / the total undesired options of the student
                            criteriaScore -= undesiredValues.Sum(undesired => coachDesiredValues.Any(c => undesired == c) ? 1 : 0) / (!undesiredValues.Any() ? 1f : undesiredValues.Count);
                            // - the amount of options the student desires, but the coach does not / the total desired options of the student
                            criteriaScore -= desiredValues.Sum(undesired => coachUndesiredValues.Any(c => undesired == c) ? 1 : 0) / (!desiredValues.Any() ? 1f : desiredValues.Count);
                            break;
                        case CriteriaEvaluationType.MatchOne:
                            // +1 if the student and coach both desire 1 of the options
                            criteriaScore += desiredValues.Any(desired => coachDesiredValues.Any(c => desired == c)) ? 1 : 0;
                            // -1 if the student does not desire an option but the coach does
                            criteriaScore -= undesiredValues.Any(undesired => coachDesiredValues.Any(c => undesired == c)) ? 1 : 0;
                            // -1 if the student desires an option that the coach does not
                            criteriaScore -= desiredValues.Any(undesired => coachUndesiredValues.Any(c => undesired == c)) ? 1 : 0;
                            break;
                        case CriteriaEvaluationType.CustomMatch:
                            var matcher = MatcherFactory.GetMatcherFromCritera(criteria);
                            
                            if (matcher == null)
                                continue;

                            criteriaScore += matcher.Match(possibleCoach, student, coachDesiredValues, coachUndesiredValues, desiredValues, undesiredValues);
                            break;
                    }

                    coachScore += criteriaScore;
                }

                sortedList.Add(new Tuple<float, Coach>(coachScore, possibleCoach));
            }

            var result = sortedList
                .OrderByDescending(o => o.Item1)
                .Take(3)
                .Select(o => o.Item2)
                .ToList();
            
            return result;
        }
    }
}