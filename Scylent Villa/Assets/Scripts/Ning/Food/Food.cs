using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField] private int minCurrencyAmount;
    [SerializeField] private int maxCurrencyAmount;
    [SerializeField] private float animationDuration = 1.2f; // Duration of the animation
    [SerializeField] private float animationHeight = 0.15f; // Height to move the food prefab
    private bool doubleCurrencyActive = false; // Flag to track if double currency is active
    [SerializeField] private float doubleCurrencyDuration = 15f; // Duration of double currency effect

    private CurrencyUI currencyUI;
    private FoodSpawner foodSpawner; // Reference to the FoodSpawner
    private int spawnPointIndex;
    private bool isAnimating = true;

    // Add this method to set the spawn point index
    public void SetSpawnPointIndex(int index)
    {
        spawnPointIndex = index;
    }

    private void Awake()
    {

        // Get reference to FoodSpawner script
        foodSpawner = FindObjectOfType<FoodSpawner>();

        currencyUI = FindObjectOfType<CurrencyUI>();

        // Start animation coroutine
        StartCoroutine(Animate());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isAnimating = false; // Stop animation

            // Earn currency
            int currencyEarned = Random.Range(minCurrencyAmount, maxCurrencyAmount + 1);

            // If double currency is active, double the currency earned
            if (doubleCurrencyActive)
            {
                currencyEarned *= 2;
            }

            // Pass currencyEarned to FoodSpawner
            foodSpawner.CurrencyCount(currencyEarned);

            // Decrement food count in FoodSpawner
            foodSpawner.DecrementFoodCount();

            // Reset locker for the spawn point index of this food
            foodSpawner.ResetLockerAtIndex(spawnPointIndex);

            Destroy(gameObject);
        }
    }

    private IEnumerator Animate()
    {
        while (isAnimating) // Loop animation if isAnimating is true
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

            yield return new WaitForSeconds(0f); // Wait for a short time at the top position

            timeElapsed = 0f;

            while (timeElapsed < animationDuration)
            {
                transform.position = Vector2.Lerp(targetPos, originalPos, timeElapsed / animationDuration);
                timeElapsed += Time.deltaTime;
                yield return null;
            }

            transform.position = originalPos;

            yield return null; // Add a small delay before starting the animation again
        }
    }

    // Method to activate double currency for a certain duration
    public void ActivateDoubleCurrency()
    {
        StartCoroutine(DoubleCurrencyTimer());
    }

    // Coroutine to deactivate double currency after a certain duration
    private IEnumerator DoubleCurrencyTimer()
    {
        doubleCurrencyActive = true;
        yield return new WaitForSeconds(doubleCurrencyDuration);
        doubleCurrencyActive = false;
    }
}