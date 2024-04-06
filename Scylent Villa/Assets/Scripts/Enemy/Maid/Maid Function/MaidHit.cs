using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaidHit : MonoBehaviour
{
    // Declaration
    private AIPath aIPath;
    private MaidFOV maidFOV;

    public bool hitPlayer = false;

    private void Start()
    {
        aIPath = GetComponent<AIPath>();
        maidFOV = GetComponentInChildren<MaidFOV>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && maidFOV.isDetected)
        {
            collision.gameObject.GetComponent<PlayerHealth>().isDead = true;

            hitPlayer = true;

            // Enemy should stop searching if player is dead.
            aIPath.canSearch = false;
        }
    }
}
