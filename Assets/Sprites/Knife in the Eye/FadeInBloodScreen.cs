using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInBloodScreen : MonoBehaviour
{
    public Image overlay;
    public float fadeInDuration;

    IEnumerator Start()
    {
        yield return Tools.Fade(overlay, fadeInDuration, true);
    }
}
