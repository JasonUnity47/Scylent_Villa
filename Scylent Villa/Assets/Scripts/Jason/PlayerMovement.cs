using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    // Declaration
    public Joystick joystick;

    [SerializeField] private float moveSpeedDefault; // Store default move speed
    [SerializeField] private float moveSpeed;
    [SerializeField] private float offset;
    [SerializeField] private float increaseAmount = 1f; // Adjust this value as needed
    [SerializeField] private float multiplier = 2f;
    private float originalFOV;
    private float currentFOV;

    private Vector2 movement;

    public Vector2 direction;

    public bool front = false;
    public bool back = false;
    public bool left = false;
    public bool right = false;

    public bool move = false;

    private Rigidbody2D rb;

    private Animator anim;

    private Coroutine buffDurationCoroutine; // Coroutine reference for duration of buff

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        front = true;
        anim.SetBool("FrontBool", front);

        // Store default FOV
        UnityEngine.Rendering.Universal.Light2D playerLight = GetComponentInChildren<UnityEngine.Rendering.Universal.Light2D>();
        if (playerLight != null)
        {
            originalFOV = playerLight.pointLightOuterRadius;
            currentFOV = originalFOV;
        }

        // Store default move speed
        moveSpeedDefault = moveSpeed;
    }

    private void Update()
    {
        movement.x = joystick.Horizontal;
        movement.y = joystick.Vertical;

        if (joystick.gameObject.activeSelf == false)
        {
            movement.x = 0;
            movement.y = 0;
        }

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

        anim.SetFloat("Horizontal", movement.x);
        anim.SetFloat("Vertical", movement.y);
        anim.SetFloat("Speed", movement.sqrMagnitude); // Using sqrMagnitude for better performance.

        //Debug.Log(movement.x + "" + movement.y);

        if (movement.y < -offset && !front)
        {
            front = true;
            back = false;
            left = false;
            right = false;

            //Debug.Log(1);

            anim.SetBool("BackBool", back);
            anim.SetBool("RightBool", right);
            anim.SetBool("LeftBool", left);

            anim.SetBool("FrontBool", front);
        }

        if (movement.y > offset && !back)
        {
            back = true;
            front = false;
            left = false;
            right = false;

            //Debug.Log(2);

            anim.SetBool("FrontBool", front);
            anim.SetBool("RightBool", right);
            anim.SetBool("LeftBool", left);

            anim.SetBool("BackBool", back);
        }

        if (movement.x < -offset && !left)
        {
            left = true;
            front = false;
            back = false;
            right = false;

            //Debug.Log(3);

            anim.SetBool("FrontBool", front);
            anim.SetBool("BackBool", back);
            anim.SetBool("RightBool", right);

            anim.SetBool("LeftBool", left);
        }

        if (movement.x > offset && !right)
        {
            right = true;
            left = false;
            front = false;
            back = false;

            //Debug.Log(4);

            anim.SetBool("FrontBool", front);
            anim.SetBool("BackBool", back);
            anim.SetBool("LeftBool", left);

            anim.SetBool("RightBool", right);
        }
        
        if (movement != Vector2.zero)
        {
            direction = ((movement + (Vector2)transform.position) - (Vector2)transform.position) * -1;
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    // Apply acceleration buff
    public void ApplyAcceleration()
    {
        moveSpeed *= multiplier;

        // Start coroutine to revert changes after duration
        if (buffDurationCoroutine != null)
        {
            StopCoroutine(buffDurationCoroutine);
        }
        buffDurationCoroutine = StartCoroutine(RevertAccelerationAfterDuration(multiplier));
    }

    // Apply FOV increase buff
    public void ApplyFOVIncrease()
    {
        currentFOV += increaseAmount;

        // Update FOV
        UnityEngine.Rendering.Universal.Light2D playerLight = GetComponentInChildren<UnityEngine.Rendering.Universal.Light2D>();
        if (playerLight != null)
        {
            playerLight.pointLightOuterRadius = currentFOV;
        }

        // Start coroutine to revert changes after duration
        if (buffDurationCoroutine != null)
        {
            StopCoroutine(buffDurationCoroutine);
        }
        buffDurationCoroutine = StartCoroutine(RevertFOVAfterDuration(increaseAmount));
    }

    // Coroutine to revert acceleration changes after specified duration
    private IEnumerator RevertAccelerationAfterDuration(float multiplier)
    {
        yield return new WaitForSeconds(15f); // Wait for 15 seconds

        // Revert acceleration changes
        moveSpeed = moveSpeedDefault;
    }

    // Coroutine to revert FOV changes after specified duration
    private IEnumerator RevertFOVAfterDuration(float increaseAmount)
    {
        yield return new WaitForSeconds(15f); // Wait for 15 seconds

        // Revert FOV changes
        UnityEngine.Rendering.Universal.Light2D playerLight = GetComponentInChildren<UnityEngine.Rendering.Universal.Light2D>();
        if (playerLight != null)
        {
            currentFOV -= increaseAmount;
            playerLight.pointLightOuterRadius = currentFOV;
        }
    }
}
