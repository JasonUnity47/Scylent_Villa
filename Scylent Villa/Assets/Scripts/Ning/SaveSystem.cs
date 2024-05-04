using UnityEngine;

public class SaveSystem : MonoBehaviour
{

    // Start method that deletes fodSave when the script starts
    void Start()
    {
        //DeleteFodSave();
    }

    // Function to save fodSave
    public static void SaveFodSave(float fodSave)
    {
        PlayerPrefs.SetFloat("FodSave", fodSave);
        PlayerPrefs.Save();
    }

    // Function to load fodSave
    public static float LoadFodSave()
    {
        return PlayerPrefs.GetFloat("FodSave", 0.00f);
    }

    // Function to delete fodSave from PlayerPrefs
    public static void DeleteFodSave()
    {
        // Delete the "FodSave" key and its associated value from PlayerPrefs.
        PlayerPrefs.DeleteKey("FodSave");

        // Save the changes to PlayerPrefs.
        PlayerPrefs.Save();
    }

    // Function to save the volume setting
    public static void SaveVolume(float volume)
    {
        PlayerPrefs.SetFloat("Volume", volume);
        PlayerPrefs.Save();
    }

    // Function to load the volume setting
    public static float LoadVolume()
    {
        return PlayerPrefs.GetFloat("Volume", 1.0f); // Default volume is 1.0f (max volume) if not found.
    }
}
