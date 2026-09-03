using System;

namespace Planeted
{
    public class PLParserException : Exception
    {
        public int Position { get; }

        public PLParserException(string message, int position)
            : base(message)
        {
            this.Position = position;
        }
    }
}