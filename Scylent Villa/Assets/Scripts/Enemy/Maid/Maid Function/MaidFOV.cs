using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class MaidFOV : MonoBehaviour
{
    // Declaration
    // FOV
    [Header("Field Of View")]
    public float fovAngle = 90f;
    public float range = 7f;

    // Detection
    [Header("Detection")]
    [SerializeField] private GameObject detectionMark;
    private GameObject detectionObject;
    private bool once = false;
    public bool isDetected = false;
    private bool isVibrated = false;

    // Transfrom
    private Transform playerPos;
    private Vector2 directionToPlayer;

    // Script Reference
    private Maid maid;
    private MaidHit maidHit;
    private AbilityManager abilityManager;

    // Light
    [Header("Light")]
    public Light2D lightView;

    private void Start()
    {
        // Get reference.
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        maid = GetComponentInParent<Maid>();
        maidHit = GetComponentInParent<MaidHit>();

        abilityManager = FindObjectOfType<AbilityManager>();

        lightView = GetComponentInChildren<Light2D>();
    }

    private void Update()
    {
        // Get the current active scene name.
        string currentSceneName = SceneManager.GetActiveScene().name;

        // Check if the enemy is not in the tutorial game scene.
        if (currentSceneName != "Tutorial Level")
        {
            if (!maidHit.hitPlayer)
            {
                DetectPlayer();
                SetLightPosition();
                LightChange();
                ShowDetection();
            }

            else
            {
                isDetected = false;
            }
        }
        else
        {
            if (!maidHit.hitPlayer)
            {
                DetectPlayer();
                SetLightPosition();
                LightChange();
                ShowDetection();
            }

            else
            {
                isDetected = false;
                DetectPlayer();
                SetLightPosition();
                LightChange();
                ShowDetection();
            }
        }

    }

    // Detect whether player is in the field of view.
    void DetectPlayer()
    {
        directionToPlayer = playerPos.position - transform.position;

        // Calculate angle between the direction to the player and the direction of the Light2D transform.
        Vector2 lightDirection = transform.up; // Assuming the Light2D is oriented upwards.
        float angleToPlayer = Vector2.Angle(lightDirection, directionToPlayer);

        RaycastHit2D playerObject = Physics2D.Raycast(transform.position, directionToPlayer, range);
        Debug.DrawRay(transform.position, directionToPlayer, Color.red); // Visualize the raycast.

        if (angleToPlayer < fovAngle / 2)
        {
            if (playerObject.collider != null && playerObject.collider.CompareTag("Player") && !abilityManager.IsPlayerInStealth())
            {
                Debug.DrawRay(transform.position, directionToPlayer, Color.green); // Visualize the raycast
                isDetected = true;

                if (!isVibrated)
                {
                    isVibrated = true;
                    Handheld.Vibrate();
                }
            }

            else
            {
                isDetected = false;
                isVibrated = false;
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
            // Get the blend tree parameters for horizontal and vertical movement.
            float horizontalMovement = maid.Anim.GetFloat("Horizontal");
            float verticalMovement = maid.Anim.GetFloat("Vertical");

            // Determine the direction based on blend tree parameters.
            if (Mathf.Abs(horizontalMovement) > Mathf.Abs(verticalMovement))
            {
                // Horizontal movement dominates.
                if (horizontalMovement > 0)
                {
                    // Moving right.
                    transform.rotation = Quaternion.Euler(0, 0, -90);
                }
                else if (horizontalMovement < 0)
                {
                    // Moving left.
                    transform.rotation = Quaternion.Euler(0, 0, 90);
                }
            }
            else
            {
                // Vertical movement dominates.
                if (verticalMovement > 0)
                {
                    // Moving front.
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                }
                else if (verticalMovement < 0)
                {
                    // Moving back.
                    transform.rotation = Quaternion.Euler(0, 0, 180);
                }
            }
        }

        else
        {
            // If player is detected, rotate towards the player.
            // Create a quaternion rotation that points in the given direction (Vector3.forward is used for the z-axis).
            Quaternion rotation = Quaternion.LookRotation(Vector3.forward, directionToPlayer);

            // Set the rotation of the FOV transform.
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

    public void ShowDetection()
    {
        Vector2 detectionPos = (Vector2)transform.position + new Vector2(0, 1.35f);

        if (isDetected && !once)
        {
            once = true;
            detectionObject = Instantiate(detectionMark, detectionPos, Quaternion.identity, maid.gameObject.transform);
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