using System;

namespace Planeted
{
    public class NoiseParameters
    {
        public float WhiteNoiseScale;
        public uint Seed;

        public NoiseParameters() 
        {
            this.WhiteNoiseScale = 100.0f;
            this.SeedNoise("Planeted");
        }

        public void SeedNoise(uint seed)
        {
            this.Seed = seed;
        }
        public void SeedNoise(string seed)
        {
            this.Seed = PRNG.StringToSeed32(seed);
        }
    }
}
