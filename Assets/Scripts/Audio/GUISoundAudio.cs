using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUISoundAudio : BSoundPlayer
{
    private AudioSource audioSource;

    public AudioClip disableDNAPanel;
    public AudioClip clickNucleBase;
    public AudioClip matchFail;
    public AudioClip matchSuccess;

    public override void ChangeVolume(float _value)
    {
        audioSource.volume = _value;
    }

    public override void Start()
    {
        base.Start();

        audioSource = CreateAudioSource(gameObject);
    }

    public void Play_DisableDNAPanel()
    {
        audioSource.clip = disableDNAPanel;
        audioSource.Play();
    }

    public void Play_ClickNucleBase() {
        audioSource.clip = clickNucleBase;
        audioSource.Play();
    }
    public void Play_MatchFail()
    {
        audioSource.clip = matchFail;
        audioSource.Play();
    }
    public void Play_MatchSuccess()
    {
        audioSource.clip = matchSuccess;
        audioSource.Play();
    }
}
