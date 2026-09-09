namespace Planeted
{
    public class ReturnStatement : AStatement
    {
        private AExpression expression;

        public ReturnStatement(AExpression expression)
        {
            this.expression = expression;
        }

        public override void Execute(PDSLRuntime runtime)
        {
            runtime.Result = this.expression.Eval(runtime);
        }
    }
}