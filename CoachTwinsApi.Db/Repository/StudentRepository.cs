using System;
using System.Linq;
using System.Threading.Tasks;
using CoachTwinsApi.Db.Entities;
using CoachTwinsApi.Db.Repository.Contract;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;

namespace CoachTwinsApi.Db.Repository
{
    public class StudentRepository: BaseRepository<Student, Guid>, IStudentRepository
    {
        public StudentRepository(CoachTwinsDbContext context, EntityEncryptor encryptor) : base(context, encryptor)
        {
        }
    }
}