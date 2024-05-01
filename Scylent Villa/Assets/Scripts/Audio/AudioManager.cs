using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    // Declaration
    // Sound References
    public Sound[] sounds;

    // This gameobject itself.
    public static AudioManager instance;

    // Reference to the volume slider
    public Slider volumeSlider;

    // References to the Sound On and Sound Off buttons
    public Button soundOnButton;
    public Button soundOffButton;

    // Variable to store the last saved volume
    private float lastVolume;

    private void Awake()
    {
        // If the scene doesn't have a Audio Manager.
        if (instance == null)
        {
            // Set this gameobject as the Audio Manager in the scene.
            instance = this;
        }

        else
        {
            // Destroy this gameobject if the scene already has a Audio Manager.
            Destroy(gameObject);

            return;
        }

        // Make this gameobject keep when switching scenes.
        DontDestroyOnLoad(this);

        // Assignment for each sound.
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }

        // Attach the OnVolumeChanged method to the slider's onValueChanged event
        if (volumeSlider != null)
        {
            volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
        }

        // Load the saved volume value and set it as the initial slider value
        float savedVolume = SaveSystem.LoadVolume();
        volumeSlider.value = savedVolume;
        lastVolume = savedVolume; // Store the saved volume
        OnVolumeChanged(savedVolume);
        // Load and set the button states
        LoadButtonStates();
    }

    // Play sound.
    public void Play(string name)
    {
        // Find the sound.
        Sound s = Array.Find(sounds, sound => sound.name == name);
        
        // If no specific sound in the scene.
        if (s == null)
        {
            // Show error.
            Debug.LogWarning("Sound: " + name + " not found!");
            
            return;
        }

        // Play sound.
        s.source.Play();

        return;
    }

    // Play sound.
    public void Stop(string name)
    {
        // Find the sound.
        Sound s = Array.Find(sounds, sound => sound.name == name);

        // If no specific sound in the scene.
        if (s == null)
        {
            // Show error.
            Debug.LogWarning("Sound: " + name + " not found!");

            return;
        }

        // Play sound.
        s.source.Stop();

        return;
    }
    // This method is called whenever the slider value changes
    void OnVolumeChanged(float volume)
    {
        // Update the volume of all audio sources in the sounds array
        foreach (Sound s in sounds)
        {
            s.source.volume = volume;
        }

        // Update button states
        soundOffButton.gameObject.SetActive(false);
        soundOnButton.gameObject.SetActive(true);

        // Save button states
        SaveButtonStates();

        // Save the current volume setting
        SaveSystem.SaveVolume(volume);

        // Update lastVolume variable
        lastVolume = volume;
    }

    // Method to mute sound
    public void SoundOff()
    {
        volumeSlider.value = 0; // Set slider to 0
        foreach (Sound s in sounds)
        {
            s.source.volume = 0; // Directly set the volume
        }

        // Update button states
        soundOnButton.gameObject.SetActive(false);
        soundOffButton.gameObject.SetActive(true);

        // Save button states
        SaveButtonStates();
    }

    // Method to unmute sound
    public void SoundOn()
    {
        volumeSlider.value = lastVolume; // Set slider to last saved volume
        OnVolumeChanged(lastVolume); // Update volume of audio sources

        // Update button states
        soundOffButton.gameObject.SetActive(false);
        soundOnButton.gameObject.SetActive(true);

        // Save button states
        SaveButtonStates();
    }

    // Save button states using PlayerPrefs
    void SaveButtonStates()
    {
        PlayerPrefs.SetInt("SoundOnButtonActive", soundOnButton.gameObject.activeSelf ? 1 : 0);
        PlayerPrefs.SetInt("SoundOffButtonActive", soundOffButton.gameObject.activeSelf ? 1 : 0);
    }

    // Load button states using PlayerPrefs
    void LoadButtonStates()
    {
        bool soundOnActive = PlayerPrefs.GetInt("SoundOnButtonActive", 0) == 1;
        bool soundOffActive = PlayerPrefs.GetInt("SoundOffButtonActive", 1) == 1;

        soundOnButton.gameObject.SetActive(soundOnActive);
        soundOffButton.gameObject.SetActive(soundOffActive);
    }

    // Call save on application quit
    void OnApplicationQuit()
    {
        SaveButtonStates();
    }
    // Using below code to play the specific sound you want.
    // FindObjectOfType<AudioManager>().Play("Sound Name");
}
