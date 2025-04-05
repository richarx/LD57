using UnityEngine;

[System.Serializable]
public struct MinMax
{
    [SerializeField] float _min;
    public float min { get { return _min; } set { _min = value; if (!doNotEnforceMinInferiorToMax && _min > max) _max = min; } }

    [SerializeField] float _max;
    public float max { get { return _max; } set { _max = value; if (!doNotEnforceMinInferiorToMax && _max < min) _min = max; } }

    [SerializeField] public bool doNotEnforceMinInferiorToMax;

    public MinMax(float min, float max, bool doNotEnforceMinInferiorToMax = false)
    {
        _min = min;
        _max = max;
        this.doNotEnforceMinInferiorToMax = doNotEnforceMinInferiorToMax;
    }
}
