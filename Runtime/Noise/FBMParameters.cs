namespace Planeted
{
    public class FBMParameters
    {
        public int NumberOfOctaves;
        public float Lacunarity;
        public float Persistence;
        public float StartFrequency;
        public bool Normalize;
        public float Exponent;
        
        FBMParameters()
        {
            this.NumberOfOctaves = 1;
            this.Lacunarity = 2.0f;
            this.Persistence = 0.5f;
            this.StartFrequency = 1.0f;
            this.Normalize = false;
            this.Exponent = 1.0f;
        }
    }
}
