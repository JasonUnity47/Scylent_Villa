using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputButton : MonoBehaviour
{
    // Back
    public void BackStep(GameObject panel)
    {
        if (panel.activeSelf)
        {
            panel.SetActive(false);
            TimeResume();
        }

        return;
    }

    // Settings
    public void ShowSettings(GameObject settings)
    {
        if (!settings.activeSelf)
        {
            settings.SetActive(true);
            TimeStop();
        }

        return;
    }

    // Control
    public void ShowTutorial(GameObject tutorial)
    {
        if (!tutorial.activeSelf)
        {
            tutorial.SetActive(true);
            TimeStop();
        }

        return;
    }

    // Quit
    public void ShowQuit(GameObject quit)
    {
        if (!quit.activeSelf)
        {
            quit.SetActive(true);
            TimeStop();
        }

        return;
    }

    // Credit
    public void ShowCredit(GameObject credit)
    {
        if (!credit.activeSelf)
        {
            credit.SetActive(true);
            TimeStop();
        }

        return;
    }

    // In-Game Settings
    public void ShowInGame(GameObject inGame)
    {
        if (!inGame.activeSelf)
        {
            inGame.SetActive(true);
            TimeStop();
        }

        return;
    }

    // Resume Game
    public void ResumeGame(GameObject inGame)
    {
        if (inGame.activeSelf)
        {
            inGame.SetActive(false);
            TimeResume();
        }

        return;
    }

    // Back to Main
    public void BackMain()
    {
        SceneManager.LoadScene(0);
        return;
    }

    // Start
    public void StartGame()
    {
        SceneManager.LoadScene(2);
        TimeResume();
        return;
    }

    // Tutorial
    public void StartTutorial()
    {
        SceneManager.LoadScene(1);
        TimeResume();
        return;
    }

    // Quit
    public void QuitGame()
    {
        Application.Quit();
        return;
    }

    // Freeze Time
    void TimeStop()
    {
        Time.timeScale = 0;
        return;
    }

    // Unfreeze Time
    void TimeResume()
    {
        Time.timeScale = 1;
        return;
    }
}
