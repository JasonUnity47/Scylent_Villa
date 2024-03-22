using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonHealth : MonoBehaviour
{
    private SonMovement sonMovement;

    public bool isStunned = false;

    public float stunTime;

    private void Start()
    {
        sonMovement = GetComponent<SonMovement>();

        //GetStunned();
    }

    public void GetStunned()
    {
        StartCoroutine("WaitStun");
    }

    IEnumerator WaitStun()
    {
        // Is stunned.
        isStunned = true;

        // Stored value
        float storeMoveSpeed = sonMovement.moveSpeed;
        float storeChaseSpeed = sonMovement.chaseSpeed;

        sonMovement.moveSpeed = 0;
        sonMovement.chaseSpeed = 0;

        yield return new WaitForSeconds(stunTime);

        isStunned = false;

        sonMovement.moveSpeed = storeMoveSpeed;
        sonMovement.chaseSpeed = storeChaseSpeed;
    }
}
