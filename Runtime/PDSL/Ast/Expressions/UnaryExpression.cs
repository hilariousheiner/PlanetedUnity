namespace Planeted
{
    public class UnaryExpression : AExpression
    {
        private TokenTypeEnum op;
        private AExpression operand;

        public UnaryExpression(TokenTypeEnum op, AExpression operand)
        {
            this.op = op;
            this.operand = operand;
        }

        public override PDSLValue Eval(PDSLRuntime runtime)
        {
            PDSLValue result = this.operand.Eval(runtime);

            switch (this.op)
            {
                case TokenTypeEnum.Minus:
                    result = PDSLUtils.Negate(result);
                    break;
                default:
                    throw new PLRuntimeException("unknown unary operator", 0);
            }
            return result;
        }
    }
}