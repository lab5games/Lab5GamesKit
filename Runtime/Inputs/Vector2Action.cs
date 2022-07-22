using UnityEngine;

namespace Lab5Games
{
    [System.Serializable]
    public struct Vector2Action
    {
        public Vector2 value;

        public static implicit operator Vector2(Vector2Action action) => action.value;

        public void Reset()
        {
            value = Vector2.zero;
        }
    }
}
