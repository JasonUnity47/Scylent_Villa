using System.Collections;
using UnityEngine;

public class TimerSystem : MonoBehaviour
{
    // Declaration
    // Timer
    [Header("Timer")]
    public float timer;
    public float survivalTime = 0f; // Total survival time in seconds
    public float fodInterval = 300f; // Time interval for awarding FOD in seconds (5 minutes)

    // Object Reference
    [Header("Object Reference")]
    [SerializeField] private GameObject uIFOD;

    // Script Reference
    private CurrencySystem currencySystem;
    private CurrencyUI currencyUI;

    private void Start()
    {
        // Hide ui FOD panel.
        uIFOD.SetActive(false);

        // Get reference.
        currencySystem = GetComponent<CurrencySystem>();
        currencyUI = FindObjectOfType<CurrencyUI>();
        
        // Initialize the timer.
        timer = 0;
    }

    private void Update()
    {
        // The time in the game keep increasing over time.
        timer += Time.deltaTime;
        survivalTime += Time.deltaTime;

        // If the survival time reach to the predefined time.
        if (survivalTime >= fodInterval)
        {
            // Player will get a FOD as a reward.
            AwardFOD();
        }
    }

    private void AwardFOD()
    {
        // Increase the FOD count.
        currencySystem.fodCount++;

        // Update the ui panel.
        currencyUI.UpdateTotalFODUI(currencySystem.fodCount);

        // Reset survival time after awarding FOD.
        survivalTime = 0f;

        // Set a timer to show ui panel.
        StartCoroutine(ActivateFODUI());
    }

    private IEnumerator ActivateFODUI()
    {
        // Show panel.
        uIFOD.SetActive(true);

        yield return new WaitForSeconds(5f);

        // Hide panel.
        uIFOD.SetActive(false);
    }
}
