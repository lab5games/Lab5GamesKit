using UnityEngine;

namespace Lab5Games.Mathematics
{
    public static class BezierCurve
    {
        public static Vector3 Linear(Vector3 p0, Vector3 p1, float t)
        {
            return p0 + t * (p1 - p0);
        }

        public static Vector3 Quadratic(Vector3 p0, Vector3 p1, Vector3 p2, float t)
        {
            float u = 1 - t;
            float tt = t * t;
            float uu = u * u;

            return (uu * p0) + (2 * u * t * p1) + (tt * p2);
        }

        public static Vector3 Cubic(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
        {
            float u = 1 - t;
            float tt = t * t;
            float uu = u * u;
            float ttt = t * t * t;
            float uuu = u * u * u;

            return (uuu * p0) + (3 * uu * t * p1) + (3 * u * tt * p2) + (ttt * p3);
        }
    }
}
