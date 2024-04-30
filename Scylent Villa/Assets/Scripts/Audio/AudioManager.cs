using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Declaration
    // Sound References
    public Sound[] sounds;

    // This gameobject itself.
    public static AudioManager instance;

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

    // Using below code to play the specific sound you want.
    // FindObjectOfType<AudioManager>().Play("Sound Name");
}
