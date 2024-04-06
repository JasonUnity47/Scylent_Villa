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

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (isDead && !once)
        {
            once = true;

            Physics2D.IgnoreLayerCollision(8, 7);
            playerMovement.enabled = false;
            rb.velocity = Vector2.zero;
            anim.SetBool("DeadBool", true);
        }
    }
}
