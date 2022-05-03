using CoachTwinsApi.Db.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachTwinsApi.Db.ApiModels
{
    public class LoginResponse
    {
        public Guid userId { get; set; }
        public string authToken { get; set; }
    }
}
