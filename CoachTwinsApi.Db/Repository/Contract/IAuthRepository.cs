using CoachTwinsApi.Db.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachTwinsApi.Db.Repository.Contract
{
    public interface IAuthRepository : IBaseRepository<AuthToken, Guid>
    {
        public Task<AuthToken?> Check(string token);
        public Task<AuthToken?> Update(string token);
        public Task<Student?> GetCurrentStudent(string token);
    }
}
