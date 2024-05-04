using UnityEngine;

public class BuffSpawner : MonoBehaviour
{
    // Declaration
    // Buff Spawner
    [Header("Buff Spawner")]
    public Transform[] buffSpawnPoints;

    // Buff
    [Header("Buff")]
    [SerializeField] private GameObject acceleration;
    [SerializeField] private GameObject increaseFOV;
    [SerializeField] private GameObject doubleCurrency;

    // Locker
    [Header("Locker")]
    [SerializeField] public bool[] buffLocker;

    // Buff Attribute
    [Header("Buff Attribute")]
    public int maxBuff;
    public int currentNumBuff;

    // Variable
    private int lastNumber = -1;

    // Timer
    [Header("Timer")]
    public float startTime;
    private float timeBtwFrame;

    private void Start()
    {
        // Initialize buff and timer.
        currentNumBuff = 0;
        timeBtwFrame = startTime;

        // Initialize the locker status.
        buffLocker = new bool[buffSpawnPoints.Length];

        for (int i = 0; i < buffLocker.Length; i++)
        {
            buffLocker[i] = false;
        }
    }

    private void Update()
    {
        // If buff count reach to the max value then remain the value.
        if (currentNumBuff > maxBuff)
        {
            currentNumBuff = maxBuff;
        }

        // If buff count is less than the max value.
        if (currentNumBuff < maxBuff)
        {
            // If the timer reach to 0.
            if (timeBtwFrame <= 0)
            {
                // Reset the timer.
                timeBtwFrame = startTime;

                // Random spawn buff.
                int randomNumber = Random.Range(0, buffSpawnPoints.Length);

              

                if (randomNumber != lastNumber && !buffLocker[randomNumber])
                {
                    buffLocker[randomNumber] = true;
                    GameObject buffPrefab = Random.Range(0, 3) == 0 ? acceleration : (Random.Range(0, 2) == 0 ? increaseFOV : doubleCurrency);
                    GameObject newBuffInstance = Instantiate(buffPrefab, buffSpawnPoints[randomNumber].position, Quaternion.identity);

                    // Set the spawn point index for the buff.
                    SetBuffSpawnPointIndex(newBuffInstance, randomNumber);

                    currentNumBuff++;
                }
                else
                {
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
            // Check which type of buff is instantiated and set the spawn point index accordingly.
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
        currentNumBuff--; // Decrement currentNumFood when a buff is destroyed.
    }
}
