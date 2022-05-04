using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachTwinsApi.Db.Entities
{
    public enum CoachRequestStatus
    {
        Pending,
        Accepted,
        Denied
    }
    public class CoachRequest
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public CoachRequestStatus Status { get; set; }
        public DateTime? ReviewedOn { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
