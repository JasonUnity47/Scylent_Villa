using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoystickPosition : MonoBehaviour
{
    // Declaration
    public GameObject joystick;
    public RectTransform MainCanvas;
    Touch touchMovement;

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            touchMovement = Input.GetTouch(0); // First touch.

            // Touch 1 time to turn on the joystick.
            if (touchMovement.phase == TouchPhase.Began)
            {
                joystick.SetActive(true);

                Vector2 touchPos; // touch point position.
                RectTransformUtility.ScreenPointToLocalPointInRectangle(MainCanvas, touchMovement.position, null, out touchPos); // CONVERT screen pixel position TO canvas position.

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