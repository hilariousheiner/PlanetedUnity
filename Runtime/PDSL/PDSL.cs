using System.Collections.Generic;
using UnityEngine;

namespace Planeted
{
    public static class PDSL
    {
        public delegate PDSLValue BuiltinFunctionDelegate(PDSLRuntime runtime, List<PDSLValue> args);

        public static void RunFile(string path, PDSLRuntime runtime)
        {
            if (runtime.SourceFileReader.TryReadSourceFile(path, out string code))
            {
                Debug.Log(code);
                PDSL.Run(code, runtime);
            }
        }

        public static void Run(string code, PDSLRuntime runtime)
        {
            Lexer lexer = new Lexer(code);
            PLParser parser = new PLParser(lexer);
            PDSLProgram program = parser.Parse();
            program.Run(runtime);
        }
    }
}