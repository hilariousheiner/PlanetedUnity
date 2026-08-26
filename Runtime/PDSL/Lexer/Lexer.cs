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
            this.source = source;
            this.pos = 0;
        }

        public Token Next()
        {
            this.readWhitespaceAndComments();

            char c = this.peek();

            Token result = new Token(TokenTypeEnum.End, "");

            if (c == '\0')
            {
                return result;
            }

            if (char.IsLetter(c) || c == '_')
            {
                return this.readIdentifier();
            }

            if (char.IsDigit(c))
            {
                return this.readNumber();
            }

            if (c == '"')
            {
                return this.readString();
            }

            this.advance();

            switch (c)
            {
                case '=':
                    result.TokenType = TokenTypeEnum.Equals;
                    result.Lexeme = "=";
                    break;
                case ';':
                    result.TokenType = TokenTypeEnum.Semicolon; 
                    result.Lexeme = ";";
                    break;
                case '(':
                    result.TokenType = TokenTypeEnum.LParen; 
                    result.Lexeme = "(";
                    break;
                case ')':
                    result.TokenType = TokenTypeEnum.RParen;
                    result.Lexeme = ")";
                    break;
                case '[':
                    result.TokenType = TokenTypeEnum.LBrack;
                    result.Lexeme = "[";
                    break;
                case ']':
                    result.TokenType = TokenTypeEnum.RBrack;
                    result.Lexeme = "]";
                    break;
                case ',':
                    result.TokenType = TokenTypeEnum.Comma;
                    result.Lexeme = ",";
                    break;
                case '-':
                    result.TokenType = TokenTypeEnum.Minus;
                    result.Lexeme = "-";
                    break;
                default:
                    throw new LexerException("unexpected character: " + c, this.pos);
            }
            return result;
        }

        private void readWhitespaceAndComments()
        {
            while (true)
            {
                while (char.IsWhiteSpace(this.peek()))
                {
                    this.advance();
                }

                if (this.peek() == '/' && this.peekNext() == '/')
                {
                    while (this.peek() != '\n' && this.peek() != '\0')
                    {
                        this.advance();
                    }
                    continue;
                }

                if (this.peek() == '/' && this.peekNext() == '*')
                {
                    this.advance(); // /
                    this.advance(); // *

                    while (true)
                    {
                        if (this.peek() == '\0')
                        {
                            throw new LexerException("Unterminated block comment.", this.pos);
                        }

                        if (this.peek() == '*' && this.peekNext() == '/')
                        {
                            this.advance(); // *
                            this.advance(); // /
                            break;
                        }
                        this.advance();
                    }
                    continue;
                }
                break;
            }
        }

        private Token readIdentifier()
        {
            string lexeme = string.Empty;

            while(char.IsLetterOrDigit(this.peek()) || this.peek() == '_')
            {
                lexeme += this.advance();
            }

            if(keywordDict.ContainsKey(lexeme))
            {
                return new Token(keywordDict[lexeme], lexeme);
            }
            return new Token(TokenTypeEnum.Identifier, lexeme);
        }
        private Token readNumber()
        {
            string lexeme = string.Empty;
            bool isFloat = false;

            while (char.IsDigit(this.peek()) || this.peek() == '.')
            {
                if (this.peek() == '.')
                {
                    if (isFloat)
                    {
                        throw new LexerException("Invalid float.", this.pos);
                    }
                    isFloat = true;
                }
                lexeme += this.advance();
            }

            if (isFloat)
            {
                return new Token(TokenTypeEnum.FloatLiteral, lexeme);
            }
            return new Token(TokenTypeEnum.IntLiteral, lexeme);
        }
        private Token readString()
        {
            string lexeme = string.Empty;

            if (this.peek() == '"')
            {
                this.advance(); // discard initial "

                while (this.peek() != '"')
                {
                    if (this.peek() == '\0')
                    {
                        throw new LexerException("unterminated string literal.", this.pos);
                    }
                    lexeme += this.advance();
                }
                this.advance(); // discard final "
            }
            return new Token(TokenTypeEnum.StringLiteral, lexeme);
        }

        private char peek()
        {
            if (this.pos >= this.source.Length)
            {
                return '\0';
            }
            return this.source[pos];
        }
        private char peekNext()
        {
            int next = this.pos + 1;
            if (next >= this.source.Length)
            {
                return '\0';
            }
            return this.source[next];
        }
        private char advance()
        {
            if (this.pos >= this.source.Length)
            {
                return '\0';
            }
            return this.source[this.pos++];
        }
    }
}