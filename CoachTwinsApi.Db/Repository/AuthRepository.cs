using CoachTwinsApi.Db.Entities;
using CoachTwinsApi.Db.Extensions;
using CoachTwinsApi.Db.Repository.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachTwinsApi.Db.Repository
{
    public class AuthRepository : BaseRepository<AuthToken, Guid>, IAuthRepository
    {
        protected readonly CoachTwinsDbContext _context;
        public AuthRepository(CoachTwinsDbContext context) : base(context)
        {
            this._context = context;
        }
        public async Task<AuthToken?> Check(string token)
        {
            return _context.AuthTokens.Single(a => a.IsValid && a.Active && a.Value == token, out var res) ? res : null;
        }


        public async Task<AuthToken?> Update(string token)
        {
            var tokenObj = await Check(token);
            if (tokenObj is null)
            {
                return null;
            }
            tokenObj.Value = StringExtensions.RandomString(20);
            tokenObj.ValidThru = DateTime.Now.AddHours(48);
            await _context.SaveChangesAsync();
            return tokenObj;
        }

        public async Task<Student?> GetCurrentStudent(string token)
        {
            var authTokenObject = await Check(token);
            if (authTokenObject == null)
            {
                return null;
            }
            if (!_context.Students.Single(u => u.Id == authTokenObject.ActiveGuid, out var user))
            {
                return null;
            }
            return user;
        }
    }
}
