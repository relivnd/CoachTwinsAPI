using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachTwinsApi.Db.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime AddYears(this DateTime dt, double d)
        {
            var x = Math.Floor(d*365.0);
            return dt.AddDays(x);
        }
    }
}
