using UnityEngine;

namespace Planeted
{
    public static class NoiseFunctions
    {
        public delegate float NoiseFunction1D(float p, NoiseParameters parameters);
        public delegate NoiseFunction1D NoiseTransform1D(NoiseFunction1D noiseFunction);
        
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

        public static NoiseFunction1D Billow1D(NoiseFunctions.NoiseFunction1D noiseFun)
        {
            return (float p, NoiseParameters parameters) =>
            {
                return Mathf.Abs(noiseFun(p, parameters));
            };
        }

        public static NoiseFunction1D Ridge1D(NoiseFunctions.NoiseFunction1D noiseFun)
        {
            return (float p, NoiseParameters parameters) =>
            {
                float n = (0.9f - Mathf.Abs(noiseFun(p, parameters)));
                return n * n;
            };
        }
        public static NoiseTransform1D GetNoiseStyleTransform1D(NoiseStyleEnum noiseStyle)
        {
            NoiseTransform1D result = null;

            switch (noiseStyle)
            {
                case NoiseStyleEnum.Plain:
                    result = (NoiseFunctions.NoiseFunction1D noiseFun) => { return noiseFun; };  
                    break;
                case NoiseStyleEnum.Billow:
                    result = NoiseFunctions.Billow1D;
                    break;
                case NoiseStyleEnum.Ridge:
                    result = NoiseFunctions.Ridge1D;
                    break;
                default:
                    break;
            }
            return result;
        }
    }
}