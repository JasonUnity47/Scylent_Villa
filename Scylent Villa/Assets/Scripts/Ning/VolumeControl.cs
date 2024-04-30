using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    // Declaration
    public Slider volumeSlider;
    //public AudioSource audioSource; // Reference to the AudioSource component you want to control the volume.

    void Start()
    {
        // Load the saved volume value.
        float savedVolume = SaveSystem.LoadVolume();

        // Set the initial value of the slider to match the saved volume.
        volumeSlider.value = savedVolume;

        // Set the volume of the audio source to match the saved volume.
        //audioSource.volume = savedVolume;

        // Add a listener for when the slider value changes.
        volumeSlider.onValueChanged.AddListener(delegate { OnVolumeChanged(); });
    }

    // This method is called whenever the slider value changes
    void OnVolumeChanged()
    {
        // Update the volume of the audio source to match the slider value.
        // audioSource.volume = volumeSlider.value;

        // Save the current volume setting.
        SaveSystem.SaveVolume(volumeSlider.value);
    }
}
