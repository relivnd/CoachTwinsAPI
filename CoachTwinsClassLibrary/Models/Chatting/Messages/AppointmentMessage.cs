using CoachTwins.Models.Appointments;

namespace CoachTwins.Models.Chatting.Messages
{
    /// <summary>
    /// Model representing a message with an appointment in it.
    /// </summary>
    public class AppointmentMessage : Message
    {

        /// <summary>
        /// The appointment in the message.
        /// </summary>
        public Appointment Appointment { get; set; }

    }
}
