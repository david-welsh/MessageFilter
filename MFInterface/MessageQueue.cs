using System;

namespace MFCommon
{
    /// <summary>
    /// Interface for a message queue that accepts messages from a receiver.
    /// </summary>
    public interface MessageQueue
    {
        /// <summary>
        /// Inssert a new message into the queue.
        /// </summary>
        /// <param name="author">Author of the message.</param>
        /// <param name="text">Text of the message.</param>
        /// <param name="date">Date the message was sent/received.</param>
        /// <param name="tag">Tag asscociated with the message.</param>
        void AddMessage(String author, String text, String date, String tag);

        /// <summary>
        /// Fetches the next message in the queue.
        /// </summary>
        /// <returns>The next message available.</returns>
        Message GetNextMessage();
    }
}
