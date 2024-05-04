using System.Collections;
using UnityEngine;

public class ShowSkip : MonoBehaviour
{
    // Declaration
    // Object References
    public GameObject skipButton;

    private void Start()
    {
        // Start a timer to show the skip button.
        StartCoroutine(WaitSkip());
    }

    IEnumerator WaitSkip()
    {
        yield return new WaitForSeconds(1.5f);

        // If the skip button is not active.
        if (!skipButton.activeSelf)
        {
            // Show button.
            skipButton.SetActive(true);
        }
    }
}
