using System.Collections.Generic;
using System.Threading;
using MFCommon;

namespace MessageFilter
{
    /// <summary>
    /// A structure to accept messages from receivers to be passed into the 
    /// filter.
    /// </summary>
    public class MessageQueueImpl : MessageQueue
    {
        private readonly object _lockobj = new object();
        private Queue<Message> _messageQueue;
   
        /// <summary>
        /// Constructor.
        /// </summary>
        public MessageQueueImpl()
        {
            _messageQueue = new Queue<Message>();
        }

        /// <summary>
        /// Insert a new message into the queue.
        /// </summary>
        /// <param name="author">
        /// String representing the author of the 
        /// message.
        /// </param>
        /// <param name="text">
        /// String representation of the text of the 
        /// message.
        /// </param>
        /// <param name="date">
        /// String representation of the date the 
        /// message was received/sent.
        /// </param>
        /// <param name="tag">
        /// The tag associated with the message 
        /// (tagging to be handled by receiver).
        /// </param>
        public void AddMessage(string author, string text, string date, 
            string tag)
        {
            lock (_lockobj)
            {
                _messageQueue.Enqueue(new Message(author, text, date, tag));
                Monitor.Pulse(_lockobj);
            }
        }

        /// <summary>
        /// Fetches the next message in the queue. Waits until messages are
        /// available.
        /// </summary>
        /// <returns>Next Message available.</returns>
        public Message GetNextMessage()
        {
            lock (_lockobj)
            {
                while (_messageQueue.Count == 0)
                {
                    Monitor.Wait(_lockobj);
                }

                return _messageQueue.Dequeue();
            }
        }
    }
}
