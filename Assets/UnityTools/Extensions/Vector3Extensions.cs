using UnityEngine;

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
}