using System.Collections;
using UnityEngine;

public class Food : MonoBehaviour
{
    // Food Count
    [Header("Food Count")]
    [SerializeField] private int minCurrencyAmount;
    [SerializeField] private int maxCurrencyAmount;

    // Animation
    [Header("Animation")]
    [SerializeField] private float animationDuration = 1.2f; // Duration of the animation
    [SerializeField] private float animationHeight = 0.15f; // Height to move the food prefab
    private bool isAnimating = true;

    // Script Reference
    private FoodSpawner foodSpawner; // Reference to the FoodSpawner
    
    // Spawn
    private int spawnPointIndex;

    // Add this method to set the spawn point index
    public void SetSpawnPointIndex(int index)
    {
        spawnPointIndex = index;
    }

    private void Awake()
    {
        // Get reference to FoodSpawner script.
        foodSpawner = FindObjectOfType<FoodSpawner>();

        // Start animation coroutine.
        StartCoroutine(Animate());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If this gameobject collide with object named Player.
        if (collision.CompareTag("Player"))
        {
            FindObjectOfType<AudioManager>().Play("Pickup");
            isAnimating = false; // Stop animation.

            // Earn currency.
            int currencyEarned = Random.Range(minCurrencyAmount, maxCurrencyAmount + 1);

            // If double currency is active, double the currency earned.
            if (foodSpawner.doubleCurrencyActive)
            {
                currencyEarned *= 2;
            }

            // Pass currencyEarned to FoodSpawner.
            foodSpawner.CurrencyCount(currencyEarned);

            // Decrement food count in FoodSpawner.
            foodSpawner.DecrementFoodCount();

            // Reset locker for the spawn point index of this food.
            foodSpawner.ResetLockerAtIndex(spawnPointIndex);

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