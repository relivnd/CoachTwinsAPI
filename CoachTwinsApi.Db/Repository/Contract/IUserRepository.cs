using System;
using System.Threading.Tasks;
using CoachTwinsApi.Db.Entities;

namespace CoachTwinsApi.Db.Repository.Contract
{
    public interface IUserRepository: IBaseRepository<User, Guid>
    {
        public Task<byte[]?> GetProfilePicture(Guid id);
        public Task<byte[]?> GetProfilePictureByUserId(Guid id);
        public Task AddProfilePicture(ProfilePicture picture);

    }

}