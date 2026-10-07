using UnityEngine; 

namespace Planeted
{
    public static class WhiteNoise
    {
        public static float WhiteNoise1D(float p, NoiseParameters noiseParameters)
        {
            int pi = (int)Mathf.Floor(p * noiseParameters.WhiteNoiseScale);

            uint h = PRNG.Hash1D(pi, noiseParameters.Seed);

            return PRNG.HashToSigned(h);
        }
    }
}