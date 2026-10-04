using System.Collections.Generic;

namespace Planeted
{
    public static class PDSL
    {
        public delegate PDSLValue BuiltinFunctionDelegate(PDSLRuntime runtime, List<PDSLValue> args);

        public static void RunFile(string path, PDSLRuntime runtime)
        {
            string resolved = runtime.ResolvePath(path);

            if (runtime.SourceFileReader.TryReadSourceFile(resolved, out string code))
            {

                runtime.FileStack.Push(path);
                PDSL.Run(code, runtime);

                if (runtime.DebugFlag)
                {
                    runtime.DumpEnvironment();
                }
                runtime .FileStack.Pop();
            }
        }

        public static void Run(string code, PDSLRuntime runtime)
        {
            Lexer lexer = new Lexer(code);
            PLParser parser = new PLParser(lexer);
            PDSLProgram program = parser.Parse();
            program.Run(runtime);
        }

        public static PDSLValue Load(string path, PDSLRuntime runtime)
        {
            PDSLRuntime new_runtime = new PDSLRuntime(runtime.SourceFileReader, runtime.Logger);

            PDSLLib.Load(new_runtime);

            new_runtime.OutPath = runtime.OutPath;

            PDSL.RunFile(runtime.ResolvePath(path), new_runtime);

            return new_runtime.Result;
        }
    }
}