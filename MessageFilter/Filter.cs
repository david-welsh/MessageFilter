﻿using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using MFCommon;
using System.Diagnostics;

namespace MessageFilter
{
    /// <summary>
    /// Main framework by which filtering is handled from message queue to filter queue.
    /// </summary>
    public class Filter
    {
        private readonly object _lockobj = new object();

        private MessageQueue _messageQueue;
        private FilterQueueManager _filterQueue;

        private ISet<String> _tags;

        /// <summary>
        /// Constructor for Filter.
        /// </summary>
        /// <param name="filterQueue">FilterQueue managed by caller.</param>
        public Filter(FilterQueueManager filterQueue)
        {
            _messageQueue = new MessageQueueImpl();
            _filterQueue = filterQueue;

            _tags = new HashSet<String>();
        }

        private Boolean ValidateTags(ISet<String> tags)
        {
            foreach (var tag in tags)
            {
                if (!ValidateTag(tag)) return false;
            }
            return true;
        }

        private Boolean ValidateTag(String tag)
        {
            if (tag.Equals("")
                || tag.Equals("#")
                || tag.Any(Char.IsWhiteSpace))
            {
                return false;
            }
            return true;
        }

        private Boolean InsertTag(String tag)
        {
            // Add # if not present.
            if (!tag.StartsWith("#")) tag = "#" + tag;

            lock (_lockobj)
            {
                // Don't accept tags already in the set.
                if (_tags.Contains(tag)) return false;

                _tags.Add(tag);
            }
            return true;
        }

        public Boolean AddTags(ISet<String> tags)
        {
            if (!ValidateTags(tags)) return false;
            foreach (var tag in tags)
            {
                InsertTag(tag);
            }
            return true;
        }

        /// <summary>
        /// Add a new tag for the filter to find in messages.
        /// </summary>
        /// <param name="tag">The tag to search for.</param>
        /// <returns>True if tag is valid and not already present, false 
        /// otherwise.</returns>
        public Boolean AddTag(String tag)
        {
            // Don't accept empty tags or tags with spaces.
            if (!ValidateTag(tag)) return false;

            return InsertTag(tag);
        }

        /// <summary>
        /// Remove a tag to stop the filter getting messages containing that tag.
        /// </summary>
        /// <param name="tag">The tag to remove.</param>
        public void RemoveTag(String tag)
        {
            lock (_lockobj)
            {
                _tags.Remove(tag);
            }
        }

        /// <summary>
        /// Register a message receiver for the filter to get incoming messages 
        /// from.
        /// </summary>
        /// <param name="receiver">The receiver to add.</param>
        public Boolean RegisterReceiver(MessageReceiver receiver)
        {
            if (!receiver.Init(_messageQueue))
            {
                return false;
            }
            Task.Run((Action) receiver.Run);
            return true;
        }

        /// <summary>
        /// Run the filter.
        /// </summary>
        public void Run()
        {
            while (true)
            {
                Message newMessage = _messageQueue.GetNextMessage();
                Debug.WriteLine("Received message...");
                FilterMessage(newMessage);
            }
        }

        public ISet<String> Tags
        {
            get { return _tags; }
        }

        // Check if a message has a tag we are looking for and add it to the 
        // filter queue if so.
        private void FilterMessage(Message message)
        {
            lock (_lockobj)
            {
                if (!_tags.Contains(message.Tag)) return;
            }

            _filterQueue.AddMessage(message);
        }

    }
}
