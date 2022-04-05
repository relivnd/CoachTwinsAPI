using System;
using CoachTwinsApi.Db.Entities;
using CoachTwinsApi.Db.Repository.Contract;
using Microsoft.AspNetCore.DataProtection;

namespace CoachTwinsApi.Db.Repository
{
    public class MatchingCriteriaRepository: BaseRepository<MatchingCriteria, Guid>, IMatchingCriteriaRepository
    {
        public MatchingCriteriaRepository(CoachTwinsDbContext context, EntityEncryptor encryptor) : base(context, encryptor)
        {
        }
    }
}