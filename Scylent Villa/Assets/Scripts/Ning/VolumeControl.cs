using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    public Slider volumeSlider;
    // List of audio sources you want to control
    public List<AudioSource> audioSources;



    void Start()
    {


        // Add a listener for when the slider value changes
        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);

        // Load the saved volume value
        float savedVolume = SaveSystem.LoadVolume();


        // Set the initial value of the slider to match the saved volume
        volumeSlider.value = savedVolume;

        // Set the volume of all audio sources to match the saved volume
        OnVolumeChanged(savedVolume);


    }

    public void OnVolumeChanged(float value)
    {
        foreach (var audioSource in audioSources)
        {
            audioSource.volume = value; // Directly set the volume
        }


        // Save the current volume setting
        SaveSystem.SaveVolume(value);


    }




}