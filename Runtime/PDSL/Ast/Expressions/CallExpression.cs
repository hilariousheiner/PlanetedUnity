using System.Collections.Generic; 

namespace Planeted
{
    public class CallExpression : AExpression
    {
        private string name;
        private List<AExpression> args;

        public CallExpression(string name, List<AExpression> args)
        {
            this.name = name;
            this.args = args;
        }

        public override PDSLValue Eval(PDSLRuntime runtime)
        {
            List<PDSLValue> evaluatedArgs = new List<PDSLValue>();

            foreach (AExpression arg in this.args)
            {
                evaluatedArgs.Add(arg.Eval(runtime));
            }
            return runtime.CallFunction(this.name, evaluatedArgs);
        }
    }
}
