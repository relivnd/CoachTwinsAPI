using CoachTwinsApi.Db.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachTwinsApi.Db.Entities
{
    public class PortalUser
    {
        public Guid Id { get; set; }
        public string Email { get; set; }

        public string Password { get; set; } = "pass123".Sha256();

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
