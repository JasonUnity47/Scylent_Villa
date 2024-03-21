using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterHealth : MonoBehaviour
{
    private MasterMovement masterMovement;

    public bool isStunned = false;

    public float stunTime;

    private void Start()
    {
        masterMovement = GetComponent<MasterMovement>();

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
        float storeMoveSpeed = masterMovement.moveSpeed;
        float storeChaseSpeed = masterMovement.chaseSpeed;

        masterMovement.moveSpeed = 0;
        masterMovement.chaseSpeed = 0;

        yield return new WaitForSeconds(stunTime);

        isStunned = false;

        masterMovement.moveSpeed = storeMoveSpeed;
        masterMovement.chaseSpeed = storeChaseSpeed;
    }
}
