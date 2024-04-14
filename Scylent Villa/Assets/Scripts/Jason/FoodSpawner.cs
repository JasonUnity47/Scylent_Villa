using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    // Declaration
    public Transform[] spawnPoints;
    [SerializeField] private GameObject food;
    [SerializeField] private bool[] locker;
    [SerializeField] private GameObject foodUI;
    private CurrencySystem currencySystem;
    private CurrencyUI currencyUI;
    public int maxFood;
    public int currentNumFood;
    

    private int lastNumber = -1;

    // Timer
    public float startTime;
    private float timeBtwFrame;

    private void Start()
    {
        currencySystem = GetComponent<CurrencySystem>();
        currencyUI = FindObjectOfType<CurrencyUI>();

        currentNumFood = 0;
        timeBtwFrame = startTime;
        foodUI.SetActive(false);
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
                    GameObject newFoodInstance = Instantiate(food, spawnPoints[randomNumber].position, Quaternion.identity);
                    newFoodInstance.GetComponent<Food>().SetSpawnPointIndex(randomNumber);
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
       
        foodUI.SetActive(true);

        
        yield return new WaitForSeconds(5f);

       
        foodUI.SetActive(false);
    }
}