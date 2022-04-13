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

        public async Task AddProfilePictureFor(ProfilePicture pic, User user)
        {
            _db.ProfilePictures.RemoveRange(_db.ProfilePictures.Where(p => p.UserId == user.Id || p.Id == pic.Id));
            await _db.SaveChangesAsync();
            var  picture = new ProfilePicture()
                {
                    data = pic.data,
                    UserId = user.Id
                };
            _db.ProfilePictures.Add(picture); 
            user.ProfilePicture = picture.Id;
            _db.SaveChanges();
        }

        public async Task FileReport(ProfileReport report)
        {
            _db.ProfileReports.Add(report);
           await _db.SaveChangesAsync();
        }

        public async Task<byte[]> GetProfilePicture(Guid id)
        {
            return !_db.ProfilePictures.Single(p => p.Id == id, out var picture) ? null : picture.data;
        }

        public async Task<byte[]?> GetProfilePictureByUserId(Guid id)
        {
            if (!_db.Users.Single(u=>u.Id==id,out var user))
            {
                return null;
            }
            if (!_db.ProfilePictures.Single(p=>p.UserId==user.Id,out var picture))
            {
                return null;
            }
            return picture.data;
        }
    }
}