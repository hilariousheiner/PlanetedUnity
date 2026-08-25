using System.Collections.Generic;

namespace Planeted
{
    public class Lexer
    {
        private static Dictionary<string, TokenTypeEnum> keywordDict = new Dictionary<string, TokenTypeEnum>
        {
            { "true", TokenTypeEnum.BoolLiteral },
            { "false", TokenTypeEnum.BoolLiteral },
            { "null", TokenTypeEnum.NullLiteral },
            { "import", TokenTypeEnum.Import },
            { "return", TokenTypeEnum.Return },
        };

        private string source;
        private int pos;

        public Lexer(string source)
        {

        }
        public Token Next()
        {
            return null;
        }

        private void readWhitespaceAndComments()
        {

        }

        private Token readIdentifier()
        {
            return null;
        }
        private Token readNumber()
        {
            return null;
        }
        private Token readString()
        {
            return null;
        }

        private char peek()
        {
            return '\0';
        }
        private char peekNext()
        {
            return '\0';
        }
        private char advance()
        {
            return '\0';
        }
    }
}