using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityManager : MonoBehaviour
{
    public static AbilityManager Instance { get; private set; }

    private AbilitySpawner abilitySpawner;

    public GameObject mushroomButton;
    public GameObject bucketButton;
    private Button bucketButtonComponent;
    private Button mushroomButtonComponent;

    private PlayerStealth playerStealth;
    private StunAbility stunAbility;
    private PlayerHealth playerHealth;
    
    private bool isPlayerInStealth = false;
    public float stunDuration = 3f; 
    public float stealthDuration = 3f;

    private void Start()
    {
        playerStealth = FindObjectOfType<PlayerStealth>();
        stunAbility = FindObjectOfType<StunAbility>();
        abilitySpawner = FindObjectOfType<AbilitySpawner>();
        playerHealth = FindObjectOfType<PlayerHealth>();

        mushroomButtonComponent = mushroomButton.GetComponent<Button>();
        bucketButtonComponent = bucketButton.GetComponent<Button>();

        mushroomButtonComponent.interactable = true;
        bucketButtonComponent.interactable = false;
        mushroomButton.SetActive(false);
        bucketButton.SetActive(false);
    }

    private void Update()
    {
        UpdateBucketButtonInteractability();
        if (playerHealth.isDead)
        {
            bucketButtonComponent.interactable = false;
            mushroomButtonComponent.interactable = false;
        }
    }

    private void UpdateBucketButtonInteractability()
    {
        bucketButtonComponent.interactable = stunAbility.CanUseStunAbility();
    }

    public void ActivateMushroomAbility()
    {
        
            playerStealth.MakeInvisibleForDuration(stealthDuration);
            abilitySpawner.RemoveAbility();
            // Set the player to be in stealth
            SetPlayerStealth(true);
            Invoke(nameof(MushroomEnd), stealthDuration);
           
            mushroomButton.SetActive(false);

        
    }

    public void ActivateBucketAbility()
    {
        
            stunAbility.UseStunAbility(stunDuration);
            abilitySpawner.RemoveAbility();
            
            
            bucketButton.SetActive(false);
        
    }

    

    private void MushroomEnd()
    {
        // Reset the player's stealth status
        SetPlayerStealth(false);
    }

    public void MushroomAvailableBool()
    {
        
        mushroomButton.SetActive(true);
    }

    public void BucketAvailableBool()
    {
        
        bucketButton.SetActive(true);
    }

    public bool IsPlayerInStealth()
    {
        return isPlayerInStealth;
    }

    public void SetPlayerStealth(bool isInStealth)
    {
        isPlayerInStealth = isInStealth;
    }
}
