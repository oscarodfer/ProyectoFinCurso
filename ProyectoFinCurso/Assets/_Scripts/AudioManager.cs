using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    // Start is called before the first frame update
    private void Start()
    {
        Play("Level 1");    
    }

    void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.isLoop;
        }
    }

    public void Play (string name)
    {
        Sound s = System.Array.Find(sounds, sound => sound.name == name);

        if (s.isOneShot)
        {
            s.source.PlayOneShot(s.source.clip);
        }
        else
        {
            s.source.Play();
        }    
    }

    public void Stop (string name)
    {
        Sound s = System.Array.Find(sounds, sound => sound.name == name);

        s.source.Stop();
    }
}
