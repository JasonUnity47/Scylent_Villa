using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneSFX : MonoBehaviour
{
    // Reference to the AudioSource component
    public AudioSource audioSource;

    // Called when the script instance is being loaded
    void Awake()
    {
        
    }

    // This method is called whenever the GameObject this script is attached to becomes active
    void OnEnable()
    {
        // If the AudioSource is available, play the audio
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }

    
}
