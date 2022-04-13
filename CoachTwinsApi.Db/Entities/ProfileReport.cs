using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachTwinsApi.Db.Entities
{
    public class ProfileReport
    {
        public Guid Id { get; set; }
        public DateTime Issued { get; set; }
        public Guid ReportedUserId { get; set; }
        public Guid IssuerId { get; set; }
        
        public string Description { get; set; }
    }
}
