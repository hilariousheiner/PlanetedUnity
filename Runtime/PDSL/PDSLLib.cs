using System.Collections.Generic;

namespace Planeted
{
    public static class PDSLLib
    {
        public static void Load(PDSLRuntime runtime)
        {
            runtime.InstallBuiltinFunction("noise", PDSLLib.builtin_noise);
        }

        private static PDSLValue builtin_noise(PDSLRuntime runtime, List<PDSLValue> args)
        {
            PDSLUtils.ExpectArgsCount(args, 0, "noise");

            Noise noise = new Noise();

            return PDSLValue.Noise(noise);
        }
    }
}