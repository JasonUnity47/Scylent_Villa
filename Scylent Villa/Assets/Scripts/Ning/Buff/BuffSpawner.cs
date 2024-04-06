using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffSpawner : MonoBehaviour
{
    // Declaration
    public Transform[] buffSpawnPoints;
    [SerializeField] private GameObject acceleration;
    [SerializeField] private GameObject increaseFOV;
    [SerializeField] private GameObject doubleCurrency;
    [SerializeField] public bool[] buffLocker;
    public int maxBuff;
    public int currentNumBuff;
    private int lastNumber = -1;

    // Timer
    public float startTime;
    private float timeBtwFrame;



    private void Start()
    {


        currentNumBuff = 0;
        timeBtwFrame = startTime;

        buffLocker = new bool[buffSpawnPoints.Length];

        for (int i = 0; i < buffLocker.Length; i++)
        {
            buffLocker[i] = false;
        }
    }


    private void Update()
    {
        if (currentNumBuff > maxBuff)
        {
            currentNumBuff = maxBuff;
        }

        if (currentNumBuff < maxBuff)
        {
            if (timeBtwFrame <= 0)
            {
                timeBtwFrame = startTime;

                int randomNumber = Random.Range(0, buffSpawnPoints.Length);

                Debug.Log(randomNumber);

                if (randomNumber != lastNumber && !buffLocker[randomNumber])
                {
                    buffLocker[randomNumber] = true;
                    GameObject buffPrefab = Random.Range(0, 3) == 0 ? acceleration : (Random.Range(0, 2) == 0 ? increaseFOV : doubleCurrency);
                    GameObject newBuffInstance = Instantiate(buffPrefab, buffSpawnPoints[randomNumber].position, Quaternion.identity);

                    // Set the spawn point index for the buff
                    SetBuffSpawnPointIndex(newBuffInstance, randomNumber);

                    currentNumBuff++;
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

    // Method to set the spawn point index for the instantiated buff
    private void SetBuffSpawnPointIndex(GameObject buffInstance, int spawnPointIndex)
    {
        if (buffInstance != null)
        {
            // Check which type of buff is instantiated and set the spawn point index accordingly
            if (buffInstance.CompareTag("AccelerationBuff"))
            {
                AccelerationBuff accelerationBuff = buffInstance.GetComponent<AccelerationBuff>();
                accelerationBuff.SetSpawnPointIndex(spawnPointIndex);
            }
            else if (buffInstance.CompareTag("IncreaseFOVBuff"))
            {
                IncreaseFOVBuff increaseFOVBuff = buffInstance.GetComponent<IncreaseFOVBuff>();
                increaseFOVBuff.SetSpawnPointIndex(spawnPointIndex);
            }
            else if (buffInstance.CompareTag("DoubleCurrencyBuff"))
            {
                DoubleCurrencyBuff doubleCurrencyBuff = buffInstance.GetComponent<DoubleCurrencyBuff>();
                doubleCurrencyBuff.SetSpawnPointIndex(spawnPointIndex);
            }
        }
    }

    // Method to unlock the spawn point after the buff is collected
    public void UnlockSpawnPoint(int spawnPointIndex)
    {
        if (spawnPointIndex >= 0 && spawnPointIndex < buffLocker.Length)
        {
            buffLocker[spawnPointIndex] = false;
        }
    }

    public void DecrementBuffCount()
    {
        currentNumBuff--; // Decrement currentNumFood when a food is destroyed

    }

   
}
