using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public List<AudioClip> clips = new List<AudioClip>();
    public List<string> names    = new List<string>();
    public AudioSource audio;
    public void Start()
    {
        audio = GetComponent<AudioSource>();        
    }

    public void PlayClip(int i)
    {
        audio.PlayOneShot(clips[i]);
        audio.pitch = Random.RandomRange(0.8f, 1.1f);
    }

    public void PlayClip(string name)
    {
        int get = name.IndexOf(name);
        audio.pitch = Random.RandomRange(0.8f, 1.1f);
        if (get == -1)
        {
            audio.PlayOneShot(clips[1]);
            return;
        }
        audio.PlayOneShot(clips[name.IndexOf(name)]);
    }
}
