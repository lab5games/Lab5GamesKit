using System;
using UnityEngine;

namespace Lab5Games.Lab5GamesKit
{
    public enum EEaseTypes
    {
        Linear          = 0,

        InSine          = 1,
        OutSine         = 2,
        InOutSine       = 3,

        InQuad          = 4,
        OutQuad         = 5,
        InOutQuad       = 6,

        InCubic         = 7,
        OutCubic        = 8,
        InOutCubic      = 9,

        InQuart         = 10,
        OutQuart        = 11,
        InOutQuart      = 12,

        InExpo          = 13,
        OutExpo         = 14,
        InOutExpo       = 15,

        InCirc          = 16,
        OutCirc         = 17,
        InOutCirc       = 18,

        InBack          = 19,
        OutBack         = 20,
        InOutBack       = 21,

        InElastic       = 22,
        OutElastic      = 23,
        InOutElastic    = 24,

        InBounce        = 25,
        OutBounce       = 26,
        InOutBounce     = 27
    }

    public static class Easing
    {
        public static Func<float, float> Function(EEaseTypes type)
        {
            return _funcs[(int)type];
        }

        private static Func<float, float>[] _funcs = new Func<float, float>[]
        {
            Linear,
            InSine, OutSine, InOutSine,
            InQuad, OutQuad, InOutQuad,
            InCubic, OutCubic, InOutCubic,
            InQuart, OutQuart, InOutQuart,
            InQuint, OutQuint, InOutQuint,
            InExpo, OutExpo, InOutExpo,
            InCirc, OutCirc, InOutCirc,
            InBack, OutBack, InOutBack,
            InElastic, OutElastic, InOutElastic,
            InBounce, OutBounce, InOutBounce
        };

        public static float Linear(float t)
        {
            return t;
        }

        public static float InSine(float t)
        {
            return Mathf.Sin(Mathf.PI * 0.5f * t);
        }

        public static float OutSine(float t)
        {
            return 1 + Mathf.Sin(Mathf.PI * 0.5f * (--t));
        }

        public static float InOutSine(float t)
        {
            return 0.5f * (1 + Mathf.Sin(Mathf.PI * (t - 0.5f)));
        }

        public static float InQuad(float t)
        {
            return t * t;
        }

        public static float OutQuad(float t)
        {
            return t * (2 - t);
        }

        public static float InOutQuad(float t)
        {
            return t < 0.5f ? 2 * t * t : t * (4 - 2 * t) - 1;
        }

        public static float InCubic(float t)
        {
            return t * t * t;
        }

        public static float OutCubic(float t)
        {
            return 1 + (--t) * t * t;
        }

        public static float InOutCubic(float t)
        {
            return t < 0.5 ? 4 * t * t * t : 1 + (--t) * (2 * (--t)) * (2 * t);
        }

        public static float InQuart(float t)
        {
            t *= t;
            return t * t;
        }

        public static float OutQuart(float t)
        {
            t = (--t) * t;
            return 1 - t * t;
        }

        public static float InOutQuart(float t)
        {
            if (t < 0.5)
            {
                t *= t;
                return 8 * t * t;
            }
            else
            {
                t = (--t) * t;
                return 1 - 8 * t * t;
            }
        }

        public static float InQuint(float t)
        {
            float t2 = t * t;
            return t * t2 * t2;
        }

        public static float OutQuint(float t)
        {
            float t2 = (--t) * t;
            return 1 + t * t2 * t2;
        }

        public static float InOutQuint(float t)
        {
            float t2;
            if (t < 0.5)
            {
                t2 = t * t;
                return 16 * t * t2 * t2;
            }
            else
            {
                t2 = (--t) * t;
                return 1 + 16 * t * t2 * t2;
            }
        }

        public static float InExpo(float t)
        {
            return (Mathf.Pow(2, 8 * t) - 1) / 255;
        }

        public static float OutExpo(float t)
        {
            return 1 - Mathf.Pow(2, -8 * t);
        }

        public static float InOutExpo(float t)
        {
            if (t < 0.5)
            {
                return (Mathf.Pow(2, 16 * t) - 1) / 510;
            }
            else
            {
                return 1 - 0.5f * Mathf.Pow(2, -16 * (t - 0.5f));
            }
        }

        public static float InCirc(float t)
        {
            return 1 - Mathf.Sqrt(1 - t);
        }

        public static float OutCirc(float t)
        {
            return Mathf.Sqrt(t);
        }

        public static float InOutCirc(float t)
        {
            if (t < 0.5)
            {
                return (1 - Mathf.Sqrt(1 - 2 * t)) * 0.5f;
            }
            else
            {
                return (1 + Mathf.Sqrt(2 * t - 1)) * 0.5f;
            }
        }

        public static float InBack(float t)
        {
            return t * t * (2.70158f * t - 1.70158f);
        }

        public static float OutBack(float t)
        {
            return 1 + (--t) * t * (2.70158f * t + 1.70158f);
        }

        public static float InOutBack(float t)
        {
            if (t < 0.5)
            {
                return t * t * (7 * t - 2.5f) * 2;
            }
            else
            {
                return 1 + (--t) * t * 2 * (7 * t + 2.5f);
            }
        }

        public static float InElastic(float t)
        {
            float t2 = t * t;
            return t2 * t2 * Mathf.Sin(t * Mathf.PI * 4.5f);
        }

        public static float OutElastic(float t)
        {
            float t2 = (t - 1) * (t - 1);
            return 1 - t2 * t2 * Mathf.Cos(t * Mathf.PI * 4.5f);
        }

        public static float InOutElastic(float t)
        {
            float t2;
            if (t < 0.45)
            {
                t2 = t * t;
                return 8 * t2 * t2 * Mathf.Sin(t * Mathf.PI * 9);
            }
            else if (t < 0.55)
            {
                return 0.5f + 0.75f * Mathf.Sin(t * Mathf.PI * 4);
            }
            else
            {
                t2 = (t - 1) * (t - 1);
                return 1 - 8 * t2 * t2 * Mathf.Sin(t * Mathf.PI * 9);
            }
        }

        public static float InBounce(float t)
        {
            return Mathf.Pow(2, 6 * (t - 1)) * Mathf.Abs(Mathf.Sin(t * Mathf.PI * 3.5f));
        }

        public static float OutBounce(float t)
        {
            return 1 - Mathf.Pow(2, -6 * t) * Mathf.Abs(Mathf.Cos(t * Mathf.PI * 3.5f));
        }

        public static float InOutBounce(float t)
        {
            if (t < 0.5)
            {
                return 8 * Mathf.Pow(2, 8 * (t - 1)) * Mathf.Abs(Mathf.Sin(t * Mathf.PI * 7));
            }
            else
            {
                return 1 - 8 * Mathf.Pow(2, -8 * t) * Mathf.Abs(Mathf.Sin(t * Mathf.PI * 7));
            }
        }
    }
}
