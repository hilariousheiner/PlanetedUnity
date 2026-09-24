using System.Collections.Generic;
using System.Linq.Expressions;

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
            AStatement result;

            if (this.currentToken.TokenType == TokenTypeEnum.Return)
            {
                result = this.parseReturnStatement();
                //ToDo: stop parsing. If not eof => unreachable code detected.
            }
            else
            {
                if (this.currentToken.TokenType == TokenTypeEnum.Import)
                {
                    result = this.parseImportStatement();
                }
                else
                {
                    if (this.nextToken.TokenType == TokenTypeEnum.Equals)
                    {
                        result = this.parseAssignmentStatement();
                    }
                    else
                    {
                        result = this.parseExpressionStatement();
                    }
                }
            }
            return result;
        }

        private ImportStatement parseImportStatement()
        {
            this.expect(TokenTypeEnum.Import);

            string path = this.expect(TokenTypeEnum.StringLiteral).Lexeme;

            return new ImportStatement(path);
        }

        private ReturnStatement parseReturnStatement()
        {
            // return
            this.expect(TokenTypeEnum.Return);

            // expression
            AExpression expression = this.parseExpression();

            // ;
            this.expect(TokenTypeEnum.Semicolon);

            return new ReturnStatement(expression);
        }
        private AssignmentStatement parseAssignmentStatement()
        {
            // identifier
            string name = this.expect(TokenTypeEnum.Identifier).Lexeme;

            // =
            this.expect(TokenTypeEnum.Equals);

            // expression
            AExpression expression = this.parseExpression();

            // ;
            this.expect(TokenTypeEnum.Semicolon);

            return new AssignmentStatement(name, expression);
        }

        private ExpressionStatement parseExpressionStatement()
        {
            AExpression expression = this.parseExpression();

            // ;
            this.expect(TokenTypeEnum.Semicolon);

            return new ExpressionStatement(expression);
        }

        private AExpression parseExpression()
        {
            return this.parseUnaryExpression();
        }

        private AExpression parseUnaryExpression()
        {
            if (this.currentToken.TokenType == TokenTypeEnum.Minus)
            {
                this.advance();

                AExpression operand = this.parseUnaryExpression();
                return new UnaryExpression(TokenTypeEnum.Minus, operand);
            }
            return this.parsePrimaryExpression();
        }
        private AExpression parsePrimaryExpression()
        {
            if (this.currentToken.TokenType == TokenTypeEnum.Identifier)
            {
                if (this.nextToken.TokenType == TokenTypeEnum.LParen)
                {
                    return this.parseCallExpression();
                }
                string identifier = this.currentToken.Lexeme;
                this.advance();
                return new VariableExpression(identifier);
            }
            if (this.currentToken.TokenType == TokenTypeEnum.LParen)
            {
                return this.parseTupleExpression();
            }
            if (this.currentToken.TokenType == TokenTypeEnum.LBrack)
            {
                return this.parseListExpression();
            }
            return this.parseLiteral();
        }

        private ConstantExpression parseLiteral()
        {
            PDSLValue result = PDSLValue.Null;

            switch (this.currentToken.TokenType)
            {
                case TokenTypeEnum.IntLiteral:
                    result = PDSLValue.Integer(int.Parse(this.currentToken.Lexeme));
                    break;
                case TokenTypeEnum.FloatLiteral:
                    result = PDSLValue.Float(float.Parse(this.currentToken.Lexeme));
                    break;
                case TokenTypeEnum.BoolLiteral:
                    result = PDSLValue.Bool(this.currentToken.Lexeme == "true");
                    break;
                case TokenTypeEnum.StringLiteral:
                    result = PDSLValue.String(this.currentToken.Lexeme);
                    break;
                case TokenTypeEnum.NullLiteral:
                    result = PDSLValue.Null;
                    break;
                default:
                    throw new PLParserException("Invalid value type: " + Token.TokenTypeToString(this.currentToken.TokenType), 0);
            }

            this.advance();
            return new ConstantExpression(result);
        }

        private CallExpression parseCallExpression()
        {
            // identifier
            string name = this.expect(TokenTypeEnum.Identifier).Lexeme;

            // parse argument list:
            // (
            this.expect(TokenTypeEnum.LParen);

            List<AExpression> args = new List<AExpression>();

            if (this.currentToken.TokenType != TokenTypeEnum.RParen)
            {
                while (true)
                {
                    args.Add(this.parseExpression());
                    if (this.currentToken.TokenType == TokenTypeEnum.Comma)
                    {
                        this.advance();
                        continue;
                    }
                    break;
                }
            }
            // )

            this.expect(TokenTypeEnum.RParen);
            return new CallExpression(name, args);
        }

        private TupleExpression parseTupleExpression()
        {
            // (
            this.expect(TokenTypeEnum.LParen);

            List<AExpression> args = new List<AExpression>();

            if (this.currentToken.TokenType != TokenTypeEnum.RParen)
            {
                while (true)
                {
                    args.Add(this.parseExpression());
                    if (this.currentToken.TokenType == TokenTypeEnum.Comma)
                    {
                        this.advance();
                        continue;
                    }
                    break;
                }
            }
            // )

            this.expect(TokenTypeEnum.RParen);
            return new TupleExpression(args);
        }
        private ListExpression parseListExpression()
        {
            // [
            this.expect(TokenTypeEnum.LBrack);

            List<AExpression> args = new List<AExpression>(); 

            if (this.currentToken.TokenType != TokenTypeEnum.RBrack)
            {
                while (true)
                {
                    args.Add(this.parseExpression());
                    if (this.currentToken.TokenType == TokenTypeEnum.Comma)
                    {
                        this.advance();
                        continue;
                    }
                    break;
                }
            }
            // ]

            this.expect(TokenTypeEnum.RBrack);
            return new ListExpression(args);
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
                throw new PLParserException("Unexpected Token: " + Token.TokenTypeToString(this.currentToken.TokenType) + " (expected " + Token.TokenTypeToString(tokenType) + ")", 0);
            }

            Token result = this.currentToken;
            this.advance();

            return result;
        }
    }
}
