using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachTwinsApi.Db.ApiModels
{
    public class GenericResponse
    {
        public GenericResponse(string msg)
        {
            this.message = msg;
        }
        public string message { get; set; }
    }
}
