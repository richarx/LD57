using DG.Tweening;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    const KeyCode VolumeUpKey = KeyCode.UpArrow;
    const KeyCode VolumeDownKey = KeyCode.DownArrow;

    const float VolumeStep = 0.1f;

    const string MixerParameterName = "Volume";

    public float fadeDuration = 0.1f;
    public float pauseDuration = 0.5f;
    [Space]
    public Image volumeUpGraphic;
    public Image volumeDownGraphic;
    [Space]
    public AudioMixer audioMixer;
    [Space]
    public AudioClip onChangeSound;

    Sequence volumeUpSequence;
    Sequence volumeDownSequence;

    float currentVolume = 0.5f;

    void Awake()
    {
        ApplyVolumeToMixerGroup();

        volumeUpGraphic.DOFade(0, 0);
        volumeDownGraphic.DOFade(0, 0);
    }

    void Update()
    {
        if (Input.GetKeyDown(VolumeUpKey))
        {
            volumeUpSequence?.Kill(true);
            volumeDownSequence?.Kill(true);

            volumeUpSequence = DOTween.Sequence()
                                      .Append(volumeUpGraphic.DOFade(1, fadeDuration))
                                      .AppendInterval(pauseDuration)
                                      .Append(volumeUpGraphic.DOFade(0, fadeDuration));

            currentVolume = Mathf.Clamp01(currentVolume + VolumeStep);
            ApplyVolumeToMixerGroup();

            SFXManager.Instance.PlaySFX(onChangeSound, 1);
        }

        if (Input.GetKeyDown(VolumeDownKey))
        {
            volumeUpSequence?.Kill(true);
            volumeDownSequence?.Kill(true);

            volumeUpSequence = DOTween.Sequence()
                                      .Append(volumeDownGraphic.DOFade(1, fadeDuration))
                                      .AppendInterval(pauseDuration)
                                      .Append(volumeDownGraphic.DOFade(0, fadeDuration));

            currentVolume = Mathf.Clamp01(currentVolume - VolumeStep);
            ApplyVolumeToMixerGroup();

            SFXManager.Instance.PlaySFX(onChangeSound, 1);
        }
    }

    void ApplyVolumeToMixerGroup()
    {
        float dB = Mathf.Log10(Mathf.Clamp(currentVolume, 0.0001f, 1f)) * 20;
        audioMixer.SetFloat(MixerParameterName, dB);
    }
}
