using UnityEngine;

namespace Lab5Games
{
    public static class RandomEx
    {
        public static void SetSeed(int seed)
        {
            Random.InitState(seed);
        }

        public static float Next()
        {
            return Random.value;
        }

        public static float Next(float min, float max)
        {
            return Random.Range(min, max);
        }

        public static int Next(int min, int max)
        {
            return Random.Range(min, max);
        }

        public static float NextExponent(float lamda)
        {
            float y = Next();
            float x = -Mathf.Log(1 - y) / lamda;
            return x;
        }

        public static float NextGaussian(float mean, float dev)
        {
            return mean + NextGaussian() * dev;
        }

        public static float NextGaussian(float mean, float dev, float min, float max)
        {
            float x;
            do
            {
                x = NextGaussian(mean, dev);
            } while (x < min || x > max);

            return x;
        }

        static float NextGaussian()
        {
            float v1, v2, s;
            do
            {
                v1 = 2.0f * Random.Range(0f, 1f) - 1.0f;
                v2 = 2.0f * Random.Range(0f, 1f) - 1.0f;
                s = v1 * v1 + v2 * v2;
            } while (s >= 1.0f || s == 0f);

            s = Mathf.Sqrt((-2.0f * Mathf.Log(s)) / s);

            return v1 * s;
        }
    }
}
