using System;
using System.Threading.Tasks;
using CoachTwinsApi.Db.Entities;
using CoachTwinsApi.Db.Repository.Contract;
using Microsoft.AspNetCore.DataProtection;
using System.Linq;
using CoachTwinsApi.Db.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace CoachTwinsApi.Db.Repository
{
    public class UserRepository: BaseRepository<User, Guid>, IUserRepository
    {
        private readonly CoachTwinsDbContext _db;
        public UserRepository(CoachTwinsDbContext context, EntityEncryptor encryptor) : base(context, encryptor)
        {
            this._db = context;   
        }

        public async Task AddProfilePicture(ProfilePicture picture)
        {
            if (!_db.ProfilePicture.Single(p=>p.Id==picture.Id,out var res))
            {
                _db.ProfilePicture.Add(picture);
            }
            else
            {
                res.data = picture.data;
            }
           await _db.SaveChangesAsync();
        }

        public async Task<byte[]?> GetProfilePicture(Guid id)
        {
            if (_db.ProfilePicture.Single(p=>p.Id==id,out var res))
            {
                return res.data;
            }
            return null;
        }

        public async Task<byte[]?> GetProfilePictureByUserId(Guid id)
        {
            if (_db.Users.Single(u=>u.Id==id,out var user))
            {
                var pic = _db.Users.Include("ProfilePicture").Single(uu => uu.Id == user.Id).ProfilePicture;
                return pic is null ? null : pic.data;
            }
            return null;
        }
    }
}