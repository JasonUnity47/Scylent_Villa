using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyUI : MonoBehaviour
{
    public Text currencyText;

    void Start()
    {
        
    }

    // Method to update the currency UI with total amount
    public void UpdateTotalCurrencyUI(int totalCurrency)
    {
        // Update the text component with the total currency amount
        currencyText.text = totalCurrency.ToString();
    }
}
