using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    // Declaration
    public Transform[] spawnPoints;
    [SerializeField] private GameObject food;
    [SerializeField] private bool[] locker;
    public int maxFood;
    public int currentNumFood;

    private int lastNumber = -1;

    // Timer
    public float startTime;
    private float timeBtwFrame;

    private void Start()
    {
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
}
