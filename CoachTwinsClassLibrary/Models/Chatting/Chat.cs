using CoachTwins.Models.Chatting.Messages;
using System.Collections.Generic;

namespace CoachTwins.Models.Chatting
{
    /// <summary>
    /// Model representing a chat.
    /// </summary>
    public class Chat
    {
        
        /// <summary>
        /// All the messages sent within the chat.
        /// </summary>
        public IList<Message> Messages { get; set; }

    }
}
