using System;
using CoachTwinsApi.Db.Entities;

namespace CoachTwinsApi.Db.Repository.Contract
{
    public interface IChatRepository: IBaseRepository<Chat, Guid>
    {
        
    }
}