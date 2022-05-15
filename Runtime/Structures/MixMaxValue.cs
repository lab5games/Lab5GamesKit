
namespace Lab5Games
{
    [System.Serializable]
    public struct MixMaxValue 
    {
        public float min;
        public float max;

        public MixMaxValue(float min, float max)
        {
            this.min = min;
            this.max = max;
        }
    }
}
