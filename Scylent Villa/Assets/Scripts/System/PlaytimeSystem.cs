using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaytimeSystem : MonoBehaviour
{
    // Declaration
    // Variable
    private float fodAmount;

    // Script Reference
    private InputButton inputButton;

    private void Start()
    {
        // Get reference.
        inputButton = GetComponent<InputButton>();
    }

    private void Update()
    {
        // Keep updating the fod amount that the player has.
        fodAmount = SaveSystem.LoadFodSave();
    }

    public void CheckFOD(GameObject errorPanel)
    {
        // If the FOD amount is less than 1 then reject the player to play the game and show error message.
        if (fodAmount < 1)
        {
            if (!errorPanel.activeSelf)
            {
                errorPanel.SetActive(true);
            }

            return;
        }

        // Else if the fod amount is equal or more than 1 then enable player to play the game by reducing 1 FOD.
        else if (fodAmount >= 1)
        {
            fodAmount--;

            SaveSystem.SaveFodSave(fodAmount);

            inputButton.StartGame();
        }
    }
}
