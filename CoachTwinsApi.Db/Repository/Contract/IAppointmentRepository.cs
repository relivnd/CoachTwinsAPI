using System;
using System.Collections.Generic;
using CoachTwinsApi.Db.Entities;

namespace CoachTwinsApi.Db.Repository.Contract
{
    public interface IAppointmentRepository: IBaseRepository<Appointment, Guid>
    {
        public IList<Appointment> GetAllOverlapping(Appointment appointment);
    }
}