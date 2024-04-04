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
    private int currentIndex = 0;

    // Value
    [Header("Value")]
    public float moveSpeed;

    // Timer
    [Header("Timer")]
    [SerializeField] private float startWaitTime;
    private float timeBtwWaitTime;

    // Component Reference
    private Rigidbody2D rb;

    private void Start()
    {
        aIPath = GetComponent<AIPath>();
        rb = GetComponent<Rigidbody2D>();
        timeBtwWaitTime = startWaitTime; // Set the intial time for timer.
    }

    public void Patrol()
    {
        aIPath.destination = moveSpots[currentIndex].position;

        if (Vector2.Distance(transform.position, moveSpots[currentIndex].position) < 0.3f)
        {
            if (timeBtwWaitTime <= 0)
            {
                if (currentIndex + 1 < moveSpots.Length)
                {
                    currentIndex++;
                }

                else
                {
                    currentIndex = 0;
                }

                timeBtwWaitTime = startWaitTime;
            }

            else
            {
                timeBtwWaitTime -= Time.deltaTime;
            }
        }
    }
}
