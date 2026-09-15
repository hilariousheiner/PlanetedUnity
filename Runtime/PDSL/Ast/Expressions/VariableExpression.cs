namespace Planeted
{
    public class VariableExpression : AExpression
    {
        private string identifier; 

        public VariableExpression(string identifier)
        {
            this.identifier = identifier;
        }

        public override PDSLValue Eval(PDSLRuntime runtime)
        {
            return runtime.GetVariableValue(this.identifier);
        }
    }
}