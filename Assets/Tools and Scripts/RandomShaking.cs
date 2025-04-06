using UnityEngine;
using Random = UnityEngine.Random;

public class RandomShaking : MonoBehaviour
{
    [SerializeField] private float shakePower;
    private float lastShakeTimestamp;

    private Vector2 startPosition;
    
    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        transform.position = startPosition + Random.insideUnitCircle.normalized * shakePower;
    }
}
