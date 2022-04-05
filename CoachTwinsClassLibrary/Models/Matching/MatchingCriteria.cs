namespace CoachTwins.Models.Matching
{
    
    /// <summary>
    /// A model that binds criteria to a user.
    /// </summary>
    public class MatchingCriteria
    {
        /// <summary>
        /// True => the user desires this.
        /// False => the user does not desire this
        /// </summary>
        public bool Prefer { get; set; }

        public string Key { get; set; }
        
        public string Value { get; set; }
    }
}