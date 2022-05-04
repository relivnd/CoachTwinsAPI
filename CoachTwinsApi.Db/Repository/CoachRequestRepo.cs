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
    public class CoachRequestRepo : BaseRepository<CoachRequest, Guid>, ICoachRequestRepo
    {
        public CoachRequestRepo(CoachTwinsDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<CoachRequest>> GetCoachRequests()
        {
            return _context.CoachRequests.ToList();
        }

        public async Task<bool> Approve(Guid Id)
        {
            var q = await TryFind(c=>c.Id==Id);
            if (q.succes == false)
            {
                throw new Exception("Id invalid");
            }
            var student = _context.Students.Single(s => s.Id == q.result.StudentId);
            if (student is null)
            {
                throw new Exception("Student not found");
            }
            if (student.IsCoach) {
                throw new Exception("Student is already a coach");
            }
            student.studentType = StudentType.Coach;
            q.result.Status = CoachRequestStatus.Accepted;
            q.result.ReviewedOn = DateTime.Now;
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Reject(Guid Id)
        {
            var q = await TryFind(c => c.Id == Id);
            if (q.succes == false)
            {
                throw new Exception("Id invalid");
            }
            var student = _context.Students.Single(s => s.Id == q.result.StudentId);
            if (student is null)
            {
                throw new Exception("Student not found");
            }
            if (student.IsCoach)
            {
                throw new Exception("Student is already a coach");
            }
            q.result.Status = CoachRequestStatus.Denied;
            q.result.ReviewedOn = DateTime.Now;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task PerformCoachRequest(Guid studentId)
        {
            if (_context.Students.Single(s=>s.Id==studentId) is null)
            {
                throw new Exception("Student not found");
            }
            foreach (var req in _context.CoachRequests.Where(c=>c.StudentId==studentId))
            {
                _context.Remove(req);
            }
            await _context.SaveChangesAsync();
            var request = new CoachRequest()
            {
                StudentId = studentId,
                CreatedOn = DateTime.Now,
                Status = CoachRequestStatus.Pending
            };
            _context.CoachRequests.Add(request);
            await _context.SaveChangesAsync();
        }
       
    }
}
