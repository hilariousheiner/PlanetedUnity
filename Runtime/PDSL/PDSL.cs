using UnityEngine;

namespace Planeted
{
    public class PDSL
    {
        public static void Run(string code, PDSLRuntime runtime)
        {
            Lexer lexer = new Lexer(code);
            PLParser parser = new PLParser(lexer);
            PDSLProgram program = parser.Parse();
            program.Run(runtime);
        }
    }
}