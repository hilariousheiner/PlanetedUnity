using System.Collections.Generic;

namespace Planeted
{
    public class PLParser
    {
        private Lexer lexer;
        private Token currentToken;
        private Token nextToken; 

        public PLParser(Lexer lexer) 
        { 
            this.lexer = lexer;
            this.nextToken = this.lexer.Next();
            this.advance();
        }

        public PDSLProgram Parse()
        {
            List<AStatement> statementList = new List<AStatement>();

            while (this.currentToken.TokenType != TokenTypeEnum.End)
            {
                statementList.Add(this.parseStatement());
            }
            return new PDSLProgram(statementList);
        }

        private AStatement parseStatement()
        {
            return null;
        }

        private void advance()
        {
            this.currentToken = this.nextToken;
            this.nextToken = this.lexer.Next();
        }

        private Token expect(TokenTypeEnum tokenType)
        {
            if (this.currentToken.TokenType != tokenType)
            {
                //throw std::runtime_error("Unexpected token: " + TokenTypeToString(this->current.type) + " (expected " + TokenTypeToString(tokenType) + ")");
            }

            Token result = this.currentToken;
            this.advance();

            return result;
        }
    }
}
