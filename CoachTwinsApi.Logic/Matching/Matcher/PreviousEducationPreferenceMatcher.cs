using System.Collections.Generic;
using CoachTwinsApi.Db.Entities;

namespace CoachTwinsAPI.Logic.Matching.Matcher
{
    public class PreviousEducationPreferenceMatcher: IMatcher
    {
        public float Match(Coach coach, Student student, IList<string> desiredCoach, IList<string> undesiredCoach, IList<string> desired, IList<string> undesired)
        {
            var score = 0f;
            
            if (desiredCoach.Contains(student.PreviousEducation))
                score += 0.5f;
            if (desired.Contains(coach.PreviousEducation))
                score += 0.5f;
            if (undesiredCoach.Contains(student.PreviousEducation))
                score -= 0.5f;
            if (undesired.Contains(coach.PreviousEducation))
                score -= 0.5f;
            
            return score;
        }
    }
}