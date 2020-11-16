using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    public AudioClip footstep;
    public AudioClip swingHeavy;
    public AudioClip rage;

    AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void PlaySFX(int number)
    {
        switch (number)
        {
            case 0:
                source.pitch = Random.Range(0.8f, 1.2f);
                source.volume = 0.05f;
                source.PlayOneShot(footstep);
                break;
            case 1:
                source.pitch = Random.Range(0.8f, 1.2f);
                source.volume = 1;
                source.PlayOneShot(rage);
                break;
            case 2:
                source.pitch = Random.Range(0.8f, 1.2f);
                source.volume = Random.Range(0.8f, 1.2f);
                source.PlayOneShot(swingHeavy);
                break;
            default:
                break;
        }
    }
}
