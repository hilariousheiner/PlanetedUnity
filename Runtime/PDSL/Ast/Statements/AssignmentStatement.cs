namespace Planeted
{
    public class AssignmentStatement : AStatement
    {
        private string name;
        private AExpression expression;

        public AssignmentStatement(string name, AExpression expression)
        {
            this.name = name;
            this.expression = expression;
        }

        public override void Execute(PDSLRuntime runtime)
        {
            runtime.SetVariableValue(this.name, this.expression.Eval(runtime));
        }
    }
}