using System;
using System.Collections.Generic;
using MFCommon;

namespace MessageFilter
{
    /// <summary>
    /// Maintains the data structure for managing the queues associated with 
    /// each tag.
    /// </summary>
    public class FilterQueueManager
    {
        private readonly object _lockobj = new object();

        private Dictionary<String, Queue<Message>> _filterQueues;

        private Action<String> _messageHook;

        /// <summary>
        /// Constructor for filter queues.
        /// </summary>
        /// <param name="messageHook">Action to perform after message is 
        /// received.</param>
        public FilterQueueManager(Action<String> messageHook = null)
        {
            _filterQueues = new Dictionary<string, Queue<Message>>();

            _messageHook = messageHook;
        }

        // Add a message to the appropriate queue if the tag is being filtered.
        internal void AddMessage(Message message)
        {
            if (message.Tag == null) return;
            lock (_lockobj)
            {
                if (!_filterQueues.ContainsKey(message.Tag))
                {
                    _filterQueues.Add(message.Tag, new Queue<Message>());
                }
                _filterQueues[message.Tag].Enqueue(message);
            }

            _messageHook(message.Tag);
        }

        /// <summary>
        /// Fetches the next message in a queue for a specific tag.
        /// </summary>
        /// <param name="tag">The tag to get the next queued message 
        /// for.</param>
        /// <returns>The next Message in the queue.</returns>
        public Message GetNextMessage(String tag)
        {
            lock (_lockobj)
            {
                if (!_filterQueues.ContainsKey(tag)) return null;
                return _filterQueues[tag].Dequeue();
            }
        }
    }
}
