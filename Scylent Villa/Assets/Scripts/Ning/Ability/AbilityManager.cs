using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    public GameObject mushroomButton;
    public GameObject bucketButton;

    private PlayerStealth playerStealth;
    private StunAbility stunAbility;

    private bool mushroomAvailable = false;
    private bool bucketAvailable = false;
    private bool isPlayerInStealth = false;
    public float stunDuration = 3f; 

    private void Start()
    {
        playerStealth = FindObjectOfType<PlayerStealth>();
        stunAbility = FindObjectOfType<StunAbility>();

        mushroomButton.SetActive(mushroomAvailable);
        bucketButton.SetActive(bucketAvailable);
    }

    public void ActivateMushroomAbility()
    {
        if (mushroomAvailable)
        {
            playerStealth.MakeInvisibleForDuration(stunDuration);
            mushroomAvailable = false;
            
            mushroomButton.SetActive(false);

            // Set the player to be in stealth
            SetPlayerStealth(true);

            Invoke(nameof(MushroomEnd), 3f);

            
        }
    }

    public void ActivateBucketAbility()
    {
        if (bucketAvailable)
        {
            stunAbility.UseStunAbility(3f);
            bucketAvailable = false;
            
            bucketButton.SetActive(false);
        }
    }

    

    private void MushroomEnd()
    {
        // Reset the player's stealth status
        SetPlayerStealth(false);
    }

    public void MushroomAvailableBool()
    {
        mushroomAvailable = true;
        mushroomButton.SetActive(true);
    }

    public void BucketAvailableBool()
    {
        bucketAvailable = true;
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
