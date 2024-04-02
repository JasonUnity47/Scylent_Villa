using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFOVController : MonoBehaviour
{
    public EnemyMovement enemyMovement; // Reference to the EnemyMovement script attached to the enemy
    public UnityEngine.Rendering.Universal.Light2D light2D; // Reference to the Light2D component

    void Update()
    {
        // Check if enemyMovement is not null before using it
        if (enemyMovement != null)
        {
            // Check if the enemy is patrolling or chasing the player
            if (!enemyMovement.isDetected)
            {
                // Patrol: Set light position to next move spot
                SetLightPosition(enemyMovement.GetMoveSpot());
            }
            else
            {
                // Chase: Set light position to next target
                SetLightPosition(enemyMovement.GetTarget());
            }
        }
        else
        {
            Debug.LogError("EnemyMovement reference is not assigned!");
        }
    }

    // Set the light's position based on the next movement direction
    void SetLightPosition(Vector2 direction)
    {
        // Create a quaternion rotation that points in the given direction (Vector3.forward is used for the z-axis)
        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, direction);

        // Set the rotation of the Light2D transform
        light2D.transform.rotation = rotation;
    }
}
