using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathfinding : MonoBehaviour
{
    private Transform playerPos;

    public Transform detectArea;

    [SerializeField] private float radius;

    [SerializeField] private LayerMask whatIsPlayer;

    public bool isDetected = false;

    private AIPath aiPath;

    private void Awake()
    {
        aiPath = GetComponent<AIPath>();

        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        DetectPlayer();
        ChasePlayer();
    }

    void DetectPlayer()
    {
        isDetected = Physics2D.OverlapCircle(detectArea.position, radius, whatIsPlayer);
    }

    void ChasePlayer()
    {
        if (isDetected)
        {
            aiPath.destination = playerPos.position;
        }
    }
}
