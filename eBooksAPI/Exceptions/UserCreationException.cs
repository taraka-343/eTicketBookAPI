using System;
using System.Runtime.Serialization;

namespace eBooksAPI.Exceptions
{
    [Serializable]
    internal class UserCreationException : Exception
    {
        public UserCreationException()
        {
        }

        public UserCreationException(string message) : base(message)
        {
        }

        public UserCreationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UserCreationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}