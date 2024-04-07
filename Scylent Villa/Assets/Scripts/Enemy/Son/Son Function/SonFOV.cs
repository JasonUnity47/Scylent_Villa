using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SonFOV : MonoBehaviour
{
    // Declaration
    public float fovAngle = 90f;
    public float range = 7f;

    [SerializeField] private GameObject detectionMark;
    private GameObject detectionObject;
    private bool once = false;

    private Transform playerPos;
    private Vector2 directionToPlayer;

    public bool isDetected = false;

    private Son son;
    private SonHit sonHit;

    private AbilityManager abilityManager;

    public Light2D lightView;

    private void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        son = GetComponentInParent<Son>();
        sonHit = GetComponentInParent<SonHit>();

        abilityManager = FindObjectOfType<AbilityManager>();

        lightView = GetComponentInChildren<Light2D>();
    }

    private void Update()
    {
        if (!sonHit.hitPlayer)
        {
            if (!abilityManager.IsPlayerInStealth())
            {
                DetectPlayer();
                SetLightPosition();
            }
        }

        else
        {
            isDetected = false;
        }

        LightChange();
        ShowDetection();
    }

    // Detect whether player is in the field of view
    void DetectPlayer()
    {
        directionToPlayer = playerPos.position - transform.position;

        // Calculate angle between the direction to the player and the direction of the Light2D transform
        Vector2 lightDirection = transform.up; // Assuming the Light2D is oriented upwards
        float angleToPlayer = Vector2.Angle(lightDirection, directionToPlayer);

        RaycastHit2D playerObject = Physics2D.Raycast(transform.position, directionToPlayer, range);
        Debug.DrawRay(transform.position, directionToPlayer, Color.red); // Visualize the raycast

        if (angleToPlayer < fovAngle / 2)
        {
            if (playerObject.collider != null && playerObject.collider.CompareTag("Player"))
            {
                Debug.DrawRay(transform.position, directionToPlayer, Color.green); // Visualize the raycast
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
            float horizontalMovement = son.Anim.GetFloat("Horizontal");
            float verticalMovement = son.Anim.GetFloat("Vertical");

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

    void LightChange()
    {
        if (isDetected)
        {
            lightView.color = Color.red;
            lightView.intensity = 3;
        }

        else if (!isDetected && lightView.color != Color.yellow)
        {
            lightView.color = Color.yellow;

            if (lightView.intensity > 1)
            {
                lightView.intensity = 1;
            }
        }

        return;
    }

    void ShowDetection()
    {
        Vector2 detectionPos = (Vector2)transform.position + new Vector2(0, 1.35f);

        if (isDetected && !once)
        {
            once = true;
            detectionObject = Instantiate(detectionMark, detectionPos, Quaternion.identity, son.gameObject.transform);
        }

        else if (!isDetected)
        {
            once = false;
            Destroy(detectionObject);
        }
    }

    public bool IsPlayerDetected()
    {
        return isDetected;
    }
}
