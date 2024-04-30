using UnityEngine;

public class UnhighlightObject : MonoBehaviour
{
    // Declaration
    // Object Reference
    public GameObject unhighlightObject;

    // Script Reference
    private PlayerHealth playerHealth;

    private void Start()
    {
        // Check whether a gameobject named Player is exist in the scene.
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            // Get reference.
            playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        }
    }

    private void Update()
    {
        // Check whether the playerHealth script is exist in the scene.
        if (playerHealth != null)
        {
            // Check whether the player is dead and the FOV object is active.
            if (playerHealth.isDead && unhighlightObject.activeSelf)
            {
                // Hide FOV.
                unhighlightObject.SetActive(false);
            }
        }
    }
}
