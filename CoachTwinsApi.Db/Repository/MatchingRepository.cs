using System;
using CoachTwinsApi.Db.Entities;
using CoachTwinsApi.Db.Repository.Contract;
using Microsoft.AspNetCore.DataProtection;

namespace CoachTwinsApi.Db.Repository
{
    public class MatchingRepository: BaseRepository<Match, Guid>, IMatchingRepository
    {
        public MatchingRepository(CoachTwinsDbContext context, EntityEncryptor encryptor) : base(context, encryptor)
        {
        }
    }
}