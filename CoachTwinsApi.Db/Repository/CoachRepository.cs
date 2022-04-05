using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoachTwinsApi.Db.Entities;
using CoachTwinsApi.Db.Repository.Contract;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;

namespace CoachTwinsApi.Db.Repository
{
    public class CoachRepository : BaseRepository<Coach, Guid>, ICoachRepository
    {
        public CoachRepository(CoachTwinsDbContext context, EntityEncryptor encryptor) : base(context, encryptor)
        {
        }
        
        /// <summary>
        /// Get coaches that have atleast 1 matching prefered criteria, if non are available get all of them
        /// </summary>
        /// <param name="student"></param>
        /// <param name="studentDesiredCriteria"></param>
        /// <param name="studentAvoidCriteria"></param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<Coach>> GetCoachesWithMatchingCharacteristics(Student student)
        {
            //filter out coaches that have 0 overlapping matching criteria
            var possibleQuery = _dbSet
                .Where(c => c.AvailableForMatching)
                .Where(c => c.MatchingCriteria
                    .Where(m => m.Prefer ?? false)
                    .Select(m => m.Criteria)
                    .Where(m => m.CriteriaEvaluationType != CriteriaEvaluationType.CustomMatch)
                    .SelectMany(c => c.MatchingCriteria)
                    .Where(m => m.Prefer ?? false)
                    .Select(m => m.User)
                    .Any(u => u.Id == student.Id)
                );

            if (!possibleQuery.Any())
            {
                possibleQuery = _dbSet
                    .Include(c => c.MatchingCriteria)
                    .ThenInclude(m => m.Criteria)
                    .AsSplitQuery()
                    .Where(c => c.AvailableForMatching);
            }

            return possibleQuery;
        }
    }
}