using System;

namespace MiniMap
{
    public class MissingGenericTypeFromCollectionException : Exception
    {
        public MissingGenericTypeFromCollectionException()
            : base("Mapping Exception: Missing generic type from collection")
        {
        }

        public MissingGenericTypeFromCollectionException(string message) : base(message)
        {
        }

        public MissingGenericTypeFromCollectionException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}