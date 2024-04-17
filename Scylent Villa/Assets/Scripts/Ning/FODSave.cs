using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FODSave : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI fodSaveText;
    

    private void Start()
    {
       

        
        // Display fodSave value in UI
        UpdateFodSaveUI();
       
    }

    // Function to update the UI with the current fodSave value
    private void UpdateFodSaveUI()
    {
        // Get the current fodSave value from SaveSystem
        float fodSave = SaveSystem.LoadFodSave();

        // Update the UI text with the fodSave value
        fodSaveText.text = fodSave.ToString("F2");
    }
}

