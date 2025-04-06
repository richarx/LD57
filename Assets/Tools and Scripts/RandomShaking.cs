using UnityEngine;
using Random = UnityEngine.Random;

public class RandomShaking : MonoBehaviour
{
    [SerializeField] public float shakePower;
    [SerializeField] private bool useLocalPosition;

    private float lastShakeTimestamp;

    private Vector2 startPosition;
    
    private void Start()
    {
        startPosition = useLocalPosition ? transform.localPosition : transform.position;
    }

    private void Update()
    {
        if (useLocalPosition)
            transform.localPosition = startPosition + Random.insideUnitCircle.normalized * shakePower;
        else
            transform.position = startPosition + Random.insideUnitCircle.normalized * shakePower;
    }
}
