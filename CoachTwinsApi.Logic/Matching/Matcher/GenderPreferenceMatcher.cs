using System.Collections.Generic;
using CoachTwinsApi.Db.Entities;

namespace CoachTwinsAPI.Logic.Matching.Matcher
{
    public class GenderPreferenceMatcher: IMatcher
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
        )
        {
            var coachGender = coach.Gender;
            var studentGender = student.Gender;

            var score = 0f;

            if (desired.Contains(coachGender))
                score += 0.5f;
            if (desiredCoach.Contains(studentGender))
                score += 0.5f;
            if (undesired.Contains(coachGender))
                score -= 0.5f;
            if (undesiredCoach.Contains(studentGender))
                score -= 0.5f;

            return score;
        }
    }
}