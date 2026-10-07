namespace Planeted
{
    public class Noise
    {
        private NoiseParameters noiseParameters;
        public NoiseParameters NoiseParameters => this.noiseParameters;
        
        public Noise()
        {
            this.noiseParameters = new NoiseParameters();
        }
    }
}