using System;

namespace Planeted
{
    public class LexerException : Exception
    {
        public int Position { get; }

        public LexerException(string message, int position)
            : base(message)
        {
            this.Position = position;
        }
    }
}