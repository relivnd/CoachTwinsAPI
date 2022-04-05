using CoachTwins.Models.Users;
using System;

namespace CoachTwins.Models.Chatting.Messages
{

    /// <summary>
    /// Model of a generic message in the chat. 
    /// </summary>
    public class Message
    {

        /// <summary>
        /// The date and time of when the message was sent.
        /// </summary>
        public DateTime SentAt { get; set; }

        /// <summary>
        /// The sender of the message.
        /// </summary>
        public User Source { get; set; }
    }
}
