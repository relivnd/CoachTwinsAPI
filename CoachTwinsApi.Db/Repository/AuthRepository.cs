using System;
using System.Threading.Tasks;
using CoachTwinsApi.Db.Entities;
using CoachTwinsApi.Db.Repository.Contract;
using Microsoft.AspNetCore.DataProtection;
using System.Linq;
using CoachTwinsApi.Db.Extensions;
using Microsoft.AspNetCore.Http;

namespace CoachTwinsApi.Db.Repository
{
    public class AuthRepository: BaseRepository<AuthToken, Guid>, IAuthRepository
    {
        private CoachTwinsDbContext _db;
        public AuthRepository(CoachTwinsDbContext context, EntityEncryptor encryptor) : base(context, encryptor)
        {
            this._db = context;
        }
        public async Task<AuthToken?> Check(string token)
        {
            return _db.AuthTokens.Single(a => a.IsValid && a.Active && a.Value == token, out var res) ? res : null;
        }

        public async Task<AuthToken?> Login(string username, string password)
        {
            if (_db.Users.Single(u=>u.Email==username && u.Password == password,out var user))
            {
                var token = new AuthToken()
                {
                    Active = true,
                    ActiveGuid= user.Id,
                    ValidThru=DateTime.Now.AddHours(48),
                    LoginType=LoginType.AppUser,
                    Value=StringExtensions.RandomString(30),
                };
                _db.AuthTokens.Add(token);
                await _db.SaveChangesAsync();
                return token;
            }
            if (_db.PortalUsers.Single(u => u.Email == username && u.Password == password, out var portalUser))
            {
                var token = new AuthToken()
                {
                    Active = true,
                    ActiveGuid = portalUser.Id,
                    ValidThru = DateTime.Now.AddHours(48),
                    LoginType = LoginType.PortalUser,
                    Value = StringExtensions.RandomString(30),
                };
                _db.AuthTokens.Add(token);
                await _db.SaveChangesAsync();
                return token;
            }

            return null;
        }

        public async Task<AuthToken?> Update(string token)
        {
         if (!_db.AuthTokens.Single(a=>a.Value == token,out var authToken))
            {
                return null;
            }
            authToken.Value = StringExtensions.RandomString(20);
            authToken.ValidThru = DateTime.Now.AddHours(48);
           await _db.SaveChangesAsync();
            return authToken;
        }
        public async Task<User?> GetCurrentUser(string token)
        {
            var authTokenObject = await Check(token);
            if (authTokenObject == null)
            {
                return null;
            }
            if (!_db.Users.Single(u=>u.Id==authTokenObject.ActiveGuid,out var user))
            {
                return null;
            }
            return user;
        }
        
    }
}