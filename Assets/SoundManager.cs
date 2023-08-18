using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public List<AudioClip> clips = new List<AudioClip>();
    public AudioSource audio;

    public void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    public void PlayClip(int i)
    {
        if (clips[i] == null)
            Debug.Break();
        audio.PlayOneShot(clips[i]);
        audio.pitch = Random.Range(0.8f, 1.1f);
    }
}
