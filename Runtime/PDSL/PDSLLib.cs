using System.Collections.Generic;

namespace Planeted
{
    public static class PDSLLib
    {
        public static void Load(PDSLRuntime runtime)
        {
            runtime.InstallBuiltinFunction("noise", PDSLLib.builtin_noise);
            runtime.InstallBuiltinFunction("seedNoise", PDSLLib.builtin_seedNoise);
        }

        private static PDSLValue builtin_noise(PDSLRuntime runtime, List<PDSLValue> args)
        {
            PDSLUtils.ExpectArgsCount(args, 0, "noise");

            Noise noise = new Noise();

            return PDSLValue.Noise(noise);
        }

        private static PDSLValue builtin_seedNoise(PDSLRuntime runtime, List<PDSLValue> args)
        {
            PDSLUtils.ExpectArgsCount(args, 2, "seedNoise");

            Noise noise = PDSLUtils.GetNoiseArg(args, 0, "seedNoise");

            if(args[1].Type == ValueTypeEnum.Int)
            {
                int seed = args[1].GetIntValue();
                noise.NoiseParameters.SeedNoise((uint)seed);
            }
            else if(args[1].Type == ValueTypeEnum.String)
            {
                string seed = args[1].GetStringValue();
                noise.NoiseParameters.SeedNoise(seed);
            }
            else
            {
                throw new PLRuntimeException("seed must be an integer or a string.", 0);
            }
            return PDSLValue.Null;
        }
    }
}