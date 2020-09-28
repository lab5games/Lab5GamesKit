using UnityEngine;

namespace Lab5Games
{
    public interface IRandomNumberGenerator
    {
        void Init(int seed);
        float Next();
    }

    class URNG : IRandomNumberGenerator
    {
        public void Init(int seed)
        {
            UnityEngine.Random.InitState(seed);
        }

        public float Next()
        {
            return UnityEngine.Random.value;
        }
    }

    public static class RandomEx
    {
        public static IRandomNumberGenerator RNG { get; set; }

        static RandomEx()
        {
            RNG = new URNG();
        }

        public static float Next()
        {
            return RNG.Next();
        }

        public static float Range(float min, float max)
        {
            if (min >= max)
                return min;

            return (Next() * (max - min) + min);
        }

        public static int Range(int min, int max)
        {
            if (min >= max)
                return min;

            return (int)(Next() * (max - min) + min);
        }
    }
}
