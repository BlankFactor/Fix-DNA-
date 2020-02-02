using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameMusicAudio : BMusicPlayer
{
    private AudioSource audioSource;

    public AudioClip bgm;

    public override void ChangeVolume(float _value)
    {
        audioSource.volume = _value;
    }

    public override void Start()
    {
        base.Start();

        audioSource = CreateAudioSource(gameObject);
    }

    public void Play_BGM() {
        audioSource.clip = bgm;

        audioSource.Play();
    }
}
