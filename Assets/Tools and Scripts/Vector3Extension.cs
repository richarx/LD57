using UnityEngine;

public static class Vector3Extension
{
    public static Vector2 ToVector2(this Vector3 vec3) => new Vector2(vec3.x, vec3.y);
}