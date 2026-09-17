using System.Collections.Generic;

namespace Planeted
{
    public class TupleExpression : AExpression
    {
        private List<AExpression> expressionsList;

        public TupleExpression(List<AExpression> expressionsList)
        {
            this.expressionsList = expressionsList;
        }

        public override PDSLValue Eval(PDSLRuntime runtime)
        {
            List<PDSLValue> elements = new List<PDSLValue>(); 

            foreach (AExpression expression in this.expressionsList)
            {
                elements.Add(expression.Eval(runtime));
            }
            return PDSLValue.Tuple(new PLTuple(elements));
        }
    }
}