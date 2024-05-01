using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AbilityManager : MonoBehaviour
{
    // Declaration
    // Self
    public static AbilityManager Instance { get; private set; }

    // Script Reference
    private AbilitySpawner abilitySpawner;
    private TutorialSpawner tutorialSpawner;

    // Button
    [Header("Button")]
    public GameObject mushroomButton;
    public GameObject bucketButton;
    private Button bucketButtonComponent;
    private Button mushroomButtonComponent;

    // Script Reference
    private PlayerStealth playerStealth;
    private StunAbility stunAbility;
    private PlayerHealth playerHealth;

    // Ability Value
    [Header("Ability Value")]
    private bool isPlayerInStealth = false;
    public float stunDuration = 3f; 
    public float stealthDuration = 3f;

    private void Start()
    {
        // Get reference.
        playerStealth = FindObjectOfType<PlayerStealth>();
        stunAbility = FindObjectOfType<StunAbility>();
        abilitySpawner = FindObjectOfType<AbilitySpawner>();
        playerHealth = FindObjectOfType<PlayerHealth>();
        tutorialSpawner = FindObjectOfType<TutorialSpawner>();

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

        // Check whether the player is dead.
        if (playerHealth.isDead)
        {
            // Disable the button function.
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
        FindObjectOfType<AudioManager>().Play("Stealth");
        playerStealth.MakeInvisibleForDuration(stealthDuration);

        // Get the current active scene name.
        string currentSceneName = SceneManager.GetActiveScene().name;

        // Check if the player is not in the tutorial game scene.
        if (currentSceneName != "Tutorial Level")
        {
            abilitySpawner.RemoveAbility();
        }

        else
        {
            tutorialSpawner.RemoveMSpawn();
        }

        // Set the player to be in stealth.
        SetPlayerStealth(true);
        Invoke(nameof(MushroomEnd), stealthDuration);
        
        // Hide button.
        mushroomButton.SetActive(false);
    }

    public void ActivateBucketAbility()
    {
        FindObjectOfType<AudioManager>().Play("Stun");
        stunAbility.UseStunAbility(stunDuration);

        // Get the current active scene name.
        string currentSceneName = SceneManager.GetActiveScene().name;

        // Check if the player is not in the tutorial game scene.
        if (currentSceneName != "Tutorial Level")
        {
            abilitySpawner.RemoveAbility();
        }

        else
        {
            tutorialSpawner.RemoveBSpawn();
        }
        
        // Hide button.
        bucketButton.SetActive(false);
    }

    private void MushroomEnd()
    {
        // Reset the player's stealth status.
        SetPlayerStealth(false);
    }

    public void MushroomAvailableBool()
    {
        // Show button.
        mushroomButton.SetActive(true);
    }

    public void BucketAvailableBool()
    {
        // Show button.
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
