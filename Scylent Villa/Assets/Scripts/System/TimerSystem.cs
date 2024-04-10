using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerSystem : MonoBehaviour
{
    // Declaration
    public float timer;
    public float survivalTime = 0f; // Total survival time in seconds
    public float fodInterval = 300f; // Time interval for awarding FOD in seconds (5 minutes)
    public int fodCount = 0;
    private CurrencyUI currencyUI;

    private void Start()
    {
        currencyUI = FindObjectOfType<CurrencyUI>();
        timer = 0;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        survivalTime += Time.deltaTime;

        if (survivalTime >= fodInterval)
        {
            AwardFOD();
        }
    }

    private void AwardFOD()
    {
        fodCount++;
        currencyUI.UpdateTotalFODUI(fodCount);

        // Reset survivalTime after awarding FOD
        survivalTime = 0f;
    }
}
