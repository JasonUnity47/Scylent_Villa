using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaidHealth : MonoBehaviour
{
    private MaidMovement maidMovement;

    public bool isStunned = false;

    public float stunTime;

    private void Start()
    {
        maidMovement = GetComponent<MaidMovement>();

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
        float storeMoveSpeed = maidMovement.moveSpeed;
        float storeChaseSpeed = maidMovement.chaseSpeed;

        maidMovement.moveSpeed = 0;
        maidMovement.chaseSpeed = 0;

        yield return new WaitForSeconds(stunTime);

        isStunned = false;

        maidMovement.moveSpeed = storeMoveSpeed;
        maidMovement.chaseSpeed = storeChaseSpeed;
    }
}
