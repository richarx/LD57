using UnityEngine;

public static class Vector2Extension
{
    public static Vector3 ToVector3(this Vector2 vec2, float z = 0f) => new(vec2.x, vec2.y, z);

    public static Vector2 SetMagnitude(this Vector2 vec2, float newMagnitude) => vec2.normalized * Mathf.Max(0, newMagnitude);

    public static Vector2 Rotate(this Vector2 v, float degrees)
    {
        float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
        float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);

        float tx = v.x;
        float ty = v.y;
        v.x = (cos * tx) - (sin * ty);
        v.y = (sin * tx) + (cos * ty);
        return v;
    }

    public static Vector2 PerpendicularClockwise(this Vector2 v) => new(v.y, -v.x);

    public static Vector2 PerpendicularCounterClockwise(this Vector2 v) => new(-v.y, v.x);
}