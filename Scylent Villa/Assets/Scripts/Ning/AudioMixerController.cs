using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioMixerController : MonoBehaviour
{
    public AudioMixer audioMixer; // Reference to the audio mixer
    public Slider volumeSlider; // Reference to the slider
    public bool mute;
    // Initialize the slider value to match the current volume level
    private float currentVolume;

    void Start()
    {
        audioMixer.GetFloat("MyExposedParam", out currentVolume);

    }

    private void Update()
    {
        currentVolume = volumeSlider.value;

        if (mute == false)
        {
            audioMixer.SetFloat("MyExposedParam", Mathf.Log10(currentVolume) * 10);
        }
        else
        {
            audioMixer.SetFloat("MyExposedParam", Mathf.Log10(0.00000001f) * 10);
        }
    }

    public void MuteMixer()
    {
        mute = !mute;
    }

    // Function to update the master volume based on the slider value
    public void SetVolume(float value)
    {
        audioMixer.SetFloat("MyExposedParam", Mathf.Log10(value) * 10);
    }
}
