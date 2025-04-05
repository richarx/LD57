using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class PowerController : MonoBehaviour
{
    public float speedStart;
    public float speedEnd;

    private Coroutine slidingRoutine;
    private Slider slider;

    private bool isSliding;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            StartSliding();

        if (isSliding && Input.GetKeyUp(KeyCode.Space))
            StopSliding();
    }

    private void StartSliding()
    {
        isSliding = true;
        slidingRoutine = StartCoroutine(DoSliding());
    }

    private void StopSliding()
    {
        isSliding = false;
        StopCoroutine(slidingRoutine);
    }

    private IEnumerator DoSliding()
    {
        while (slider.value < slider.maxValue)
        {
            slider.value += Time.deltaTime * Mathf.Lerp(speedStart, speedEnd, slider.value);
            yield return null;
        }

        StopSliding();
    }
}
