using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrugDrip : MonoBehaviour
{
    public float speed;

    private SpriteRenderer spriteRenderer;

    private IEnumerator Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        while (spriteRenderer.size.x >= 0)
        {
            spriteRenderer.size = new Vector2(spriteRenderer.size.x - Time.deltaTime * speed, spriteRenderer.size.y);
            yield return null;
        }

        spriteRenderer.size = new Vector2(0, spriteRenderer.size.y);
    }
}
