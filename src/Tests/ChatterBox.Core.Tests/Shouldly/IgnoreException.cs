using System;
using System.Runtime.Serialization;

namespace ChatterBox.Core.Tests.Shouldly
{
    public class IgnoreException : Exception
    {
        public IgnoreException()
        {
        }

        public IgnoreException(string message) 
            : base(message)
        {
        }

        public IgnoreException(string message, Exception innerException) 
            : base(message, innerException)
        {
        }

        protected IgnoreException(SerializationInfo info, StreamingContext context) 
            : base(info, context)
        {
        }
    }
}