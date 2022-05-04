using CoachTwinsApi.ApiModels;
using CoachTwinsApi.Db.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachTwinsApi.Db.Repository.Contract
{
    public interface IAdminRepository: IBaseRepository<Administrator, Guid>
    {
        
    }
}
