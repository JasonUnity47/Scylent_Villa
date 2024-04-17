using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CurrencyUI : MonoBehaviour
{
    public TextMeshProUGUI currencyText;
    public TextMeshProUGUI fodText;
    public TextMeshProUGUI resultFood;
    public TextMeshProUGUI resultFOD;
    public TextMeshProUGUI convertFood;
    public TextMeshProUGUI convertFOD;
    public TextMeshProUGUI finalFOD;

    void Start()
    {
        
    }

    // Method to update the currency UI with total amount
    public void UpdateTotalCurrencyUI(int totalCurrency)
    {
        // Update the text component with the total currency amount
        currencyText.text = totalCurrency.ToString();
    }

    public void UpdateTotalFODUI(float totalFOD)
    {
        
        fodText.text = totalFOD.ToString();
    }

    public void ResultCurrencyUI(int resultCurrency)
    {
        
        resultFood.text = resultCurrency.ToString();
    }

    public void ResultFODUI(float fResultFOD)
    {
        
        resultFOD.text = fResultFOD.ToString();
    }

    public void ConvertCurrencyUI(int convertCurrency)
    {
        
        convertFood.text = convertCurrency.ToString();
    }

    public void ConvertFODUI(float fConvertFOD)
    {
        
        convertFOD.text = fConvertFOD.ToString("F2");
    }

    public void TotalFODUI(float fFinalFOD)
    {
        
        finalFOD.text = fFinalFOD.ToString("F2");
    }
}
