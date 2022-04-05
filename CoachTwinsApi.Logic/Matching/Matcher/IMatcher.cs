using System.Collections.Generic;
using CoachTwinsApi.Db.Entities;

namespace CoachTwinsAPI.Logic.Matching.Matcher
{
    public interface IMatcher
    {
        /// <summary>
        /// Match score for the criteria
        /// </summary>
        /// <param name="coach"></param>
        /// <param name="student"></param>
        /// <param name="desiredCoach"></param>
        /// <param name="undesiredCoach"></param>
        /// <param name="desired"></param>
        /// <param name="undesired"></param>
        /// <returns>A score how well the criteria matches</returns>
        public float Match(
            Coach coach,
            Student student,
            IList<string> desiredCoach,
            IList<string> undesiredCoach,
            IList<string> desired,
            IList<string> undesired
        );
    }
}