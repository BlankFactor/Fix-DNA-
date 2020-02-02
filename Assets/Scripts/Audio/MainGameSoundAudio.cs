using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameSoundAudio : BSoundPlayer
{
    private AudioSource audioSource;

    public AudioClip startGame;
    public AudioClip playerVictory;
    public AudioClip playerLose;

    public override void ChangeVolume(float _value)
    {
        audioSource.volume = _value;
    }

    public override void Start()
    {
        base.Start();

        audioSource = CreateAudioSource(gameObject);
    }

    public void Play_StartGame() {
        audioSource.clip = startGame;
        audioSource.Play();
    }
    public void Play_PlayerVictory()
    {
        audioSource.clip = playerVictory;
        audioSource.Play();
    }
    public void Play_PlayerLose()
    {
        audioSource.clip = playerLose;
        audioSource.Play();
    }
}
