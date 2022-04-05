using System;
using CoachTwinsApi.Db.Entities;
using CoachTwinsApi.Db.Repository.Contract;
using Microsoft.AspNetCore.DataProtection;

namespace CoachTwinsApi.Db.Repository
{
    public class ChatRepository: BaseRepository<Chat, Guid>, IChatRepository
    {
        public ChatRepository(CoachTwinsDbContext context, EntityEncryptor encryptor) : base(context, encryptor)
        {
        }
    }
}