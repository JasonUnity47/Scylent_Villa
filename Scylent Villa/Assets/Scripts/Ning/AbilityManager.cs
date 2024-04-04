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
            Invoke(nameof(ResetMushroomCooldown), 15f);
        }
    }

    public void ActivateBucketAbility()
    {
        if (bucketAvailable && !bucketCooldown)
        {
            bucketAvailable = false;
            bucketCooldown = true;
            bucketButton.SetActive(false);
            Invoke(nameof(ResetBucketCooldown), 10f);
        }
    }

    private void ResetMushroomCooldown()
    {
        mushroomCooldown = false;
        mushroomAvailable = true;
        mushroomButton.SetActive(true);
    }

    private void ResetBucketCooldown()
    {
        bucketCooldown = false;
        bucketAvailable = true;
        bucketButton.SetActive(true);
    }

    public void MushroomAvailableBool()
    {
        mushroomAvailable = true;
    }

    public void BucketAvailableBool()
    {
        bucketAvailable = true;
    }
}
