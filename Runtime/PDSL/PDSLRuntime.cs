using System.Collections.Generic;

namespace Planeted
{
    public class PDSLRuntime
    {
        private Dictionary<string, PDSLValue> environment;

        public PDSLValue Result;

        public ISourceFileReader SourceFileReader;

        public PDSLRuntime(ISourceFileReader sourceFileReader) 
        {
            this.SourceFileReader = sourceFileReader;
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

        public PDSLValue GetVariableValue(string name)
        {
            if(!this.environment.ContainsKey(name))
            {
                throw new PLRuntimeException("Undefined variable: " + name, 0);
            }
            return this.environment[name];
        }

        public PDSLValue CallFunction(string name, List<PDSLValue> args)
        {
            return PDSLValue.Null;
        }
    }
}
