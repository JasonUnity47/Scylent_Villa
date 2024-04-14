using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonHit : MonoBehaviour
{
    // Declaration
    private AIPath aIPath;
    private SonFOV sonFOV;

    public bool hitPlayer = false;

    private void Start()
    {
        aIPath = GetComponent<AIPath>();
        sonFOV = GetComponentInChildren<SonFOV>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && sonFOV.isDetected)
        {
            collision.gameObject.GetComponent<PlayerHealth>().isDead = true;

            hitPlayer = true;
        }
    }
}
