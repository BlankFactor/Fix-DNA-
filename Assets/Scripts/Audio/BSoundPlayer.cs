using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BSoundPlayer : BAudioPlayer
{
    public virtual void Start()
    {
        LinkToManager();
    }

    public abstract override void ChangeVolume(float _value);

    protected override void LinkToManager()
    {
        AudioManager.instance.AddSoundLisitenner(this);
    }

    protected override AudioSource CreateAudioSource(GameObject _go)
    {
        AudioSource _as;

        _as = _go.AddComponent<AudioSource>();
        _as.playOnAwake = false;
        _as.loop = false;

        return _as;
    }
}
