using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sound;

    void Awake()
    {
        foreach(Sound s in sound)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
        
    }
    void Start()
    {
        Play("theme");
    }
    public void Play (string name)
    {
        Sound s = Array.Find(sound, sound => sound.name == name);
    }
    // FindObjectOfType<AudioManager>().Play("check"); use in the script where it needs to be called

}
