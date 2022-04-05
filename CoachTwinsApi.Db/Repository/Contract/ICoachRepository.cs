using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoachTwinsApi.Db.Entities;

namespace CoachTwinsApi.Db.Repository.Contract
{
    public interface ICoachRepository: IBaseRepository<Coach, Guid>
    {
        public Task<IEnumerable<Coach>> GetCoachesWithMatchingCharacteristics(Student student);
    }
}