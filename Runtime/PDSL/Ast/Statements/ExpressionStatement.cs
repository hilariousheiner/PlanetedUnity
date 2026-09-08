namespace Planeted
{
    public class ExpressionStatement : AStatement
    {
        private AExpression expression;

        public ExpressionStatement(AExpression expression)
        {
            this.expression = expression;
        }

        public override void Execute(PDSLRuntime runtime)
        {
            this.expression.Eval(runtime);
        }
    }
}