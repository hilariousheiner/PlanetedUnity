namespace Planeted
{
    public abstract class AExpression
    {
        public abstract PDSLValue Eval(PDSLRuntime runtime);
    }
}