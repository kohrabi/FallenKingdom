using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class walkaudio : MonoBehaviour
{
    SoundManager soundManager;
    // Start is called before the first frame update
    void Start()
    {
        soundManager = GameObject.FindWithTag("SoundManager").GetComponent<SoundManager>();
    }

    public void PlaySoundWalk()
    {
        soundManager.PlayClip(0);
    }
}
