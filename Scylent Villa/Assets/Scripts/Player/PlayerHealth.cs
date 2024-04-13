using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public JoystickPosition joystickPosition;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
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
            }
        }
    }
}
