using System;
using CoachTwinsApi.Db.Entities;

namespace CoachTwinsApi.Db.Repository.Contract
{
    public interface IMatchingRepository: IBaseRepository<Match, Guid>
    {
        
    }
}