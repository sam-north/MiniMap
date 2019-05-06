using System;

namespace MiniMap
{
    public class MaxCloneDepthExceededException : Exception
    {
        public MaxCloneDepthExceededException()
            : base("Mapping Exception: Max clone depth exceeded")
        {
        }

        public MaxCloneDepthExceededException(string message) : base(message)
        {
        }

        public MaxCloneDepthExceededException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}