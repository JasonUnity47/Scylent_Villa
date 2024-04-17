using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencySystem : MonoBehaviour
{
    public float fodCount = 0.0f;
    public int totalCurrencyEarned = 0;
    private float fodSave;
    
    [SerializeField] private CurrencyUI currencyUI;

    private void Start()
    {
        totalCurrencyEarned = 0;
        fodCount = 0.0f;
        // Load saved fodSave
        LoadFodSave();
    }

    private void OnApplicationQuit()
    {
        // Save fodSave when application quits
        SaveFodSave();
    }

    // Function to save fodSave
    private void SaveFodSave()
    {
        SaveSystem.SaveFodSave(fodSave);
    }

    // Function to load fodSave
    private void LoadFodSave()
    {
        fodSave = SaveSystem.LoadFodSave();
    }

    public void ConvertFoodToFOD()
    {
        currencyUI.ResultCurrencyUI(totalCurrencyEarned);
        currencyUI.ResultFODUI(fodCount);

        
        int foodTotal = totalCurrencyEarned;

        currencyUI.ConvertCurrencyUI(foodTotal);

        // Define exchange rate: 1 food = 0.01 FOD
        float exchangeRate = 0.01f;

        // Calculate FOD to be added based on remaining food and exchange rate
        float fodToAdd = foodTotal * exchangeRate;

        currencyUI.ConvertFODUI(fodToAdd);

        // Add FOD to fodCount
        fodCount += fodToAdd;

        currencyUI.TotalFODUI(fodCount);

        fodSave += fodCount;

        // Save fodSave after adding FOD
        SaveFodSave();
    }

 
}