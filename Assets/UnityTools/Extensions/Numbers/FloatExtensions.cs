using UnityEngine;

namespace UnityTools.Extensions
{
    public static class FloatExtensions
    {
        #region To Vector3
        public static Vector3 ToVector3All(this float n)
        {
            return new Vector3(n, n, n);
        }

        public static Vector3 ToVector3XY(this float n)
        {
            return new Vector3(n, n, 0);
        }

        public static Vector3 ToVector3XZ(this float n)
        {
            return new Vector3(n, 0, n);
        }

        public static Vector3 ToVector3YZ(this float n)
        {
            return new Vector3(0, n, n);
        }

        public static Vector3 ToVector3X(this float n)
        {
            return new Vector3(n, 0, 0);
        }

        public static Vector3 ToVector3Y(this float n)
        {
            return new Vector3(0, n, 0);
        }

        public static Vector3 ToVector3Z(this float n)
        {
            return new Vector3(0, 0, n);
        }
        #endregion

        #region To Vector2
        public static Vector2 ToVector2All(this float n)
        {
            return new Vector2(n, n);
        }

        public static Vector2 ToVector2X(this float n)
        {
            return new Vector2(n, 0);
        }

        public static Vector2 ToVector2Y(this float n)
        {
            return new Vector2(0, n);
        }
        #endregion
    }
}