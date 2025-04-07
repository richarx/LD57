using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(SpriteRenderer))]
public class MouseTuto : MonoBehaviour
{
    public float moveDownDuration = 1f;
    public float targetY;
    [Space]
    public float waitInBetweenTime = 0.3f;
    [Space]
    public float teleportBackDuration = 1f;

    float startY;
    SpriteRenderer spriteRenderer;
    Sequence sequence;

    void Awake()
    {
        startY = transform.position.y;

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        sequence = DOTween.Sequence();

        sequence.AppendInterval(waitInBetweenTime)
                .Append(transform.DOMoveY(targetY, moveDownDuration))
                .AppendInterval(waitInBetweenTime)
                .Append(spriteRenderer.DOFade(0, teleportBackDuration / 2f))
                .Append(transform.DOMoveY(startY, 0))
                .Append(spriteRenderer.DOFade(1, teleportBackDuration / 2f))
                .SetLoops(-1);
    }

    void OnDestroy()
    {
        sequence.Kill();
    }
}
