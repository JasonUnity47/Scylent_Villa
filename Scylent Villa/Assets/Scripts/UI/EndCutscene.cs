using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndCutscene : MonoBehaviour
{
    // Set a timer to load the standard level.
    IEnumerator WaitEnd()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(3);
    }

    // If trigger is active by colliding something which is the stranger in the cutscene.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Start the timer and load to the standard level.
        StartCoroutine(WaitEnd());
    }
}
