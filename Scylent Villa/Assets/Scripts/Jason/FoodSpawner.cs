using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    // Declaration
    public Transform[] spawnPoints;
    [SerializeField] private GameObject food;
    [SerializeField] private bool[] locker;
    [SerializeField] private int minCurrencyAmount;
    [SerializeField] private int maxCurrencyAmount;
    public int maxFood;
    public int currentNumFood;
    public CurrencyUI currencyUI;
    private int totalCurrencyEarned = 0;

    private int lastNumber = -1;

    // Timer
    public float startTime;
    private float timeBtwFrame;

    private void Start()
    {
        // Get a reference to the CurrencyUI component
        currencyUI = FindObjectOfType<CurrencyUI>();
        // Update the UI with initial total currency (which is 0 at start)
        currencyUI.UpdateTotalCurrencyUI(totalCurrencyEarned);

        currentNumFood = 0;
        timeBtwFrame = startTime;

        locker = new bool[spawnPoints.Length];

        for (int i = 0; i < locker.Length; i++)
        {
            locker[i] = false;
        }
    }

    private void Update()
    {
        if (currentNumFood > maxFood)
        {
            currentNumFood = maxFood;
        }

        if (currentNumFood < maxFood)
        {
            if (timeBtwFrame <= 0)
            {
                timeBtwFrame = startTime;

                int randomNumber = Random.Range(0, spawnPoints.Length);

                Debug.Log(randomNumber);

                if (randomNumber != lastNumber && locker[randomNumber] == false)
                {
                    locker[randomNumber] = true;
                    Instantiate(food, spawnPoints[randomNumber].position, Quaternion.identity);
                    currentNumFood++;
                }

                else
                {
                    Debug.Log("Reset.");
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Remove the collided food
            Destroy(other.gameObject);

            // Spawn a new food
            SpawnNewFood();

            // Earn currency
            int currencyEarned = Random.Range(minCurrencyAmount, maxCurrencyAmount + 1);
            // Now you can do something with this currencyEarned value, like adding it to the player's currency
            Debug.Log("Currency Earned: " + currencyEarned);

            // Add the earned currency to total currency
            totalCurrencyEarned += currencyEarned;

            // Update the UI with the new total currency amount
            currencyUI.UpdateTotalCurrencyUI(totalCurrencyEarned);
        }
    }

    private void SpawnNewFood()
    {
        int randomNumber = Random.Range(0, spawnPoints.Length);
        Instantiate(food, spawnPoints[randomNumber].position, Quaternion.identity);
    }
}