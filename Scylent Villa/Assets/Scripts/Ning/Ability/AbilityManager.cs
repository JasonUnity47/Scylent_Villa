using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    public GameObject mushroomButton;
    public GameObject bucketButton;

    private PlayerStealth playerStealth;
    

    private bool mushroomAvailable = false;
    private bool bucketAvailable = false;
    private bool mushroomCooldown = false;
    private bool bucketCooldown = false;
    private bool isPlayerInStealth = false;

    private void Start()
    {
        playerStealth = FindObjectOfType<PlayerStealth>();
        
        mushroomButton.SetActive(mushroomAvailable);
        bucketButton.SetActive(bucketAvailable);
    }

    public void ActivateMushroomAbility()
    {
        if (mushroomAvailable && !mushroomCooldown)
        {
            playerStealth.MakeInvisibleForDuration(3f);
            mushroomAvailable = false;
            mushroomCooldown = true;
            mushroomButton.SetActive(false);

            Invoke(nameof(MushroomEnd), 3f);

            Invoke(nameof(ResetMushroomCooldown), 15f);

            // Set the player to be in stealth
            SetPlayerStealth(true);
        }
    }

    public void ActivateBucketAbility()
    {
        if (bucketAvailable && !bucketCooldown)
        {
            bucketAvailable = false;
            bucketCooldown = true;
            bucketButton.SetActive(false);
            

            Invoke(nameof(BucketEnd), 3f);

            Invoke(nameof(ResetBucketCooldown), 10f);


        }
    }

    private void BucketEnd()
    {
        
    }

    private void MushroomEnd()
    {
        // Reset the player's stealth status
        SetPlayerStealth(false);
    }

    private void ResetMushroomCooldown()
    {
        mushroomCooldown = false;
    }

    private void ResetBucketCooldown()
    {
        bucketCooldown = false;
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
