using System.Collections;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    // Declaration
    // Spawn Point
    [Header("Spawn Point")]
    public Transform[] spawnPoints;

    // Spawn Object
    [Header("Spawn Object")]
    [SerializeField] private GameObject food;
    [SerializeField] private bool[] locker;

    // Spawn UI
    [Header("Spawn UI")]
    [SerializeField] private GameObject foodUI;

    // Script Reference
    private CurrencySystem currencySystem;
    private CurrencyUI currencyUI;

    // Currency
    [Header("Currency")]
    public int maxFood;
    public int currentNumFood;

    // Buff
    [Header("Buff")]
    public bool doubleCurrencyActive = false; // Flag to track if double currency is active
    public float doubleCurrencyDuration = 15f; // Duration of double currency effect

    // Variable
    private int lastNumber = -1;

    // Timer
    [Header("Timer")]
    public float startTime;
    private float timeBtwFrame;

    private void Start()
    {
        // Get reference.
        currencySystem = GetComponent<CurrencySystem>();
        currencyUI = FindObjectOfType<CurrencyUI>();

        // Initialize the currency.
        currentNumFood = 0;

        // Initialize the timer.
        timeBtwFrame = startTime;

        // Intialize the ui panel.
        foodUI.SetActive(false);

        // Initialize the locker status.
        locker = new bool[spawnPoints.Length];

        for (int i = 0; i < locker.Length; i++)
        {
            locker[i] = false;
        }
    }

    private void Update()
    {
        // If the food count reach to the max value then keep as max value.
        if (currentNumFood > maxFood)
        {
            currentNumFood = maxFood;
        }

        // If the food count is less than the max value then continue spawning food.
        if (currentNumFood < maxFood)
        {
            // If the timer reach to 0.
            if (timeBtwFrame <= 0)
            {
                // Reset the timer.
                timeBtwFrame = startTime;

                // Random spawn food.
                int randomNumber = Random.Range(0, spawnPoints.Length);

                Debug.Log(randomNumber);

                if (randomNumber != lastNumber && locker[randomNumber] == false)
                {
                    locker[randomNumber] = true; // Lock the spawn position.
                    GameObject newFoodInstance = Instantiate(food, spawnPoints[randomNumber].position, Quaternion.identity); // Spawn food.
                    newFoodInstance.GetComponent<Food>().SetSpawnPointIndex(randomNumber);
                    currentNumFood++; // Increase the food count.
                }

                else
                {
                    // If spawn at the locked position then respawn until spawn at unlocked position.
                    timeBtwFrame = 0;
                }

                lastNumber = randomNumber;
            }

            else
            {
                timeBtwFrame -= Time.deltaTime;
            }
        }

        else
        {
            timeBtwFrame = startTime;
        }
    }


    public void DecrementFoodCount()
    {
        currentNumFood--; // Decrement currentNumFood when a food is destroyed
       
    }

    public void ResetLockerAtIndex(int index)
    {
        locker[index] = false;
    }

    public void CurrencyCount(int currencyEarned)
    {
        
        // Add the earned currency to total currency
        currencySystem.totalCurrencyEarned += currencyEarned;

        // Update the UI with the new total currency amount
        currencyUI.UpdateTotalCurrencyUI(currencySystem.totalCurrencyEarned);

        StartCoroutine(ActivateFoodUI());
    }

    private IEnumerator ActivateFoodUI()
    {
        // Show ui panel.
        foodUI.SetActive(true);

        
        yield return new WaitForSeconds(5f);

        // Hide ui panel.
        foodUI.SetActive(false);
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