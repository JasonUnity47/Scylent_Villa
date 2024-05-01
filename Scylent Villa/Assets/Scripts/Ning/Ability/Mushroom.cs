using System.Collections;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    // Ability Manager
    private AbilityManager abilityManager;

    // Animation
    [Header("Animation")]
    private bool isAnimating = true;
    [SerializeField] private float animationDuration = 0.5f; // Duration of the animation
    [SerializeField] private float animationHeight = 0.5f; // Height to move the food prefab

    private void Awake()
    {
        // Get reference.
        abilityManager = FindObjectOfType<AbilityManager>();

        StartCoroutine(Animate());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<AudioManager>().Play("Pickup");
            isAnimating = false; // Stop animation.
            
            abilityManager.MushroomAvailableBool();

            // Destroy the collided ability.
            Destroy(gameObject);
        }
    }

    private IEnumerator Animate()
    {
        while (isAnimating) // Loop animation if isAnimating is true.
        {
            Vector2 originalPos = transform.position;
            Vector2 targetPos = originalPos + new Vector2(0, animationHeight);

            float timeElapsed = 0f;

            while (timeElapsed < animationDuration)
            {
                transform.position = Vector2.Lerp(originalPos, targetPos, timeElapsed / animationDuration);
                timeElapsed += Time.deltaTime;
                yield return null;
            }

            transform.position = targetPos;

            yield return new WaitForSeconds(0f); // Wait for a short time at the top position.

            timeElapsed = 0f;

            while (timeElapsed < animationDuration)
            {
                transform.position = Vector2.Lerp(targetPos, originalPos, timeElapsed / animationDuration);
                timeElapsed += Time.deltaTime;
                yield return null;
            }

            transform.position = originalPos;

            yield return null; // Add a small delay before starting the animation again.
        }
    }
}