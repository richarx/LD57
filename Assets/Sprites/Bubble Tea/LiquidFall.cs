using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidFall : MonoBehaviour
{
    public float speed;

    private SpriteRenderer spriteRenderer;

    private IEnumerator Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        while (spriteRenderer.size.y > 0.05f)
        {
            spriteRenderer.size = new Vector2(spriteRenderer.size.x, spriteRenderer.size.y - Time.deltaTime * speed);
            yield return null;
        }
    }
}
