using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    public GameObject wave;
    public GameObject waveTop;
    public GameObject waveTopMarker;
    public GameObject waveBottomMarker;
    public float spawnWaveSpeed = 10;
    public float despawnWaveSpeed = 20;
    private bool waveAlive = true;
    private float waveDirection = 1;
    private float wavePauseTime = 2;
    private float wavePauseCountdown;
    public float waveSpawnTimeMax = 15;
    public float waveSpawnTimeMin = 3;
    private float waveSpawnCountdown;

    public AudioClip waveSound;
    [HideInInspector]
    public AudioSource audioWave;
    public AudioSource audioWaveDespawn;

    // Start is called before the first frame update
    void Start()
    {
        audioWave = addAudio(MakeSubclip(waveSound, 1, 4.5f), false, false, 1.0f);
        audioWaveDespawn = addAudio(MakeSubclip(waveSound,2,4.5f), false, false, 1.0f);
        wavePauseCountdown = wavePauseTime;
        waveSpawnCountdown = newWaveSpawnTime();
    }

    float newWaveSpawnTime()
    {             
        return Random.Range(waveSpawnTimeMin, waveSpawnTimeMax);
    }

    // Update is called once per frame
    void Update()
    {
        /*
         If spawn wave
        if wave has gotten to the top and pause delay is > 0 then pause
        if wave is going up then move up
        if wave is going down then move down
         */
        if (waveAlive)
        {      
            //Debug.Log("Wave Alive");
            //If we get to the top of the screen and then we should reverse the wave direction            
            if (wave.transform.position.y >= waveTopMarker.transform.position.y && wavePauseCountdown > 0)
            {
                wavePauseCountdown = wavePauseCountdown - Time.deltaTime;                
            }
            else if (wavePauseCountdown <= 0 && waveDirection == 1)
            {
                waveDirection = -1;
                gameObject.GetComponent<EnvironmentController>().spawnShells();                
            }
            //If we get to the bottom of the screen then we should reverse the wave direction
            else if (wave.transform.position.y < waveBottomMarker.transform.position.y)
            {

                waveDirection = 1;
                wavePauseCountdown = wavePauseTime;
                waveAlive = false;
            }
            else if (waveDirection > 0)
            {
                if (!audioWave.isPlaying)
                {
                    audioWave.Play();
                }
                spawnWave(spawnWaveSpeed);
            }
            else if (waveDirection < 0)
            {
                if (!audioWave.isPlaying)
                {
                    audioWave.Play();
                }
                despawnWave(despawnWaveSpeed);
            }                   
        }
        else
        {
            if (waveSpawnCountdown <= 0)
            {
                //Debug.Log("Wave Dead");
                waveAlive = true;
                waveSpawnCountdown = newWaveSpawnTime();
                wave.transform.position = new Vector3(wave.transform.position.x, waveBottomMarker.transform.position.y + 0.1f, wave.transform.position.z);
            }
            else
            {
                //Debug.Log("Wave spawn countdown = "+waveSpawnCountdown);
                waveSpawnCountdown = waveSpawnCountdown - Time.deltaTime;
            }
            
        }
    }

    void moveWave(float waveSpeed)
    {
        wave.transform.position = new Vector3(wave.transform.position.x, wave.transform.position.y + waveSpeed * waveDirection * Time.deltaTime, wave.transform.position.z);
    }

    void spawnWave(float waveSpeed)
    {
        wave.transform.position = new Vector3(wave.transform.position.x, wave.transform.position.y + waveSpeed*Time.deltaTime, wave.transform.position.z);
        waveTop.transform.position = new Vector3(waveTop.transform.position.x, waveTop.transform.position.y + waveSpeed * Time.deltaTime, waveTop.transform.position.z);
    }

    void despawnWave(float waveSpeed)
    {
        wave.transform.position = new Vector3(wave.transform.position.x, wave.transform.position.y - waveSpeed*Time.deltaTime, wave.transform.position.z);
        waveTop.transform.position = new Vector3(waveTop.transform.position.x, waveTop.transform.position.y - waveSpeed * Time.deltaTime, waveTop.transform.position.z);
    }

    AudioSource addAudio(AudioClip clip, bool loop, bool playAwake, float volume)
    {
        AudioSource newAudio = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
        newAudio.clip = clip;
        newAudio.loop = loop;
        newAudio.playOnAwake = playAwake;
        newAudio.volume = volume;
        return newAudio;
    }

    private AudioClip MakeSubclip(AudioClip clip, float start, float stop)
    {
        /* Create a new audio clip */
        int frequency = clip.frequency;
        float timeLength = stop - start;
        int samplesLength = (int)(frequency * timeLength);
        AudioClip newClip = AudioClip.Create(clip.name + "-sub", samplesLength, 1, frequency, false);
        /* Create a temporary buffer for the samples */
        float[] data = new float[samplesLength];
        /* Get the data from the original clip */
        clip.GetData(data, (int)(frequency * start));
        /* Transfer the data to the new clip */
        newClip.SetData(data, 0);
        /* Return the sub clip */
        return newClip;
    }
}
