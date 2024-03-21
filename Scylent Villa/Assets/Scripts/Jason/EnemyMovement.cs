using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public abstract class EnemyMovement : MonoBehaviour
{
    private Transform targetPos;

    public float moveSpeed;
    public float stoppingDistance;
    public float activateDistance;
    private float nextWayPointDistance = 3f;
    private float pathUpdateSeconds = 0.5f;

    protected int currentWayPoint = 0;

    public bool isDetected = false;
    public bool isNearby = false;

    protected Path path;
    private Seeker seeker;

    protected Rigidbody2D rb;

    // Start is called before the first frame update
    public virtual void Start()
    {
        targetPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        seeker = GetComponent<Seeker>();

        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, pathUpdateSeconds);
    }

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

    public virtual void NearPlayer()
    {
        if (Vector2.Distance(rb.position, targetPos.position) <= stoppingDistance)
        {
            isNearby = true;
        }

        else
        {
            isNearby = false;
        }
    }

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
        Vector2 force = direction * moveSpeed * Time.deltaTime;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWayPoint]);

        if (distance < nextWayPointDistance)
        {
            currentWayPoint++;
        }
    }
}
