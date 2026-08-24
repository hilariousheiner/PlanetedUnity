namespace Planeted
{
    public class Token
    {
        public TokenTypeEnum TokenType;
        public string Lexeme;

        public Token(TokenTypeEnum tokenType, string lexeme)
        {
            this.TokenType = tokenType;
            this.Lexeme = lexeme;
        }

        public static string TokenTypeToString(TokenTypeEnum tokenType)
        {
            string result = "unknown token type";
            switch (tokenType)
            {
                case TokenTypeEnum.Identifier:
                    result = "Identifier";
                    break;
                case TokenTypeEnum.IntLiteral:
                    result = "IntLiteral";
                    break;
                case TokenTypeEnum.FloatLiteral:
                    result = "FloatLiteral";
                    break;
                case TokenTypeEnum.BoolLiteral:
                    result = "BoolLiteral";
                    break;
                case TokenTypeEnum.StringLiteral:
                    result = "StringLiteral";
                    break;
                case TokenTypeEnum.NullLiteral:
                    result = "NullLiteral";
                    break;
                case TokenTypeEnum.Import:
                    result = "Import";
                    break;
                case TokenTypeEnum.Return:
                    result = "Return";
                    break;
                case TokenTypeEnum.Equals:
                    result = "Equals";
                    break;
                case TokenTypeEnum.Semicolon:
                    result = "Semicolon";
                    break;
                case TokenTypeEnum.LParen:
                    result = "(";
                    break;
                case TokenTypeEnum.RParen:
                    result = ")";
                    break;
                case TokenTypeEnum.LBrack:
                    result = "[";
                    break;
                case TokenTypeEnum.RBrack:
                    result = "]";
                    break;
                case TokenTypeEnum.Comma:
                    result = ",";
                    break;
                case TokenTypeEnum.Minus:
                    result = "-";
                    break;
                case TokenTypeEnum.End:
                    result = "End";
                    break;
                default:
                    break;
            }
            return result;
        }
    }
}