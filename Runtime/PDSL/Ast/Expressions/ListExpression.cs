using System.Collections.Generic;

namespace Planeted
{
    public class ListExpression : AExpression
    {
        private List<AExpression> expressionsList;

        public ListExpression(List<AExpression> expressionsList)
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
            return PDSLValue.List(new PLList(elements));
        }
    }
}