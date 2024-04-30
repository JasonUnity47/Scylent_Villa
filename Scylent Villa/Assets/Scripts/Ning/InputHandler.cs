using UnityEngine;

public class InputHandler : MonoBehaviour
{
    // Object Reference
    [Header("Object Reference")]
    [SerializeField] private GameObject resultUI;
    [SerializeField] private GameObject conversionUI;

    private void Update()
    {
        // Check if the result UI is active.
        if (resultUI.activeSelf)
        {
            // Check for tap input.
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                // Toggle visibility of result UI and conversion UI.
                resultUI.SetActive(false);
                conversionUI.SetActive(true);
            }
        }
    }
}