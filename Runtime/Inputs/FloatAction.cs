using UnityEngine;

namespace Lab5Games
{
    [System.Serializable]
    public struct FloatAction
    {
        public float value;

        public void Reset()
        {
            value = 0f;
        }
    }
}
