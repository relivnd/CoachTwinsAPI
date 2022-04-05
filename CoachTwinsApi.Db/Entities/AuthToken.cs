using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachTwinsApi.Db.Entities
{
    public class AuthToken
    {
        public Guid Id { get; set; }
        [Required]
        [MinLength(15)]
        public string Value { get; set; }
        [Required]
        public DateTime ValidThru { get; set; }
        [Required]
        public bool Active { get; set; } = false;
        public bool IsValid => DateTime.Now <= ValidThru && Active;
        public Guid ActiveGuid { get; set; }
        public LoginType LoginType { get; set; }
    }
}
