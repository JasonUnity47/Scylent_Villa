using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSpawner : MonoBehaviour
{
    public GameObject mushroomSpawn; // Prefab to spawn
    public GameObject bucketSpawn; // Prefab to spawn
    public Transform spawnPoint1; // Point where the prefab should spawn
    public Transform spawnPoint2; // Point where the prefab should spawn
    private bool spawnedMushroom = false;
    private bool spawnedBucket = false;

    private void Update()
    {
        if(spawnedMushroom == false)
        {
            SpawnMushroom();
        }
        if (spawnedBucket == false)
        {
            SpawnBucket();
        }
    }

    private void SpawnMushroom()
    {
        
        // Spawn the prefab at the spawn point
        Instantiate(mushroomSpawn, spawnPoint1.position, spawnPoint1.rotation);
        spawnedMushroom = true;
    }

    public void RemoveMSpawn()
    {
        spawnedMushroom = false;
    }

    private void SpawnBucket()
    {
        Instantiate(bucketSpawn, spawnPoint2.position, spawnPoint2.rotation);
        spawnedBucket = true;
    }

    public void RemoveBSpawn()
    {
        spawnedMushroom = false;
    }
}
