using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    // Declaration
    // Check
    [Header("Check")]
    public bool isDead = false;
    private bool once = false;
    private bool isDeadSoundPlayed = false; // Flag to track if dead sound is played

    // Value
    [Header("Dead")]
    public float waitForDead = 2f;

    // Component Reference
    private Rigidbody2D rb;
    private Animator anim;
    private PlayerMovement playerMovement;

    // Object Reference
    [Header("Object Reference")]
    [SerializeField] private GameObject FOV;
    [SerializeField] private GameObject bloodEffect;
    [SerializeField] private GameObject result;
    [SerializeField] private GameObject selfLight;
    [SerializeField] private JoystickPosition joystickPosition;

    // Find Reference
    private CurrencySystem currencySystem;

    // Define respawn point as a Transform
    [SerializeField] private Transform respawnPoint;

    private void Start()
    {
        // Get Reference.
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        currencySystem = FindObjectOfType<CurrencySystem>();
    }

    private void Update()
    {
        // Check if the player is dead.
        if (isDead)
        {
            // Stop moving.
            rb.velocity = Vector2.zero;

            // Stop walking sound.
            FindObjectOfType<AudioManager>().Stop("Walk");
            playerMovement.isWalkSoundPlaying = false;

            // Toggler to confirm the player will only die once.
            if (!once)
            {
                // Confirm dead,
                once = true;

                // Play dead sound if not played yet
                if (!isDeadSoundPlayed)
                {
                    // Play dead sound
                    FindObjectOfType<AudioManager>().Play("Dead");
                    isDeadSoundPlayed = true; // Set the flag to true
                }

                // Hide FOV.
                FOV.SetActive(false);

                // Hide Self Light.
                selfLight.SetActive(false);

                // Set the blood vfx position.
                Vector2 bloodPos = (Vector2)transform.position + new Vector2(0, 0.1f);

                // Show the blood vfx.
                GameObject blood = Instantiate(bloodEffect, bloodPos, Quaternion.identity, transform);

                // Destroy the player.
                Destroy(blood, 1.3f);

                // Check the player direction when he is dead.
                if (playerMovement.front)
                {
                    anim.SetBool("DeadFront", true);
                }

                else if (playerMovement.back)
                {
                    anim.SetBool("DeadBack", true);
                }

                else if (playerMovement.left)
                {
                    anim.SetBool("DeadLeft", true);
                }

                else if (playerMovement.right)
                {
                    anim.SetBool("DeadRight", true);
                }

                // No more player in the game.
                tag = "Untagged";

                // No collision / detection.
                Physics2D.IgnoreLayerCollision(8, 7);

                // Disable the functions of player.
                joystickPosition.joystick.SetActive(false);
                playerMovement.enabled = false;
                joystickPosition.enabled = false;

                StartCoroutine(WaitDead());
            }
        }
    }

    // Function to respawn the player at the respawn point
    private void RespawnPlayer()
    {
        // Reset once flag
        once = false;
        isDeadSoundPlayed = false; // Reset the dead sound played flag

        // Reset death flag
        isDead = false;

        // Show FOV.
        FOV.SetActive(true);

        // Show Self Light.
        selfLight.SetActive(true);

        // Reset player's position to the respawn point
        transform.position = respawnPoint.position;

        // Reset necessary components (e.g., health, animations, etc.)
        // Reset animations
        anim.SetBool("DeadFront", false);
        anim.SetBool("DeadBack", false);
        anim.SetBool("DeadLeft", false);
        anim.SetBool("DeadRight", false);

        tag = "Player";
        Physics2D.IgnoreLayerCollision(8, 7, false);

        // Reactivate player controls
        joystickPosition.joystick.SetActive(true);
        playerMovement.enabled = true;
        joystickPosition.enabled = true;
    }

    IEnumerator WaitDead()
    {
        yield return new WaitForSeconds(waitForDead);

        // Get the current active scene name
        string currentSceneName = SceneManager.GetActiveScene().name;

        // Check if the player is not in the tutorial game scene
        if (currentSceneName != "Tutorial Level")
        {
            // Call ConvertFoodToFOD function
            currencySystem.ConvertFoodToFOD();

            // Show the result panel.
            result.SetActive(true);
        }

        // If the player is in the tutorial level, respawn the player
        if (currentSceneName == "Tutorial Level")
        {
            RespawnPlayer();
        }
    }
}
