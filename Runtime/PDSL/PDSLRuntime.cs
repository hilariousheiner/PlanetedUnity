using System.Collections.Generic;

namespace Planeted
{
    public class PDSLRuntime
    {
        private Dictionary<string, PDSLValue> environment;

        public PDSLValue Result;

        public PDSLRuntime() 
        {
            this.environment = new Dictionary<string, PDSLValue>();
        }

        public void SetVariableValue(string name, PDSLValue value)
        {
            if (!this.environment.ContainsKey(name))
            {
                this.environment.Add(name, value);
            }
            else
            {
                this.environment[name] = value;
            }
        }
    }
}
