using UnityEngine;

public class CurrencySystem : MonoBehaviour
{
    // Declaration
    // FOD
    [Header("FOD")]
    public float fodCount = 0.0f;
    private float fodSave;

    // Food
    [Header("Food")]
    public int totalCurrencyEarned = 0;

    // Object Reference
    [Header("Object Reference")]
    [SerializeField] private CurrencyUI currencyUI;

    private void Start()
    {
        // Initialize food and FOD count.
        totalCurrencyEarned = 0;
        fodCount = 0;

        // Load saved fodSave
        LoadFodSave();
    }

    private void OnApplicationQuit()
    {
        // Save fodSave when application quits.
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
        // Get reference.
        currencyUI.ResultCurrencyUI(totalCurrencyEarned);
        currencyUI.ResultFODUI(fodCount);
        currencyUI.CurrentFODUI(fodCount);

        int foodTotal = totalCurrencyEarned;

        currencyUI.ConvertCurrencyUI(foodTotal);

        // Define exchange rate: 1 food = 0.01 FOD
        float exchangeRate = 0.01f;

        // Calculate FOD to be added based on remaining food and exchange rate.
        float fodToAdd = foodTotal * exchangeRate;

        currencyUI.ConvertFODUI(fodToAdd);

        // Add FOD to fodCount.
        fodCount += fodToAdd;

        currencyUI.TotalFODUI(fodCount);

        fodSave += fodCount;

        // Save fodSave after adding FOD.
        SaveFodSave();
    }
}