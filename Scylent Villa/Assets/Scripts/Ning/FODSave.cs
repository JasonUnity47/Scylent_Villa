using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FODSave : MonoBehaviour
{
    // Declaration
    // UI Panel
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI heartText;
    [SerializeField] private TextMeshProUGUI fodText;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private GameObject countdownPanel;

    // Save
    private float heart = 0;

    private float saveFOD;
    // Timer
    [Header("Timer")]
    private Coroutine countdownCoroutine;
    [SerializeField] private float defaultTime;
    [SerializeField] private float remainingTime;


    private void Start()
    {
        heart = SaveSystem.LoadHeart();

        saveFOD = SaveSystem.LoadFodSave();

        // Load the remaining countdown time and calculate elapsed time since the game was last closed.
        LoadAndAdjustCountdown();

        // Display the FOD save value in UI.
        UpdateHeartUI();

        // Check if we should start the countdown timer.
        CheckAndStartTimer();

        // Subscribe to sceneUnloaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

   
    private void LoadAndAdjustCountdown()
    {
        // Load remaining countdown time.
        remainingTime = PlayerPrefs.GetFloat("RemainingCountdownTime");

        // Calculate elapsed time since last close.
        if (PlayerPrefs.HasKey("LastCloseTime"))
        {
            long lastCloseTime = PlayerPrefs.GetInt("LastCloseTime");
            long currentTime = GetCurrentTimestamp();
            long elapsedSeconds = currentTime - lastCloseTime;

            // Round elapsed seconds to avoid precision issues.
            elapsedSeconds = (long)Mathf.Round(elapsedSeconds);

            // Subtract elapsed time from remaining time.
            remainingTime -= elapsedSeconds;
        }

        // Handle recovery if remaining time is zero or less.
        if (remainingTime <= 0)
        {
            HandleRecovery();
        }
    }

    private void HandleRecovery()
    {
        float recoveryTime = defaultTime;

        while (remainingTime <= 0)
        {
            remainingTime += recoveryTime;

            if (heart < 5)
            {
                heart++;
            }
        }

        // Save updated FOD save value.
        SaveSystem.SaveHeart(heart);
        // Update UI for FOD save value and remaining time.
        UpdateHeartUI();
        // Reset remaining time.
        remainingTime = defaultTime;
        // Check and start the timer if needed.
        CheckAndStartTimer();
    }

    private void UpdateHeartUI()
    {
        // Update FOD save text.
        heartText.text = heart.ToString();

        fodText.text = saveFOD.ToString("F2");
        // Calculate minutes and seconds for the remaining time.
        float minutes = Mathf.FloorToInt(remainingTime / 60);
        float seconds = remainingTime % 60;

        // Update remaining time text.
        UpdateTimerUI(minutes, seconds);
    }

    private void StartCountdownTimer()
    {
        if (countdownCoroutine != null)
        {
            StopCoroutine(countdownCoroutine);
        }

        countdownCoroutine = StartCoroutine(CountdownTimer());

        // Show panel.
        countdownPanel.SetActive(true);
    }

    private void StopCountdownTimer()
    {
        if (countdownCoroutine != null)
        {
            StopCoroutine(countdownCoroutine);
            countdownCoroutine = null;
        }

        // Hide panel.
        countdownPanel.SetActive(false);

        // Reset timer text to null.
        timerText.text = string.Empty;
    }

    private IEnumerator CountdownTimer()
    {
        while (remainingTime > 0)
        {
            // Calculate minutes and seconds for remaining time.
            float minutes = Mathf.FloorToInt(remainingTime / 60);
            float seconds = remainingTime % 60;

            // Update timer UI.
            UpdateTimerUI(minutes, seconds);

            // Decrease remaining time by time elapsed (using Time.unscaledDeltaTime to prevent pausing).
            remainingTime -= Time.unscaledDeltaTime;

            // Yield for the next frame.
            yield return null;
        }

        // Countdown finished, handle recovery.
        HandleRecovery();
    }

    private void CheckAndStartTimer()
    {
        if (heart < 5 && remainingTime > 0)
        {
            StartCountdownTimer();
        }
        else
        {
            StopCountdownTimer();
        }
    }

    private void UpdateTimerUI(float minutes, float seconds)
    {
        timerText.text = $"{minutes:00}:{seconds:00}";
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus)
        {
            // Save remaining time and last close time when losing focus.
            PlayerPrefs.SetFloat("RemainingCountdownTime", remainingTime);
            PlayerPrefs.SetInt("LastCloseTime", (int)GetCurrentTimestamp());
        }
    }

    private void OnSceneLoaded(Scene loadedScene, LoadSceneMode mode)
    {
        // Define the name of your game scene.
        string gameSceneName = "Standard Level";

        // Check if the loaded scene is the game scene.
        if (loadedScene.name == gameSceneName)
        {
            // Save remaining time and last close time.
            PlayerPrefs.SetFloat("RemainingCountdownTime", remainingTime);
            PlayerPrefs.SetInt("LastCloseTime", (int)GetCurrentTimestamp());

            // Unsubscribe from the event to avoid multiple saves.
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

    private long GetCurrentTimestamp()
    {
        return (long)(System.DateTime.UtcNow - new System.DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds;
    }
}