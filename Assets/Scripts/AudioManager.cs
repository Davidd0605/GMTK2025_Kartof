using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private Sound[] sounds;
    [SerializeField] private float volumeMultiplier = 1;
    public static AudioManager instance;
    void Awake()
    {
        if (instance == null)
            instance = this;
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
        Play("theme");
    }

    public void Play (string name)
    {
        Sound sound = Array.Find(sounds, sound => sound.name == name);
        if (sound.randomPitch)
            sound.RandomPitch(sound.pitch);
        sound.source.Play();
    }

    public void SetVolume()
    {
        foreach (Sound sound in sounds)
        {
            sound.source.volume = sound.volume * volumeMultiplier;
        }
    }
}
