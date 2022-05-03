using CoachTwinsApi.ApiModels;
using CoachTwinsApi.Db.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachTwinsApi.Db.Repository.Contract
{
    public interface ICoachRequestRepo: IBaseRepository<CoachRequest, Guid>
    {
        Task PerformCoachRequest(Guid studentId);
        Task<bool> Approve(Guid requestId);
        Task<bool> Reject(Guid requestId);
        Task<IEnumerable<CoachRequest>> GetCoachRequests();
    }
}
