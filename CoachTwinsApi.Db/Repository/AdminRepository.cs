using CoachTwinsApi.ApiModels;
using CoachTwinsApi.Db.Entities;
using CoachTwinsApi.Db.Repository.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachTwinsApi.Db.Repository
{
    public class AdminRepository : BaseRepository<Administrator, Guid>, IAdminRepository
    {
        public AdminRepository(CoachTwinsDbContext context) : base(context)
        {
        }

        
    }
}
