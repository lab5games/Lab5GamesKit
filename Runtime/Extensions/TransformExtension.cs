using UnityEngine;

namespace Lab5Games
{
    public static class TransformExtension
    {
        public static void SetPositionX(this Transform transform, float x)
        {
            Vector3 p = transform.position;
            transform.position = new Vector3(x, p.y, p.z);
        }

        public static void SetPositionY(this Transform transform, float y)
        {
            Vector3 p = transform.position;
            transform.position = new Vector3(p.x, y, p.z);
        }

        public static void SetPositionZ(this Transform transform, float z)
        {
            Vector3 p = transform.position;
            transform.position = new Vector3(p.x, p.y, z);
        }

        public static void SetPositionXY(this Transform transform, float x, float y)
        {
            Vector3 p = transform.position;
            transform.position = new Vector3(x, y, p.z);
        }

        public static void SetPositionXZ(this Transform transform, float x, float z)
        {
            Vector3 p = transform.position;
            transform.position = new Vector3(x, p.y, z);
        }

        public static void SetPositionYZ(this Transform transform, float y, float z)
        {
            Vector3 p = transform.position;
            transform.position = new Vector3(p.x, y, z);
        }
    }
}
