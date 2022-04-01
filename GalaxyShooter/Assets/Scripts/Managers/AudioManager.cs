using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource src;

    // match audiios.
    [SerializeField] public AudioClip matchStartAudio;
    [SerializeField] public AudioClip footstepsAudio;
    [SerializeField] public AudioClip victoryAudio;
    [SerializeField] public AudioClip defeatAudio;

    // agent audios.
    [SerializeField] public AudioClip eunhaDashAudio;
    [SerializeField] public AudioClip eunhaSpeedBoostAudio;
    [SerializeField] public AudioClip eunhaUltimateAudio;

    [SerializeField] public AudioClip winterQAudio;
    [SerializeField] public AudioClip winterEAudio;
    [SerializeField] public AudioClip winterUltimateAudio;

    void Start()
    {
        src = GetComponent<AudioSource>();

        src.clip = matchStartAudio;
        src.Play();
    }

 
    void Update()
    {
        
    }

    void eunhaQability()
    {
        src.clip = eunhaDashAudio;
        src.Play();
    }

    void eunhaEability()
    {
        src.clip = eunhaSpeedBoostAudio;
        src.Play();
    }

    void eunhaUltimate()
    {
        src.clip = eunhaUltimateAudio;
        src.Play();
    }

    void winterQability()
    {
        src.clip = winterQAudio;
        src.Play();
    }

    void winterEability()
    {
        src.clip = winterEAudio;
        src.Play();
    }

    void winterUltimate()
    {
        src.clip = winterUltimateAudio;
        src.Play();
    }
}
