using System;

namespace Planeted
{
    public class PLRuntimeException : Exception
    {
        public int Position { get; }

        public PLRuntimeException(string message, int position)
            : base(message)
        {
            this.Position = position;
        }
    }
}