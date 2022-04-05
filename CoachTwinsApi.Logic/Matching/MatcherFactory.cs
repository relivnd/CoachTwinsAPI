using System;
using System.Linq;
using System.Reflection;
using CoachTwinsApi.Db.Entities;
using CoachTwinsAPI.Logic.Matching.Matcher;

namespace CoachTwinsAPI.Logic.Matching
{
    public static class MatcherFactory
    {
        /// <summary>
        /// Create a matcher based on the category of the criteria
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns>The matcher or null if not found</returns>
        public static IMatcher GetMatcherFromCritera(Criteria criteria)
        {
            var assembly = Assembly.GetExecutingAssembly();
            
            // take the criteria category, append Matcher and try to find a class with that name that inherits from IMatcher
            var type = assembly.GetTypes()
                .Where(typeof(IMatcher).IsAssignableFrom)
                .Where(t => t.BaseType != t)
                .FirstOrDefault(t => t.Name == $"{string.Concat(criteria.Category[0].ToString().ToUpper(), criteria.Category.AsSpan(1))}Matcher");

            if (type == null)
                return null;
            
            return Activator.CreateInstance(type) as IMatcher;
        }
    }
}