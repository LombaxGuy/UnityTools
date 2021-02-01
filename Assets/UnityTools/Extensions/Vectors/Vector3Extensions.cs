using UnityEngine;

namespace UnityTools.Extensions
{
    public static class Vector3Extensions
    {
        /// <summary>
        /// Returns a random normalized Vector3.
        /// </summary>
        public static Vector3 RandomDirection(this Vector3 vector)
        {
            vector.x = Random.Range(-1f, 1f);
            vector.y = Random.Range(-1f, 1f);
            vector.z = Random.Range(-1f, 1f);

            vector.Normalize();

            return vector;
        }

        #region ToVector2
        public static Vector2 ToVector2XY(this Vector3 v)
        {
            return new Vector2(v.x, v.y);
        }

        public static Vector2 ToVector2XZ(this Vector3 v)
        {
            return new Vector2(v.x, v.z);
        }

        public static Vector2 ToVector2YZ(this Vector3 v)
        {
            return new Vector2(v.y, v.z);
        }
        #endregion
    }


}