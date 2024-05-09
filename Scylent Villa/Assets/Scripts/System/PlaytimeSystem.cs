using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaytimeSystem : MonoBehaviour
{
    // Declaration
    // Variable
    private float heartAmount;

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
        heartAmount = SaveSystem.LoadHeart();
    }

    public void CheckFOD(GameObject errorPanel)
    {
        // If the FOD amount is less than 1 then reject the player to play the game and show error message.
        if (heartAmount < 1)
        {
            if (!errorPanel.activeSelf)
            {
                errorPanel.SetActive(true);
            }

            return;
        }

        // Else if the fod amount is equal or more than 1 then enable player to play the game by reducing 1 FOD.
        else if (heartAmount >= 1)
        {
            heartAmount--;

            SaveSystem.SaveHeart(heartAmount);

            inputButton.StartGame();
        }
    }
}
