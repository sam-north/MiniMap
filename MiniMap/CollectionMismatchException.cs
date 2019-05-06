using System;

namespace MiniMap
{
    public class CollectionMismatchException : Exception
    {
        public CollectionMismatchException()
            : base("Mapping Exception: Both source and destination must be a collection")
        {
        }

        public CollectionMismatchException(string message) : base(message)
        {
        }

        public CollectionMismatchException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}