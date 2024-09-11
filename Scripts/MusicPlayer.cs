using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public AudioSource intro, loop;
    // Start is called before the first frame update
    void Start()
    {
        intro.Play();
        loop.PlayScheduled(AudioSettings.dspTime + intro.clip.length);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
