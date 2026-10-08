using UnityEngine;

namespace Planeted
{
    public static class NoiseFunctions
    {
        public delegate float NoiseFunction1D(float p, NoiseParameters parameters);

        public static float FBM1D(float p, FBMParameters fbmParams, NoiseParameters noiseParams, NoiseFunctions.NoiseFunction1D noiseFun)
        {
            float result = 0.0f;

            float frequency = fbmParams.StartFrequency;
            float amplitude = 1.0f;

            float t = 0.0f;

            for(int i = 0; i < fbmParams.NumberOfOctaves; i++)
            {
                result += amplitude* noiseFun(p* frequency, noiseParams);

                t += amplitude;
                frequency *= fbmParams.Lacunarity;
                amplitude *= fbmParams.Persistence;
            }

            if(fbmParams.Normalize)
            {
                result = result / t;
            }
            return Mathf.Pow(result, fbmParams.Exponent);
        }
    }
}