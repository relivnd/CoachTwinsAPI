using CoachTwinsApi.Db.Entities;
using CoachTwinsApi.Db.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachTwinsApi.Db.Repository.Contract
{
    public class PortalUserRepository :  BaseRepository<PortalUser, Guid>, IPortalUserRepository
    {
        private CoachTwinsDbContext _db;
        public PortalUserRepository(CoachTwinsDbContext context, EntityEncryptor encryptor) : base(context, encryptor)
        {
            this._db = context;
        }
    }
}
