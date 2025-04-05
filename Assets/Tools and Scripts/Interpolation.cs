using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Interpolation
{
    public static float Linear(float t) => Mathf.Clamp01(t);

    public static float SmoothStep(float t) => Mathf.SmoothStep(0, 1, Mathf.Clamp01(t));

    public static float EaseIn(float t, float factor) => Mathf.Pow(Mathf.Clamp01(t), factor);

    public static float EaseOut(float t, float factor) => 1 - Mathf.Pow(1 - Mathf.Clamp01(t), factor);
    
    public static float EaseInAndOut(float t, float factor)
    {
        t = Mathf.Clamp01(t);

        return Mathf.Lerp(Mathf.Pow(t, factor), 1 - (Mathf.Pow(1 - t, factor)), t);
    }
}
