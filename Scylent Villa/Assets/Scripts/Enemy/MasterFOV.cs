using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class MasterFOV : MonoBehaviour
{
    // Declaration
    public float fovAngle = 90f;
    public float range = 8;
    [SerializeField] private LayerMask whatIsPlayer;

    private RaycastHit2D playerObject;
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
        // Calculate angle between the direction to the player and the direction of the Light2D transform
        Vector2 lightDirection = transform.up; // Assuming the Light2D is oriented upwards
        float angleToPlayer = Vector2.Angle(lightDirection, directionToPlayer);

        playerObject = Physics2D.Raycast(transform.position, directionToPlayer, range, whatIsPlayer);

        if (angleToPlayer < fovAngle / 2)
        {
            if (playerObject.collider != null && playerObject.collider.CompareTag("Player"))
            {
                Debug.DrawRay(transform.position, directionToPlayer * range, Color.cyan); // Visualize the raycast
                isDetected = true;
            }

            else
            {
                isDetected = false;
            }
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
            // Create a quaternion rotation that points in the given direction (Vector3.forward is used for the z-axis)
            Quaternion rotation = Quaternion.LookRotation(Vector3.forward, directionToPlayer);

            // Set the rotation of the FOV transform
            transform.rotation = rotation;
        }

        return;
    }
}
