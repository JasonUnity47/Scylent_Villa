using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyFOV : MonoBehaviour
{
    // Declaration
    public float fovAngle = 90f;
    public float range = 8;
    [SerializeField] private LayerMask whatIsPlayer;

    [SerializeField] private RaycastHit2D playerObject;

    private Transform playerPos;
    private Vector2 direction;

    public bool isDetected = false;

    private void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        direction = (playerPos.position - transform.position).normalized;
        float angle = Vector3.Angle(direction, transform.up);

        playerObject = Physics2D.Raycast(transform.position, direction, range, whatIsPlayer);

        if (angle < fovAngle / 2)
        {
            if (playerObject.collider != null && playerObject.collider.CompareTag("Player"))
            {
                Debug.DrawRay(transform.position, direction * range, Color.yellow);
                isDetected = true;
            }
        }

        else
        {
            isDetected = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, -range));
    }
}
