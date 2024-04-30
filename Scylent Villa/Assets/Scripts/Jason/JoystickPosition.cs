using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickPosition : MonoBehaviour
{
    // Declaration
    // Object Refenrece
    [Header("Joystick Reference")]
    public GameObject joystick;
    public RectTransform MainCanvas;
    Touch touchMovement;

    private void Update()
    {
        // Check whether a touch is exist in the scene.
        if (Input.touchCount > 0)
        {
            // Get the touch.
            touchMovement = Input.GetTouch(0); // First touch.

            // Exit if touch is over UI element.
            // Found from Internet and works well.
            if (EventSystem.current.IsPointerOverGameObject(0))
            {
                return;
            }

            if ((touchMovement.phase == TouchPhase.Ended) || (touchMovement.phase == TouchPhase.Canceled))
            {
                return;
            }

            // Touch 1 time to turn on the joystick.
            if (touchMovement.phase == TouchPhase.Began)
            {
                joystick.SetActive(true);

                Vector2 touchPos; // Touch point position.
                RectTransformUtility.ScreenPointToLocalPointInRectangle(MainCanvas, touchMovement.position, null, out touchPos); // Convert screen pixel position to canvas position.

                joystick.transform.localPosition = touchPos;
            }

            // Tap 2 times to turn off the joystick.
            else if (touchMovement.tapCount == 2)
            {
                joystick.SetActive(false);
            }
        }
    }
}