using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerSystem : MonoBehaviour
{
    // Declaration
    public float timer;
    public float survivalTime = 0f; // Total survival time in seconds
    public float fodInterval = 300f; // Time interval for awarding FOD in seconds (5 minutes)
    private CurrencySystem currencySystem;
    private CurrencyUI currencyUI;
    [SerializeField] private GameObject uIFOD;

    private void Start()
    {
        uIFOD.SetActive(false);
        currencySystem = GetComponent<CurrencySystem>();
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
        currencySystem.fodCount++;
        currencyUI.UpdateTotalFODUI(currencySystem.fodCount);

        // Reset survivalTime after awarding FOD
        survivalTime = 0f;

        StartCoroutine(ActivateFODUI());

    }

    private IEnumerator ActivateFODUI()
    {
        
        uIFOD.SetActive(true);

        
        yield return new WaitForSeconds(5f);

        
        uIFOD.SetActive(false);
    }
}
