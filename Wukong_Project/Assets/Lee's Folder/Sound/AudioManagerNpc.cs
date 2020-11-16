using System;
using UnityEngine;

public class AudioManagerNpc : MonoBehaviour
{
    public NPCsounds[] NpcSounds;

    void Awake()
    {
        foreach (NPCsounds s in NpcSounds)
        {
            s.source2 = gameObject.AddComponent<AudioSource>();
            s.source2.clip = s.NpcClips;

            s.source2.volume = s.volume;
            s.source2.pitch = s.pitch;
            s.source2.loop = s.loop;
        }

    }

    public void Play(string NpcSound1)
    {
        NPCsounds s = Array.Find(NpcSounds, sound => sound.NpcSound1 == NpcSound1);
    }
}
