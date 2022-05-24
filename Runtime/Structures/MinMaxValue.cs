
namespace Lab5Games
{
    [System.Serializable]
    public struct MinMaxValue 
    {
        public float min;
        public float max;

        public MinMaxValue(float min, float max)
        {
            this.min = min;
            this.max = max;
        }
    }
}
