using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStealth : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void MakeInvisibleForDuration(float duration)
    {
        StartCoroutine(MakeInvisibleCoroutine(duration));
    }

    private IEnumerator MakeInvisibleCoroutine(float duration)
    {
        Color originalColor = spriteRenderer.color;
        spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0.5f);
        yield return new WaitForSeconds(duration);
        spriteRenderer.color = originalColor;
    }
}
