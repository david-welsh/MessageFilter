using System;

namespace MFCommon
{
    /// <summary>
    /// Representation of messages.
    /// </summary>
    public class Message
    {
        private String _author;
        private String _text;
        private String _date;

        private String _tag;

        /// <summary>
        /// Constructor for message.
        /// </summary>
        /// <param name="author">Author of the message.</param>
        /// <param name="text">Text of the message.</param>
        /// <param name="date">Date the message was sent/received.</param>
        /// <param name="tag">Tag associated with the message.</param>
        public Message(String author, String text, String date, String tag)
        {
            _author = author;
            _text = text;
            _date = date;
            _tag = tag;
        }

        public String Author
        {
            get { return _author; }
        }

        public String Text
        {
            get { return _text; }
        }

        public String Date
        {
            get { return _date; }
        }

        public String Tag
        {
            get { return _tag; }
            set { _tag = value; }
        }

    }
}
