using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CoachTwinsApi.Db.Extensions
{
    public static class LinqExtensions
    {
        public static bool Single<Q>(this IEnumerable<Q> iterable, Func<Q, bool> predicate, out Q res)
        {
            res = default;
            var q = iterable.Where(predicate);
            if (q.Count() == 1)
            {
                res = q.Single();
                return true;
            }
            return false;
        }
    }
}
