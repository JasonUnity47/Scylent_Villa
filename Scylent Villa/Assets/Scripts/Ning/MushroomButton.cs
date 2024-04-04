using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MushroomButton : MonoBehaviour
{
    private AbilityManager abilityManager;

    private void Awake()
    {
        abilityManager = FindObjectOfType<AbilityManager>();
    }

    public void ActivateMushroomAbility()
    {
        abilityManager.ActivateMushroomAbility();
    }
}
