using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioClip clipBird;
    public AudioClip clipSpace;
    public AudioClip clipSos;
    public AudioClip clipRain;
    public AudioClip clipBell;

    private AudioSource audioBird;
    private AudioSource audioSpace;
    private AudioSource audioSos;
    private AudioSource audioRain;
    private AudioSource audioBell;


    private float countdown = 5.0f;
    private bool playedaudio = false;

    // Start is called before the first frame update
    void Start()
    {

    }
    //clip:AudioClip, loop: boolean, playAwake: boolean, vol: float): AudioSource
    AudioSource addAudio(AudioClip clip, bool loop, bool playAwake, float volume)
    {
        AudioSource newAudio = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
        newAudio.clip = clip;  
        newAudio.loop = loop;
        newAudio.playOnAwake = playAwake;
        newAudio.volume = volume;
        return newAudio;   
    }

// Update is called once per frame
    void Update()
    {        
        countdown = countdown - Time.deltaTime;
        if (countdown < 0 && playedaudio == false)
        {
            Debug.Log("Play bird");
            audioBird.Play();
            playedaudio = true;
        }
    }

    private void Awake()
    {
        audioBird = addAudio(clipBird, false, false, 1.0f);
        audioSpace = addAudio(clipSpace, false, false, 1.0f);
        audioSos = addAudio(clipSos, false, false, 1.0f);
        audioRain = addAudio(clipRain, false, false, 1.0f);
        audioBell = addAudio(clipBell, false, false, 1.0f);
    }
}
