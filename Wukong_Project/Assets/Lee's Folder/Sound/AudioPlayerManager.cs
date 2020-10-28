
using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioPlayerManager : MonoBehaviour
{
    public PlayerSounds[] Psounds;

    void Awake()
    {
        foreach (PlayerSounds s in Psounds)
        {
            s.sourceP = gameObject.AddComponent<AudioSource>();
            s.sourceP.clip = s.clips;

            s.sourceP.volume = s.volume;
            s.sourceP.pitch = s.pitch;
            s.sourceP.loop = s.loop;
        }

    }
    void Start()
    {
        //Play("theme");
    }
    public void Play(string playerSounds)
    {
        PlayerSounds s = Array.Find(Psounds, sound => sound.playerSounds == playerSounds);
    }
    // FindObjectOfType<AudioManager>().Play("check"); use in the script where it needs to be called

}
