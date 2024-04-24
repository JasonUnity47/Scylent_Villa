using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FODSave : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI fodSaveText;
    [SerializeField] private TextMeshProUGUI timerText; // Add a reference for the timer UI
    [SerializeField] private GameObject countdownPanel;

    private float fodSave;
    private Coroutine countdownCoroutine;
    [SerializeField] private float remainingTime; // Remaining time for the countdown in seconds

    private void Start()
    {
        // Load the initial FOD save value
        fodSave = SaveSystem.LoadFodSave();

        // Load the remaining countdown time and calculate elapsed time since the app was last closed
        LoadAndAdjustCountdown();

        // Display the FOD save value in UI
        UpdateFodSaveUI();

        // Check if we should start the countdown timer
        CheckAndStartTimer();
    }

    // Load the remaining countdown time and calculate elapsed time since the app was last closed
    private void LoadAndAdjustCountdown()
    {
        // Load the remaining countdown time from PlayerPrefs
        if (PlayerPrefs.HasKey("RemainingCountdownTime"))
        {
            remainingTime = PlayerPrefs.GetFloat("RemainingCountdownTime");
        }
        else
        {
            remainingTime = 3600f; // Default to 60 minutes if no data is found
        }

        // Calculate time elapsed since the app was last closed
        if (PlayerPrefs.HasKey("LastCloseTime"))
        {
            long lastCloseTime = PlayerPrefs.GetInt("LastCloseTime");
            long currentTime = GetCurrentTimestamp();
            long elapsedSeconds = currentTime - lastCloseTime;

            // Subtract the elapsed time from the remaining countdown time
            remainingTime -= elapsedSeconds;
        }

        // If remaining time is less than or equal to zero, handle recovery
        if (remainingTime <= 0)
        {
            HandleRecovery();
        }
    }

    // Handle recovery when the countdown timer has expired
    private void HandleRecovery()
    {
        fodSave += 1; // Recover 1 FOD
        SaveSystem.SaveFodSave(fodSave); // Save updated FOD save value

        // Reset remaining time and check if another timer should start
        remainingTime = 3600f; // Reset to 60 minutes
        CheckAndStartTimer();
    }

    // Function to get the current timestamp
    private long GetCurrentTimestamp()
    {
        return (long)(System.DateTime.UtcNow - new System.DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds;
    }

    // Function to update the UI with the current FOD save value
    private void UpdateFodSaveUI()
    {
        fodSaveText.text = fodSave.ToString("F2");
    }

    // Function to start the countdown timer coroutine
    private void StartCountdownTimer()
    {
        countdownCoroutine = StartCoroutine(CountdownTimer());
        countdownPanel.SetActive(true);
    }

    // Function to stop the countdown timer coroutine
    private void StopCountdownTimer()
    {
        if (countdownCoroutine != null)
        {
            StopCoroutine(countdownCoroutine);
            countdownCoroutine = null;
        }

        countdownPanel.SetActive(false);
        timerText.text = string.Empty;
    }

    // Coroutine for the countdown timer
    private IEnumerator CountdownTimer()
    {
        while (remainingTime > 0)
        {
            float minutes = Mathf.Floor(remainingTime / 60);
            float seconds = remainingTime % 60;

            UpdateTimerUI(minutes, seconds);

            remainingTime -= Time.deltaTime;
            yield return null;
        }

        // Countdown finished, handle recovery
        HandleRecovery();
    }

    // Function to check and start the timer if needed
    private void CheckAndStartTimer()
    {
        if (fodSave < 5 && remainingTime > 0)
        {
            if (countdownCoroutine == null)
            {
                StartCountdownTimer();
            }
        }
        else
        {
            StopCountdownTimer();
        }
    }

    // Function to update the timer UI text
    private void UpdateTimerUI(float minutes, float seconds)
    {
        timerText.text = $"{minutes:00}:{seconds:00}";
    }

    // Method that gets called when the application gains or loses focus
    private void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus)
        {
            // When the app loses focus, save the current timestamp and remaining countdown time
            long currentTime = GetCurrentTimestamp();
            PlayerPrefs.SetInt("LastCloseTime", (int)currentTime);
            PlayerPrefs.SetFloat("RemainingCountdownTime", remainingTime);
        }
    }
}