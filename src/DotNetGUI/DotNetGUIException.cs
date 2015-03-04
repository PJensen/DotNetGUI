using System;
using System.Runtime.Serialization;

namespace DotNetGUI
{
    /// <summary>
    /// DotNetGUIException
    /// </summary>
    [Serializable]
    public class DotNetGUIException : Exception
    {
        /// <summary>
        /// Creates a new <see cref="DotNetGUIException" />
        /// </summary>
        public DotNetGUIException()
        {
        }

        /// <summary>
        /// Creates a new <see cref="DotNetGUIException" />
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public DotNetGUIException(string message) : base(message)
        {
        }

        /// <summary>
        /// Creates a new <see cref="DotNetGUIException" />
        /// </summary>
        /// <param name="message">the exception message</param>
        /// <param name="inner">the inner exception</param>
        public DotNetGUIException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <summary>
        /// Creates a new <see cref="DotNetGUIException" />
        /// </summary>
        /// <param name="info">exception info</param>
        /// <param name="context">exception context</param>
        protected DotNetGUIException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}