using System.Collections.Generic;

namespace Planeted
{
    public class PLList
    {
        public List<PDSLValue> Elements;

        public PLList(List<PDSLValue> elements)
        {
            this.Elements = elements;
        }

        public override string ToString()
        {
            return "[" + PLUtils.ListToString(this.Elements, (PDSLValue val) => val.ToString(), ',') + "]";
        }
    }
}