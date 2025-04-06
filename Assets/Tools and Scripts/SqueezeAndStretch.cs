using System;
using System.Collections;
using UnityEngine;

public class SqueezeAndStretch : MonoBehaviour
{
    [SerializeField] [Range(0.1f, 2.0f)]
    private float xSqueeze;
    [SerializeField] [Range(0.1f, 2.0f)]
    private float ySqueeze;
    [SerializeField] [Range(0.1f, 2.0f)]
    private float duration;
    [SerializeField] [Range(0.0f, 2.0f)]
    private float delay;
    [SerializeField] private Transform GraphicsObject;
    [SerializeField] private bool triggerOnStart;
    
    private Coroutine SqueezeRoutine;
    Vector3 originalSize = Vector3.one;

    private void Awake()
    {
        originalSize = GraphicsObject.transform.localScale;
    }

    private void Start()
    {
        if (triggerOnStart)
            Trigger();
    }

    public void Trigger(bool deactivateOnFinish = false)
    {
        if (SqueezeRoutine != null)
            StopCoroutine(SqueezeRoutine);

        if (gameObject.activeSelf)
            SqueezeRoutine = StartCoroutine(Squeeze(deactivateOnFinish));
    }

    private IEnumerator Squeeze(bool deactivateOnFinish)
    {
        yield return new WaitForSeconds(delay);
        
        Vector3 newSize = new Vector3(xSqueeze * originalSize.x, ySqueeze * originalSize.y, originalSize.z);

        float t = 0f;
        while (t <= 1.0)
        {
            t += Time.deltaTime / duration;
            GraphicsObject.localScale = Vector3.Lerp(newSize, originalSize, t);
            yield return null;
        }

        GraphicsObject.localScale = originalSize;
        if (deactivateOnFinish)
            GraphicsObject.gameObject.SetActive(false);
    }
}
