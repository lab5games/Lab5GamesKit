using UnityEngine;

namespace Lab5Games
{
    [System.Serializable]
    public struct FloatAction
    {
        public float value;

        public static implicit operator float(FloatAction action) => action.value;

        public void Reset()
        {
            value = 0f;
        }
    }
}
