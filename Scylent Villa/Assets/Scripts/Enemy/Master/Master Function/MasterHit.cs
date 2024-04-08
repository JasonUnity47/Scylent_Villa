using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterHit : MonoBehaviour
{
    // Declaration
    private AIPath aIPath;
    private MasterFOV masterFOV;

    public bool hitPlayer = false;

    private void Start()
    {
        aIPath = GetComponent<AIPath>();
        masterFOV = GetComponentInChildren<MasterFOV>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && masterFOV.isDetected)
        {
            collision.gameObject.GetComponent<PlayerHealth>().isDead = true;

            hitPlayer = true;

            // Enemy should stop searching if player is dead.
            aIPath.canMove = false;
            aIPath.canSearch = false;
            aIPath.maxSpeed = 0;
            aIPath.maxAcceleration = 0;
        }
    }
}
