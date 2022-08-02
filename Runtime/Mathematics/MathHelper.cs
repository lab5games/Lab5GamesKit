using System;
using UnityEngine;

namespace Lab5Games
{
    public static class MathHelper
    {
        public static float RoundToNearest(float value, float roundToNearest)
        {
            return Mathf.Round(value / roundToNearest) * roundToNearest;
        }
    }
}
