namespace CoachTwins.Models.Chatting.Messages
{
    /// <summary>
    /// Model representing a message with text in it.
    /// </summary>
    public class TextMessage : Message
    {

        /// <summary>
        /// The text contents of the message.
        /// </summary>
        public string Text { get; set; }

    }
}
