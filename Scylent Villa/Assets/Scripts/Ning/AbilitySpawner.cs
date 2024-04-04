using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySpawner : MonoBehaviour
{
    // Declaration
    public Transform[] abilitySpawnPoints;
    [SerializeField] private GameObject bucket;
    [SerializeField] private GameObject mushroom;
    private GameObject currentAbility;
    private bool abilitySpawned = false;


    private void Start()
    {
        SpawnNewAbility();
    }

    private void Update()
    {
        // Check if the ability has been destroyed
        if (!abilitySpawned && currentAbility == null)
        {
            // Spawn a new ability
            SpawnNewAbility();
        }
    }

    public void RemoveAbility()
    {
        abilitySpawned = false;
    }

    private void SpawnNewAbility()
    {
        int randomNumber = Random.Range(0, abilitySpawnPoints.Length);
        Transform spawnPoint = abilitySpawnPoints[randomNumber];
        // Randomly choose between bucket and mushroom
        GameObject abilityPrefab = Random.Range(0, 2) == 0 ? bucket : mushroom;
        currentAbility = Instantiate(abilityPrefab, spawnPoint.position, Quaternion.identity);
        // Set ability spawned flag to true
        abilitySpawned = true;
    }
}

