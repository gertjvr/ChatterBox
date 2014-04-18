using System;

namespace ChatterBox.Core.Tests.Shouldly
{
    public static partial class Should
    {
        public static void Ignore(string message)
        {
            throw new IgnoreException(message);
        }

        public static void Ignore(string message, Exception innerException)
        {
            throw new IgnoreException(message, innerException);
        }

        public static void Inconclusive(string message)
        {
            throw new InconclusiveException(message);
        }

        public static void Inconclusive(string message, Exception innerException)
        {
            throw new InconclusiveException(message, innerException);
        }

        public static void Inconclusive()
        {
            throw new InconclusiveException();
        }
    }
}