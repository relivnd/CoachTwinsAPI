using System;
using System.Threading.Tasks;
using CoachTwinsApi.Db.Entities;
using Microsoft.AspNetCore.Http;

namespace CoachTwinsApi.Db.Repository.Contract
{
    public interface IAuthRepository : IBaseRepository<AuthToken, Guid>
    {
        public Task<AuthToken?> Check(string token);
        public Task<AuthToken?> Update(string token);
        public Task<AuthToken?> Login(string username,string password);
        public Task<User?> GetCurrentUser(string token);
    }
}