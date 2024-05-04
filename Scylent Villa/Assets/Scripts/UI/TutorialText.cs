using UnityEngine;

public class TutorialText : MonoBehaviour
{
    // Declaration
    // Reference to the UI panel
    public GameObject uiPanel;

    private void OnDestroy()
    {
        // Reset uiPanel or handle any cleanup necessary.
        uiPanel = null;
    }


    // This method is called when another collider enters the trigger area.
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collider belongs to the player.
        if (other.CompareTag("Player"))
        {
            // Show the UI panel.
            uiPanel.SetActive(true);
        }

        // Check if this object is End Point.
        if (this.gameObject.name == "End Point")
        {
            // Show the UI panel.
            uiPanel.SetActive(true);
            FindObjectOfType<AudioManager>().Stop("Heartbeat"); // Stop the heartbeat sound.
        }
    }

    // This method is called when another collider exits the trigger area.
    private void OnTriggerExit2D(Collider2D other)
    {
        // Check if the collider belongs to the player.
        if (other.CompareTag("Player"))
        {
            // Check if uiPanel is not null before setting it inactive.
            if (uiPanel != null)
            {
                // Hide the UI panel.
                uiPanel.SetActive(false);
            }
        }
    }

}
