using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputButton : MonoBehaviour
{
    // Back
    public void BackStep(GameObject panel)
    {
        FindObjectOfType<AudioManager>().Play("Click");

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
        FindObjectOfType<AudioManager>().Play("Click");

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
        FindObjectOfType<AudioManager>().Play("Click");

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
        FindObjectOfType<AudioManager>().Play("Click");

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
        FindObjectOfType<AudioManager>().Play("Click");

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
        FindObjectOfType<AudioManager>().Play("Click");

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
        FindObjectOfType<AudioManager>().Play("Click");

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
        FindObjectOfType<AudioManager>().Play("Click");

        SceneManager.LoadScene(0);
        return;
    }

    // Start
    public void StartGame()
    {
        FindObjectOfType<AudioManager>().Play("Click");

        SceneManager.LoadScene(2);
        TimeResume();
        return;
    }

    // Tutorial
    public void StartTutorial()
    {
        FindObjectOfType<AudioManager>().Play("Click");

        SceneManager.LoadScene(1);
        TimeResume();
        return;
    }

    // Quit
    public void QuitGame()
    {
        FindObjectOfType<AudioManager>().Play("Click");

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
