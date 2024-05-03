using System.Collections;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    // Declaration
    // Joystick Reference
    [Header("Joystick Reference")]
    public Joystick joystick;

    // Movement Attribute
    [Header("Movement Attribute")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float offset;

    // Buff Value
    [Header("Buff Value")]

    // Acceleration
    [Header("Acceleration")]
    public float accelerationDuration = 15f;
    [SerializeField] private float moveSpeedDefault; // Store default move speed
    [SerializeField] private float multiplier = 2f;

    // Increase FOV
    [Header("Increase FOV")]
    [SerializeField] private float increaseAmount = 1f; // Adjust this value as needed
    [SerializeField] private float increaseAngleAmount = 1f; // Adjust this value as needed
    public float FOVDuration = 15f;
    private float originalFOV;
    private float originalInnerAngle;
    private float originalOuterAngle;
    private float currentFOV;
    private float currentInnerAngle;
    private float currentOuterAngle;

    // Variable
    [Header("Variable")]
    public Vector2 movement;
    public Vector2 direction;

    // Facing Direction
    [Header("Facing Direction")]
    public bool front = false;
    public bool back = false;
    public bool left = false;
    public bool right = false;

    public bool move = false;

    // Component Reference
    private Rigidbody2D rb;
    private Animator anim;

    private Coroutine buffDurationCoroutine; // Coroutine reference for duration of buff

    public bool isWalkSoundPlaying = false; // Flag to track if walk sound is playing


    private void Start()
    {
        // Get reference.
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        // The player will facing front at the beginning of the game.
        front = true;
        anim.SetBool("FrontBool", front);

        // Store default FOV
        UnityEngine.Rendering.Universal.Light2D playerLight = GetComponentInChildren<UnityEngine.Rendering.Universal.Light2D>();
        if (playerLight != null)
        {
            originalFOV = playerLight.pointLightOuterRadius;
            originalInnerAngle = playerLight.pointLightInnerAngle;
            originalOuterAngle = playerLight.pointLightOuterAngle;
            currentFOV = originalFOV;
            currentInnerAngle = originalInnerAngle;
            currentOuterAngle = originalOuterAngle;
        }

        // Store default move speed
        moveSpeedDefault = moveSpeed;
    }

    private void Update()
    {
        if (Time.timeScale == 0)
        {
            // Time pause, stop walk sound.
            FindObjectOfType<AudioManager>().Stop("Walk");
            isWalkSoundPlaying = false;
        }

        if (Time.timeScale != 0)
        {
            // Get touch position.
            movement.x = joystick.Horizontal;
            movement.y = joystick.Vertical;

            // If no joystick exists then no movement.
            if (joystick.gameObject.activeSelf == false)
            {
                movement.x = 0;
                movement.y = 0;
            }

            // If movement value is not 0 which means the player is moving.
            if (movement.sqrMagnitude != 0)
            {
                move = true;
                anim.SetBool("MoveBool", move);
            }

            else
            {
                move = false;
                anim.SetBool("MoveBool", move);
            }
            // Handle walk sound based on movement state
            if (move)
            {
                if (!isWalkSoundPlaying)
                {
                    // Player started moving, play walk sound
                    FindObjectOfType<AudioManager>().Play("Walk");
                    isWalkSoundPlaying = true;
                }
            }
            else
            {
                if (isWalkSoundPlaying)
                {
                    // Player stopped moving, stop walk sound
                    FindObjectOfType<AudioManager>().Stop("Walk");
                    isWalkSoundPlaying = false;
                }
            }

            // Set movement animations.
            anim.SetFloat("Horizontal", movement.x);
            anim.SetFloat("Vertical", movement.y);
            anim.SetFloat("Speed", movement.sqrMagnitude); // Using sqrMagnitude for better performance.

            // Face front.
            if (movement.y < -offset && !front)
            {
                front = true;
                back = false;
                left = false;
                right = false;

                anim.SetBool("BackBool", back);
                anim.SetBool("RightBool", right);
                anim.SetBool("LeftBool", left);

                anim.SetBool("FrontBool", front);
            }

            // Face back.
            if (movement.y > offset && !back)
            {
                back = true;
                front = false;
                left = false;
                right = false;

                anim.SetBool("FrontBool", front);
                anim.SetBool("RightBool", right);
                anim.SetBool("LeftBool", left);

                anim.SetBool("BackBool", back);
            }

            // Face left.
            if (movement.x < -offset && !left)
            {
                left = true;
                front = false;
                back = false;
                right = false;

                anim.SetBool("FrontBool", front);
                anim.SetBool("BackBool", back);
                anim.SetBool("RightBool", right);

                anim.SetBool("LeftBool", left);
            }

            // Face right.
            if (movement.x > offset && !right)
            {
                right = true;
                left = false;
                front = false;
                back = false;

                anim.SetBool("FrontBool", front);
                anim.SetBool("BackBool", back);
                anim.SetBool("LeftBool", left);

                anim.SetBool("RightBool", right);
            }

            // Get facing direction.
            if (movement != Vector2.zero)
            {
                direction = ((movement + (Vector2)transform.position) - (Vector2)transform.position) * -1;
            }
        }
    }

    private void FixedUpdate()
    {
        if (Time.timeScale != 0)
        {
            // Move.
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        }
    }

    // Apply acceleration buff
    public void ApplyAcceleration()
    {
        if (moveSpeed == moveSpeedDefault)
        {
            moveSpeed *= multiplier;
        }

        // Start coroutine to revert changes after duration
        if (buffDurationCoroutine != null)
        {
            StopCoroutine(buffDurationCoroutine);
        }
        buffDurationCoroutine = StartCoroutine(RevertAccelerationAfterDuration());
    }

    // Apply FOV increase buff
    public void ApplyFOVIncrease()
    {
        if (currentFOV == originalFOV && currentInnerAngle == originalInnerAngle && currentOuterAngle == originalOuterAngle)
        {
            currentFOV += increaseAmount;
            currentInnerAngle += increaseAngleAmount;
            currentOuterAngle += increaseAngleAmount;
        }

        // Update FOV
        UnityEngine.Rendering.Universal.Light2D playerLight = GetComponentInChildren<UnityEngine.Rendering.Universal.Light2D>();
        if (playerLight != null)
        {
            playerLight.pointLightOuterRadius = currentFOV;
            // Increase the outer spot angle
            playerLight.pointLightOuterAngle = currentOuterAngle;

            // Increase the inner spot angle (optional)
            playerLight.pointLightInnerAngle = currentInnerAngle;
        }

        // Start coroutine to revert changes after duration
        if (buffDurationCoroutine != null)
        {
            StopCoroutine(buffDurationCoroutine);
        }
        buffDurationCoroutine = StartCoroutine(RevertFOVAfterDuration(increaseAmount, increaseAngleAmount, increaseAngleAmount));
    }

    // Coroutine to revert acceleration changes after specified duration
    private IEnumerator RevertAccelerationAfterDuration()
    {
        yield return new WaitForSeconds(accelerationDuration); // Wait for 15 seconds

        // Revert acceleration changes
        moveSpeed = moveSpeedDefault;
    }

    // Coroutine to revert FOV changes after specified duration
    private IEnumerator RevertFOVAfterDuration(float increaseAmount, float increaseInnerAngle, float increaseOuterAngle)
    {
        yield return new WaitForSeconds(FOVDuration); // Wait for 15 seconds

        // Revert FOV changes
        UnityEngine.Rendering.Universal.Light2D playerLight = GetComponentInChildren<UnityEngine.Rendering.Universal.Light2D>();
        if (playerLight != null)
        {
            currentFOV -= increaseAmount;
            playerLight.pointLightOuterRadius = currentFOV;
            currentInnerAngle -= increaseAngleAmount;
            playerLight.pointLightInnerAngle = currentInnerAngle;
            currentOuterAngle -= increaseAngleAmount;
            playerLight.pointLightOuterAngle = currentOuterAngle;
        }
    }
}
