using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    // Declaration
    // AIPath
    private AIPath aIPath;

    // Patrol
    [Header("Patrol")]
    [SerializeField] private Transform[] moveSpots;

    // Value
    [Header("Value")]
    int randomIndex;
    int lastIndex;

    // Timer
    [Header("Timer")]
    [SerializeField] private float startWaitTime;
    private float timeBtwWaitTime;

    private void Start()
    {
        aIPath = GetComponent<AIPath>();
        timeBtwWaitTime = startWaitTime; // Set the intial time for timer.

        randomIndex = Random.Range(0, moveSpots.Length);
        lastIndex = -1;
    }

    public void Patrol()
    {
        if (moveSpots.Length != 0)
        {
            aIPath.destination = moveSpots[randomIndex].position;

            if (Vector2.Distance(transform.position, moveSpots[randomIndex].position) < 0.2f)
            {
                if (timeBtwWaitTime <= 0)
                {
                    lastIndex = randomIndex;

                    randomIndex = Random.Range(0, moveSpots.Length);

                    if (randomIndex == lastIndex)
                    {
                        timeBtwWaitTime = 0;
                    }
                    else
                    {
                        timeBtwWaitTime = startWaitTime;
                    }
                }

                else
                {
                    timeBtwWaitTime -= Time.deltaTime;
                }
            }
        }

        return;
    }
}