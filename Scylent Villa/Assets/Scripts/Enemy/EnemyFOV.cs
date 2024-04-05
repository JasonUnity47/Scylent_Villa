using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFOV : MonoBehaviour
{
    // Declaration
    public float fovAngle = 90f;
    public float range = 8;
    [SerializeField] private LayerMask whatIsPlayer;

    public RaycastHit2D playerObject;

    private Transform playerPos;
    private Vector2 directionToPlayer;

    public bool isDetected = false;

    private AIPath aIPath;
    private Master master;

    private void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        master = GetComponentInParent<Master>();
        aIPath = GetComponentInParent<AIPath>();
    }

    private void Update()
    {
        DetectPlayer();
        SetLightPosition();
    }

    // Detect whether player is in the field of view
    void DetectPlayer()
    {
        directionToPlayer = (playerPos.position - transform.position).normalized;
        float angle = Vector3.Angle(directionToPlayer, transform.up);

        playerObject = Physics2D.Raycast(transform.position, directionToPlayer, range, whatIsPlayer);

        if (angle < fovAngle / 2)
        {
            if (playerObject.collider != null && playerObject.collider.CompareTag("Player"))
            {
                Debug.DrawRay(transform.position, directionToPlayer * range, Color.yellow);
                isDetected = true;
            }

            else
            {
                isDetected = false;
            }
        }

        else
        {
            isDetected = false;
        }

        return;
    }

    void SetLightPosition()
    {
        if (!isDetected)
        {
            // Get the blend tree parameters for horizontal and vertical movement
            float horizontalMovement = master.Anim.GetFloat("Horizontal");
            float verticalMovement = master.Anim.GetFloat("Vertical");

            // Determine the direction based on blend tree parameters
            if (Mathf.Abs(horizontalMovement) > Mathf.Abs(verticalMovement))
            {
                // Horizontal movement dominates
                if (horizontalMovement > 0)
                {
                    // Moving right
                    transform.rotation = Quaternion.Euler(0, 0, -90);
                }
                else if (horizontalMovement < 0)
                {
                    // Moving left
                    transform.rotation = Quaternion.Euler(0, 0, 90);
                }
            }
            else
            {
                // Vertical movement dominates
                if (verticalMovement > 0)
                {
                    // Moving front
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                }
                else if (verticalMovement < 0)
                {
                    // Moving back
                    transform.rotation = Quaternion.Euler(0, 0, 180);
                }
            }
        }
        else
        {
            // If player is detected, rotate towards the player
            Quaternion rotation = Quaternion.LookRotation(Vector3.forward, directionToPlayer);
            transform.rotation = rotation;
        }
    }
}
