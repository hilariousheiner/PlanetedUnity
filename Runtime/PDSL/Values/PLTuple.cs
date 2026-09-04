using System.Collections.Generic;

namespace Planeted
{
    public class PLTuple
    {
        public List<PDSLValue> Elements;

        public PLTuple(List<PDSLValue> elements)
        {
            this.Elements = elements;
        }

        public override string ToString()
        {
            return "(" + PLUtils.ListToString(this.Elements, (PDSLValue val) =>  val.ToString(), ',') + ")";
        }
    }
}