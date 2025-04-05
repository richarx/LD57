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
    private Shaker2D shaker2D;

    private bool isSliding;

    private void Awake()
    {
        slider = GetComponent<Slider>();
        shaker2D = GetComponent<Shaker2D>();
    }

    private void Update()
    {
        if (!isSliding && Input.GetKeyDown(KeyCode.Space))
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

        if (slidingRoutine != null)
            StopCoroutine(slidingRoutine);
    }

    private IEnumerator DoSliding()
    {
        while (slider.value < slider.maxValue)
        {
            slider.value += Time.deltaTime * Mathf.Lerp(speedStart, speedEnd, slider.value);
            shaker2D.SetTrauma(slider.value);
            yield return null;
        }

        StopSliding();
    }
}
