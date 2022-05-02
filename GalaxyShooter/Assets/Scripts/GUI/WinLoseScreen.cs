using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLoseScreen : MonoBehaviour
{
    public GameObject winScreen;
    public GameObject loseScreen;

    // audio.
    public AudioSource audioSource;
    [SerializeField] public AudioClip victoryAudio;
    [SerializeField] public AudioClip loseAudio;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(Damageable.numOfEnemies == 0)
        {
            WinScreen();
        }
    }

    private void WinScreen()
    {
        winScreen.SetActive(true);

        audioSource.clip = victoryAudio;
        audioSource.Play();
    }
}
