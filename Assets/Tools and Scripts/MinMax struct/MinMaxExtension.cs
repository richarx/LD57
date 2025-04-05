using UnityEngine;

public static class MinMaxExtension
{
    /// <summary>
    /// Return true if val is between min [inclusive] and max [inclusive]
    /// </summary>
    public static bool Include(this MinMax minMax, float val)
    {
        if (minMax.min < minMax.max)
            return (val >= minMax.min && val <= minMax.max);
        return (val >= minMax.max && val <= minMax.min);
    }

    /// <summary>
    /// Return a random float number between min [inclusive] and max [inclusive]
    /// </summary>
    public static float RandomRange(this MinMax minMax)
    {
        return Random.Range(minMax.min, minMax.max);
    }

    /// <summary>
    /// Clamp the given value between min [inclusive] and max [inclusive]
    /// </summary>
    public static float Clamp(this MinMax minMax, float val)
    {
        return Mathf.Clamp(val, minMax.min, minMax.max);
    }

    /// <summary>
    /// Clamp the given int value between min [inclusive] and max [inclusive]
    /// </summary>
    public static int Clamp(this MinMax minMax, int val)
    {
        return Mathf.Clamp(val, (int)minMax.min, (int)minMax.max);
    }

    /// <summary>
    /// Linearly interpolate between min and max
    /// </summary>
    public static float Lerp(this MinMax minMax, float t)
    {
        return Mathf.Lerp(minMax.min, minMax.max, t);
    }

    /// <summary>
    /// Calculates the linear parameter t that produce the interpolant value within the range [min, max]
    /// </summary>
    public static float InverseLerp(this MinMax minMax, float value)
    {
        return Mathf.InverseLerp(minMax.min, minMax.max, value);
    }

    /// <summary>
    /// Return the minMax struct with a modified Min value
    /// </summary>
    public static MinMax Min(this MinMax minMax, float value)
    {
        return new MinMax(value, minMax.max, minMax.doNotEnforceMinInferiorToMax);
    }

    /// <summary>
    /// Return the minMax struct with a modified Max value
    /// </summary>
    public static MinMax Max(this MinMax minMax, float value)
    {
        return new MinMax(minMax.min, value, minMax.doNotEnforceMinInferiorToMax);
    }
}