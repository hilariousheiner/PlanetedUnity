using System.IO;

using System.Collections.Generic;

namespace Planeted
{
    public class PDSLRuntime
    {
        private Dictionary<string, PDSLValue> environment;
        private Dictionary<string, PDSL.BuiltinFunctionDelegate> builtinFunctions;

        public PDSLValue Result;
        public bool DebugFlag;

        public ISourceFileReader SourceFileReader;
        public ILogger Logger;

        public string OutPath;
        public Stack<string> FileStack;

        public PDSLRuntime(ISourceFileReader sourceFileReader, ILogger logger)
        {
            this.environment = new Dictionary<string, PDSLValue>();
            this.builtinFunctions = new Dictionary<string, PDSL.BuiltinFunctionDelegate>();

            this.DebugFlag = false;

            this.SourceFileReader = sourceFileReader;
            this.Logger = logger;

            this.OutPath = "./";
            this.FileStack = new Stack<string>();

            this.InstallBuiltinFunction("setDebugFlag", PDSLRuntime.builtin_setDebugFlag);
            this.InstallBuiltinFunction("log", PDSLRuntime.builtin_log);
            this.InstallBuiltinFunction("load", PDSLRuntime.builtin_load);
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
            if (!this.environment.ContainsKey(name))
            {
                throw new PLRuntimeException("Undefined variable: " + name, 0);
            }
            return this.environment[name];
        }

        public void InstallBuiltinFunction(string name, PDSL.BuiltinFunctionDelegate function)
        {
            if (this.builtinFunctions.ContainsKey(name))
            {
                this.builtinFunctions[name] = function;
            }
            else
            {
                this.builtinFunctions.Add(name, function);
            }
        }

        public PDSLValue CallFunction(string name, List<PDSLValue> args)
        {
            if (!this.builtinFunctions.ContainsKey(name))
            {
                throw new PLRuntimeException("Undefined function: " + name, 0);
            }
            return this.builtinFunctions[name](this, args);
        }

        public void DumpEnvironment()
        {
            this.Logger.Log(LogLevelEnum.Message, "environment: \n");
            foreach(KeyValuePair<string, PDSLValue> entry in this.environment)
            {
                this.Logger.Log(LogLevelEnum.Message, entry.Key + "\n");
            }
        }

        public string ResolvePath(string filename)
        {
            string current = string.Empty;

            if (this.FileStack.Count > 0)
            {
                current = FileStack.Pop();
            }

            // If no current file, treat as working directory case
            if (current == string.Empty)
            {
                return filename;
            }

            // strip file name keep directory
            string dir = new FileInfo(current).Directory.FullName;

            return Path.GetFullPath(dir + filename);
        }

        private static PDSLValue builtin_setDebugFlag(PDSLRuntime runtime, List<PDSLValue> args)
        {
            if (args.Count != 1)
            {
                throw new PLRuntimeException("setDebugFlag expects one argument.", 0);
            }
            runtime.DebugFlag = (bool)args[0].Data;
            return PDSLValue.Null;
        }

        private static PDSLValue builtin_log(PDSLRuntime runtime, List<PDSLValue> args)
        {
            /*
            if(args.Count != 1)
            {
                throw new PLRuntimeException("log expects exactly one argument.", 0);
            }
            */
            PDSLUtils.ExpectArgsCount(args, 1, "log");
            runtime.Logger.Log(LogLevelEnum.Message, (string)args[0].Data);
            return PDSLValue.Null;
        }

        private static PDSLValue builtin_load(PDSLRuntime runtime, List<PDSLValue> args)
        {
            if (args.Count != 1)
            {
                throw new PLRuntimeException("load expects exactly one argument.", 0);
            }
            return PDSL.Load(args[0].ToString(), runtime);
        }
    }
}