using System.Collections;
using UnityEngine;

public class PlayerStealth : MonoBehaviour
{
    // Component Reference
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        // Get reference.
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void MakeInvisibleForDuration(float duration)
    {
        StartCoroutine(MakeInvisibleCoroutine(duration));
    }

    private IEnumerator MakeInvisibleCoroutine(float duration)
    {
        // Change player color.
        Color originalColor = spriteRenderer.color;
        spriteRenderer.color = new Color(0, 1, 0.8564062f, 0.25f);

        yield return new WaitForSeconds(duration);

        spriteRenderer.color = originalColor;
    }
}