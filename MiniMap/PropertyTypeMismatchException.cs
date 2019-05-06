using System;

namespace MiniMap
{
    public class PropertyTypeMismatchException : Exception
    {
        public PropertyTypeMismatchException()
            : base("Mapping Exception: Property Type Mismatch")
        {
        }

        public PropertyTypeMismatchException(string message) : base(message)
        {
        }

        public PropertyTypeMismatchException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}