using UnityEngine;

namespace UnityTools.Extensions
{
    public static class Vector2Extensions
    {
        #region To Vector3
        public static Vector3 ToVector3XY(this Vector2 v)
        {
            return new Vector3(v.x, v.y, 0);
        }

        public static Vector3 ToVector3XZ(this Vector2 v)
        {
            return new Vector3(v.x, 0, v.y);
        }

        public static Vector3 ToVector3YZ(this Vector2 v)
        {
            return new Vector3(0, v.x, v.y);
        }
        #endregion
    }
}