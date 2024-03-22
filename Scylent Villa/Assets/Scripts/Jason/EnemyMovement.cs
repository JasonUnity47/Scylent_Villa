using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public abstract class EnemyMovement : MonoBehaviour
{
    #region Declaration
    // Patrol
    [Header("Patrol")]
    public Transform[] moveSpots;
    private int currentIndex = 0;

    // Value
    [Header("Value")]
    public float moveSpeed;
    public float chaseSpeed;
    public float activateDistance;

    // Timer
    [Header("Timer")]
    public float startWaitTime;
    private float timeBtwWaitTime;

    public Vector2 movement;

    // Pathfinding Values
    private float nextWayPointDistance = 3f;
    private float pathUpdateSeconds = 0.5f;
    private int currentWayPoint = 0;

    // Check Value
    [Header("Check Value")]
    public bool isDetected = false;

    // Object Reference
    private Transform targetPos;

    // Pathfinding Reference
    private Path path;
    private Seeker seeker;

    // Component Reference
    protected Rigidbody2D rb;

    #endregion

    public virtual void Start()
    {
        targetPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        timeBtwWaitTime = startWaitTime; // Set the intial time for timer.

        InvokeRepeating("UpdatePath", 0f, pathUpdateSeconds);
    }

    // Pathfinding Function
    #region Pathfinding Function

    public virtual void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, targetPos.position, OnPathComplete);
        }
    }

    public virtual void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWayPoint = 0;
        }
    }

    #endregion

    // Detect player
    public virtual void TargetInDistance()
    {
        if (Vector2.Distance(rb.position, targetPos.position) < activateDistance)
        {
            isDetected = true;
        }

        else
        {
            isDetected = false;
        }
    }

    // Chase Player
    public virtual void PathFollow()
    {
        if (path == null)
        {
            return;
        }

        if (currentWayPoint >= path.vectorPath.Count)
        {
            return;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWayPoint] - rb.position).normalized;

        Vector2 force = direction * chaseSpeed * Time.deltaTime;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWayPoint]);

        if (distance < nextWayPointDistance)
        {
            currentWayPoint++;
        }
    }

    // Patrol
    public virtual void Patrol()
    {
        transform.position = Vector2.MoveTowards(transform.position, moveSpots[currentIndex].position, moveSpeed * Time.deltaTime);

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

    public Vector2 GetMoveSpot()
    {
        movement = moveSpots[currentIndex].position - transform.position;

        return movement;
    }

    public Vector2 GetTarget()
    {
        movement = targetPos.position - transform.position;

        return movement;
    }
}
