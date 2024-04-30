using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    // Declaration
    public bool isDead = false;
    private bool once = false;

    private Rigidbody2D rb;
    private Animator anim;
    private PlayerMovement playerMovement;
    [SerializeField] private GameObject FOV;
    [SerializeField] private GameObject bloodEffect;
    [SerializeField] private GameObject result;
    [SerializeField] private GameObject selfLight;

    public JoystickPosition joystickPosition;
    private CurrencySystem currencySystem;

    // Define respawn point as a Transform
    [SerializeField] private Transform respawnPoint;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        currencySystem = FindObjectOfType<CurrencySystem>();
    }

    private void Update()
    {
        if (isDead)
        {
            rb.velocity = Vector2.zero;

            if (!once)
            {
                once = true;

                FOV.SetActive(false);

                selfLight.SetActive(false);

                Vector2 bloodPos = (Vector2)transform.position + new Vector2(0, 0.1f);

                GameObject blood = Instantiate(bloodEffect, bloodPos, Quaternion.identity, transform);

                Destroy(blood, 1.3f);

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

                tag = "Untagged";

                Physics2D.IgnoreLayerCollision(8, 7);

                joystickPosition.joystick.SetActive(false);
                playerMovement.enabled = false;
                joystickPosition.enabled = false;


                // Get the current active scene name
                string currentSceneName = SceneManager.GetActiveScene().name;

                // Check if the player is not in the tutorial game scene
                if (currentSceneName != "Tutorial Level")
                {
                    // Call ConvertFoodToFOD function
                    currencySystem.ConvertFoodToFOD();
                    result.SetActive(true);
                }

                // If the player is in the tutorial level, respawn the player
                if (currentSceneName == "Tutorial Level")
                {
                    StartCoroutine(WaitRespawn());
                }
            }
        }
    }

    // Function to respawn the player at the respawn point
    private void RespawnPlayer()
    {
        // Reset once flag
        once = false;
        // Reset death flag
        isDead = false;

        FOV.SetActive(true);

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

    IEnumerator WaitRespawn()
    {
        yield return new WaitForSeconds(2f);
        RespawnPlayer();
    }
}
