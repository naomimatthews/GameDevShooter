using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLoseScreen : MonoBehaviour
{
    public GameObject winScreen;
    public GameObject loseScreen;

    // audio.
    public AudioSource audioSource;
    [SerializeField] public AudioClip startAudio;
    [SerializeField] public AudioClip victoryAudio;
    [SerializeField] public AudioClip loseAudio;
    [SerializeField] public AudioClip oneEnemyRemainsAudio;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        audioSource.clip = startAudio;
        audioSource.Play();
    }

    void Update()
    {
        if (FriendlyManager.singleton.enemies.Count == 1)
        {
            audioSource.clip = oneEnemyRemainsAudio;
            audioSource.Play();
        }

        if (FriendlyManager.singleton.enemies.Count == 0)
        {
            WinScreen();
        }

        if (EnemyManager.singleton.players.Count == 0 && PlayerHealth.playerAlive == false)
        {
            LoseScreen();
        }
    }

    private void WinScreen()
    {
        audioSource.clip = victoryAudio;
        audioSource.Play();

        winScreen.SetActive(true);
    }

    private void LoseScreen()
    {
        audioSource.clip = loseAudio;
        audioSource.Play();

        loseScreen.SetActive(true);
    }
}
