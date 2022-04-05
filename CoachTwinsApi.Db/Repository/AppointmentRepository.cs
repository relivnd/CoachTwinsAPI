using System;
using System.Collections.Generic;
using System.Linq;
using CoachTwinsApi.Db.Entities;
using CoachTwinsApi.Db.Repository.Contract;
using Microsoft.AspNetCore.DataProtection;

namespace CoachTwinsApi.Db.Repository
{
    public class AppointmentRepository: BaseRepository<Appointment, Guid>, IAppointmentRepository
    {
        public AppointmentRepository(CoachTwinsDbContext context, EntityEncryptor encryptor) : base(context, encryptor)
        {
        }
        
        public IList<Appointment> GetAllOverlapping(Appointment appointment)
        {
            return _context.Appointments
                .Where(a => a.Match.Coach.Id == appointment.Creator.Id || a.Match.Student.Id == appointment.Creator.Id)
                .Where(a => !a.Canceled)
                .Where(a => a.Start <= appointment.Start && appointment.Start <= a.End || a.End >= appointment.End && appointment.End >= a.Start)
                .ToList();
        }
    }
}