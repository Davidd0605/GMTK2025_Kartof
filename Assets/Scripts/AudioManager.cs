using UnityEngine.Audio;
using UnityEngine;
using System;
using UnityEngine.UIElements;
using System.Linq;
using Unity.VisualScripting;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private Sound[] sounds;
    public static AudioManager instance;
    void Awake()
    {
        
        DontDestroyOnLoad(gameObject);
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;

            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
        }
    }

    public void Start()
    {
        Play("theme", 1f);
    }

    public void Play (string name, float pitch)
    {
        Sound sound = Array.Find(sounds, sound => sound.name == name);
        sound.source.pitch = pitch;
        sound.source.Play();
    }

    public void SetVolume(float volumeMultiplier)
    {
        foreach (Sound sound in sounds)
        {
            sound.source.volume = sound.volume * volumeMultiplier;
        }
    }

    public void SetVolumeMusic(float volumeMultiplier)
    {
        Sound sound = Array.Find(sounds, sound => sound.name == "theme");
        sound.source.volume = sound.volume * volumeMultiplier;
    }

    public void SetVolumeSFX(float volumeMultiplier)
    {
        foreach (Sound sound in sounds)
        {
            if(sound.name != "theme")
                sound.source.volume = sound.volume * volumeMultiplier;
            Play("talk", 1f);
        }
    }
}
