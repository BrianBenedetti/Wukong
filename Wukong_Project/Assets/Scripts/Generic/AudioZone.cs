using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Zone
{
    Fire,
    Water,
    Air
}

public class AudioZone : MonoBehaviour
{
    public Zone myZone;

    AudioSource source;

    public AudioClip fireClip;
    public AudioClip waterClip;
    public AudioClip airClip;

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(TriggerEnter());
    }

    private void OnTriggerExit(Collider other)
    {
        StartCoroutine(TriggerExit());
    }

    private void Awake()
    {
        source = GetComponentInParent<AudioSource>();
    }

    IEnumerator TriggerEnter()
    {
        float t = 0;
        float transitionTime = 1;

        for (t = 0; t < transitionTime; t += Time.deltaTime)
        {
            source.volume = (0 + (t / transitionTime));

            switch (myZone)
            {
                case Zone.Fire:
                    source.volume = Mathf.Clamp(source.volume, 0, 1);
                    break;
                case Zone.Water:
                    source.volume = Mathf.Clamp(source.volume, 0, 0.2f);
                    break;
                case Zone.Air:
                    source.volume = Mathf.Clamp(source.volume, 0, 0.4f);
                    break;
                default:
                    break;
            }
            yield return null;
        }

        switch (myZone)
        {
            case Zone.Fire:
                source.Stop();
                source.clip = fireClip;
                source.Play();
                break;
            case Zone.Water:
                source.Stop();
                source.clip = waterClip;
                source.Play();
                break;
            case Zone.Air:
                source.Stop();
                source.clip = airClip;
                source.Play();
                break;
            default:
                break;
        }
    }

    IEnumerator TriggerExit()
    {
        float t = 0;
        float transitionTime = 1;

        for (t = 0; t < transitionTime; t += Time.deltaTime)
        {
            source.volume = (1 - (t / transitionTime));
            yield return null;
        }
    }
}
