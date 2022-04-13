using System;
using System.Threading.Tasks;
using CoachTwinsApi.Db.Entities;

namespace CoachTwinsApi.Db.Repository.Contract
{
    public interface IUserRepository: IBaseRepository<User, Guid>
    {
        public Task<byte[]?> GetProfilePicture(Guid id);
        public Task<byte[]?> GetProfilePictureByUserId(Guid id);
        public Task AddProfilePictureFor(ProfilePicture picture, User user);
        public Task FileReport(ProfileReport report);

    }

}