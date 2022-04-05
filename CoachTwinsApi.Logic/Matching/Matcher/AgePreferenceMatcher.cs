using System;
using System.Collections.Generic;
using System.Linq;
using CoachTwinsApi.Db.Entities;

namespace CoachTwinsAPI.Logic.Matching.Matcher
{
    public class AgePreferenceMatcher: IMatcher
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
            var now = int.Parse(DateTime.Now.ToString("yyyyMMdd"));
            var coachBirthDay = int.Parse(coach.BirthDate.ToString("yyyyMMdd"));
            var studentCoachBirthDay = int.Parse(coach.BirthDate.ToString("yyyyMMdd"));

            // taking the yyyyMMdd format of the current date - the date in the past / 10000 gives us the amount of years passed
            int coachAge = (now - coachBirthDay) / 10000;
            int studentAge = (now - studentCoachBirthDay) / 10000;

            var score = 0f;

            if (coachAge - studentAge > 2) // student is younger
            {
                score += Convert.ToInt16(desired.Contains("Older")) / 2 + // student desires older => +0.5
                         Convert.ToInt16(desiredCoach.Contains("Younger")) / 2 + // coach desires younger => +0.5
                         -Convert.ToInt16(undesired.Contains("Older")) / 2 + // student does not desire older => -0.5
                         -Convert.ToInt16(undesiredCoach.Contains("Younger")) / 2; // coach does not desire younger => -0.5
            } else if (studentAge - coachAge > 2) // student is older
            {
                score += Convert.ToInt16(desired.Contains("Younger")) / 2 + // student desires younger => +0.5
                         Convert.ToInt16(desiredCoach.Contains("Older")) / 2 + // coach desires older => +0.5
                         -Convert.ToInt16(undesired.Contains("Younger")) / 2 + // student does not desire younger => -0.5
                         -Convert.ToInt16(undesiredCoach.Contains("Older")) / 2; // coach does not desire older => -0.5
            }
            else // student is same age
            {
                score += Convert.ToInt16(desired.Contains("Close to my age")) / 2 + // student desires same age => +0.5
                         Convert.ToInt16(desiredCoach.Contains("Close to my age")) / 2 + // coach desires same age => +0.5
                         -Convert.ToInt16(undesired.Contains("Close to my age")) / 2 + // student does not desire same age => -0.5
                         -Convert.ToInt16(undesiredCoach.Contains("Close to my age")) / 2; // coach does not desire same age => -0.5
            }

            return score;
        }
    }
}