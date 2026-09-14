namespace Planeted
{
    public class ConstantExpression : AExpression
    {
        private PDSLValue value;

        public ConstantExpression(PDSLValue value)
        {
            this.value = value;
        }

        public override PDSLValue Eval(PDSLRuntime runtime)
        {
            return this.value;
        }
    }
}
