using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioMixerController : MonoBehaviour
{
    public AudioMixer audioMixer; // Reference to the audio mixer
    public Slider volumeSlider; // Reference to the slider
    public Button soundOnButton; // Button for unmuting
    public Button soundOffButton; // Button for muting

    private bool isMuted;

    private const string VolumeKey = "VolumeLevel";
    private const string MuteKey = "IsMuted";

    void Start()
    {
        // Load the volume level and mute state from PlayerPrefs
        LoadVolumeAndMuteState();

        // Add event listener for the slider to update volume when changed
        volumeSlider.onValueChanged.AddListener(SetVolume);

        // Add event listeners for mute and unmute buttons
        soundOnButton.onClick.AddListener(() => Unmute());
        soundOffButton.onClick.AddListener(() => Mute());

        // Update button visibility based on the mute state
        UpdateButtonVisibility();
    }

    void SetVolume(float value)
    {
        // If the volume is very low, mute the audio
        if (value < 0.0001f)
        {
            Mute();
        }
        else
        {
            Unmute();
        }

        // Calculate decibel level from the volume and set it on the audio mixer
        float decibelValue = Mathf.Log10(value) * 20;
        audioMixer.SetFloat("MyExposedParam", decibelValue);

        // Save the volume level
        PlayerPrefs.SetFloat(VolumeKey, value);
    }

    void Mute()
    {
        isMuted = true;
        // Set the audio mixer to minimum volume (mute)
        audioMixer.SetFloat("MyExposedParam", -80);
        // Save the mute state
        PlayerPrefs.SetInt(MuteKey, 1);
        // Update button visibility
        UpdateButtonVisibility();
    }

    void Unmute()
    {
        isMuted = false;
        // Unmute the audio by setting volume according to the slider value
        float value = volumeSlider.value;
        float decibelValue = Mathf.Log10(value) * 20;
        audioMixer.SetFloat("MyExposedParam", decibelValue);
        // Save the mute state
        PlayerPrefs.SetInt(MuteKey, 0);
        // Update button visibility
        UpdateButtonVisibility();
    }

    void UpdateButtonVisibility()
    {
        // Set the visibility of the buttons based on the mute state
        soundOnButton.gameObject.SetActive(isMuted);
        soundOffButton.gameObject.SetActive(!isMuted);
    }

    void LoadVolumeAndMuteState()
    {
        // Load the volume level from PlayerPrefs if it exists
        if (PlayerPrefs.HasKey(VolumeKey))
        {
            float savedVolume = PlayerPrefs.GetFloat(VolumeKey);
            volumeSlider.value = savedVolume;
        }
        else
        {
            volumeSlider.value = 1.0f; // Default volume if none saved
        }

        // Load the mute state from PlayerPrefs
        if (PlayerPrefs.HasKey(MuteKey))
        {
            isMuted = PlayerPrefs.GetInt(MuteKey) == 1;
        }
        else
        {
            isMuted = false; // Default mute state is unmuted
        }

        // Apply the mute state and update the volume level in the audio mixer
        if (isMuted)
        {
            audioMixer.SetFloat("MyExposedParam", -80);
        }
        else
        {
            float value = volumeSlider.value;
            float decibelValue = Mathf.Log10(value) * 20;
            audioMixer.SetFloat("MyExposedParam", decibelValue);
        }

        // Update button visibility based on the mute state
        UpdateButtonVisibility();
    }
}