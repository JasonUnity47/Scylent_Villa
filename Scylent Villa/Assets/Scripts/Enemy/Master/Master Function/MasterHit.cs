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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && masterFOV.isDetected)
        {
            collision.gameObject.GetComponent<PlayerHealth>().isDead = true;

            hitPlayer = true;

            // Enemy should stop searching if player is dead.
            aIPath.canSearch = false;
        }
    }
}
