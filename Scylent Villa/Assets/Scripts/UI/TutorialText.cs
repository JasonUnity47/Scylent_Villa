using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialText : MonoBehaviour
{
   
    // Reference to the UI panel
    public GameObject uiPanel;

    // This method is called when another collider enters the trigger area
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collider belongs to the player
        if (other.CompareTag("Player"))
        {
            // Activate the UI panel
            uiPanel.SetActive(true);
        }

        // Check if this object is End Point
        if (this.gameObject.name == "End Point")
        {
            // Activate the UI panel
            uiPanel.SetActive(true);
        }
    }

    // This method is called when another collider exits the trigger area
    private void OnTriggerExit2D(Collider2D other)
    {
        // Check if the collider belongs to the player
        if (other.CompareTag("Player"))
        {
            // Deactivate the UI panel
            uiPanel.SetActive(false);
        }
    }

}
