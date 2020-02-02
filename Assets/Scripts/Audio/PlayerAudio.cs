using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : BSoundPlayer
{
    private AudioSource audioSource;

    public AudioClip clickCell;

    public override void ChangeVolume(float _value)
    {
        audioSource.volume = _value;
    }

    public override void Start()
    {
        base.Start();

        audioSource = CreateAudioSource(gameObject);
    }

    void Update()
    {
        
    }

    public void Play_ClickCell() {
        audioSource.clip = clickCell;
        audioSource.Play();
    }
}
