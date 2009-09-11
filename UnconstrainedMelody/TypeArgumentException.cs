using System;
using System.Runtime.Serialization;

namespace UnconstrainedMelody
{
    /// <summary>
    /// Exception thrown to indicate that an inappropriate type argument was used for
    /// a type parameter to a generic type or method.
    /// </summary>
    public class TypeArgumentException : Exception
    {
        /// <summary>
        /// Constructs a new instance of TypeArgumentException with no message.
        /// </summary>
        public TypeArgumentException()
        {
        }

        /// <summary>
        /// Constructs a new instance of TypeArgumentException with the given message.
        /// </summary>
        /// <param name="message">Message for the exception.</param>
        public TypeArgumentException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Constructs a new instance of TypeArgumentException with the given message and inner exception.
        /// </summary>
        /// <param name="message">Message for the exception.</param>
        /// <param name="inner">Inner exception.</param>
        public TypeArgumentException(string message, Exception inner)
            : base(message, inner)
        {
        }

        /// <summary>
        /// Constructor provided for serialization purposes.
        /// </summary>
        /// <param name="info">Serialization information</param>
        /// <param name="context">Context</param>
        protected TypeArgumentException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
