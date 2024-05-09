using System.Xml.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputButton : MonoBehaviour
{
    // Hide Panel
    public void BackStep(GameObject panel)
    {
        // Play ui sound.
        FindObjectOfType<AudioManager>().Play("Click");

        // If panel is active.
        if (panel.activeSelf)
        {
            // Hide panel.
            panel.SetActive(false);

            // Unfreeze time.
            TimeResume();
        }

        return;
    }

    // Show Settings Panel
    public void ShowSettings(GameObject settings)
    {
        // Play ui sound.
        FindObjectOfType<AudioManager>().Play("Click");

        // If panel is not active.
        if (!settings.activeSelf)
        {
            // Show panel.
            settings.SetActive(true);

            // Freeze time.
            TimeStop();
        }

        return;
    }

    // Show Tutorial Panel
    public void ShowTutorial(GameObject tutorial)
    {
        // Play ui sound.
        FindObjectOfType<AudioManager>().Play("Click");

        // If panel is not active.
        if (!tutorial.activeSelf)
        {
            // Show panel.
            tutorial.SetActive(true);

            // Freeze time.
            TimeStop();
        }

        return;
    }

    // Show Quit Panel
    public void ShowQuit(GameObject quit)
    {
        // Play ui sound.
        FindObjectOfType<AudioManager>().Play("Click");

        // If panel is not active.
        if (!quit.activeSelf)
        {
            // Show panel.
            quit.SetActive(true);

            // Freeze time.
            TimeStop();
        }

        return;
    }

    // Show Credit Panel
    public void ShowCredit(GameObject credit)
    {
        // Play ui sound.
        FindObjectOfType<AudioManager>().Play("Click");

        // If panel is not active.
        if (!credit.activeSelf)
        {
            // Show panel.
            credit.SetActive(true);

            // Freeze time.
            TimeStop();
        }

        return;
    }

    // Show In-Game Settings
    public void ShowInGame(GameObject inGame)
    {
        // Play ui sound.
        FindObjectOfType<AudioManager>().Play("Click");

        // If panel is not active.
        if (!inGame.activeSelf)
        {
            // Show panel.
            inGame.SetActive(true);

            // Freeze time.
            TimeStop();
        }

        return;
    }

    // Resume Game
    public void ResumeGame(GameObject inGame)
    {
        // Play ui sound.
        FindObjectOfType<AudioManager>().Play("Click");

        // If panel is active.
        if (inGame.activeSelf)
        {
            // Hide panel.
            inGame.SetActive(false);
            
            // Unfreeze time.
            TimeResume();
        }

        return;
    }

    // Back to Main
    public void BackMain()
    {
        // Play ui sound.
        FindObjectOfType<AudioManager>().Play("Click");

        // Load to the main mene scene.
        SceneManager.LoadScene(0);

        // Unfreeze time.
        TimeResume();

        return;
    }

    // Start Game
    public void StartGame()
    {
        // Play ui sound.
        FindObjectOfType<AudioManager>().Play("Click");

        // Load to the main cutscene.
        SceneManager.LoadScene(2);

        // Unfreeze time.
        TimeResume();

        return;
    }

    // Start Tutorial
    public void StartTutorial()
    {
        // Play ui sound.
        FindObjectOfType<AudioManager>().Play("Click");

        // Load to the tutorial level.
        SceneManager.LoadScene(1);

        // Unfreeze time.
        TimeResume();

        return;
    }

    // Skip Cutscene.
    public void SkipCutscene()
    {
        // Play ui sound.
        FindObjectOfType<AudioManager>().Play("Click");

        // Load to the standard level.
        SceneManager.LoadScene(3);

        // Unfreeze time.
        TimeResume();

        return;
    }

    // Quit Game
    public void QuitGame()
    {
        // Play ui sound.
        FindObjectOfType<AudioManager>().Play("Click");

        // Close the game.
        Application.Quit();

        return;
    }

    // Restart Game
    public void RestartGame()
    {
        // Play ui sound.
        FindObjectOfType<AudioManager>().Play("Click");

        float fodAmount = SaveSystem.LoadFodSave();

        if (fodAmount >= 5)
        {
            // Restart the game.
            SceneManager.LoadScene("Standard Level");
            Time.timeScale = 1;
        }

        return;
    }

    public void ShowShop(GameObject shop)
    {
        // Play ui sound.
        FindObjectOfType<AudioManager>().Play("Click");

        // If panel is not active.
        if (!shop.activeSelf)
        {
            // Show panel.
            shop.SetActive(true);

            // Freeze time.
            TimeStop();
        }

        return;
    }

    public void ShowRemark(GameObject panel)
    {
        // Play ui sound.
        FindObjectOfType<AudioManager>().Play("Click");

        // If panel is not active.
        if (!panel.activeSelf)
        {
            // Show panel.
            panel.SetActive(true);

            // Freeze time.
            TimeStop();
        }

        return;
    }

    // Freeze Time
    void TimeStop()
    {
        // Set the time speed to 0.
        Time.timeScale = 0;

        return;
    }

    // Unfreeze Time
    void TimeResume()
    {
        // Set the time speed to normal.
        Time.timeScale = 1;

        return;
    }
}
