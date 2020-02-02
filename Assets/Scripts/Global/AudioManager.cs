using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [HideInInspector]
    public static AudioManager instance;

    [Header("音效监听组")]
    public List<BAudioPlayer> soundPlayers = new List<BAudioPlayer>();

    [Header("音乐监听组")]
    public List<BAudioPlayer> musicPlayers = new List<BAudioPlayer>();

    [Header("音频设置")]
    [Range(0, 1)]
    public float musicVolume = 0.5f;
    [Range(0, 1)]
    public float soundVolume = 0.5f;

    private float musicVolumeBuffer;
    private float soundVolumeBuffer;

    private void Awake()
    {
        instance = this;

        musicVolumeBuffer = 0;
        soundVolumeBuffer = 0;
    }

    public void Update()
    {
        if (!musicVolume.Equals(musicVolumeBuffer))
        {
            ChangeMusicVolume();
            musicVolumeBuffer = musicVolume;
        }
        if (!soundVolume.Equals(soundVolumeBuffer)) {
            ChangeSoundVolume();
            soundVolumeBuffer = soundVolume;
        }
       
    }

    #region 监听组管理操作
    public void AddSoundLisitenner(BAudioPlayer _ap)
    {
        soundPlayers.Add(_ap);
    }
    public void RemoveSoundLisitenner(BAudioPlayer _ap)
    {
        soundPlayers.Remove(_ap);
    }
    public void AddMusicLisitenner(BAudioPlayer _ap)
    {
        musicPlayers.Add(_ap);
    }
    public void RemoveMusicLisitenner(BAudioPlayer _ap)
    {
        musicPlayers.Remove(_ap);
    }
    #endregion


    // 等待加入 滑块OnValueChange 监听行列
    public void ChangeSoundVolume()
    {
        foreach (var v in soundPlayers) {
            v.ChangeVolume(soundVolume);
        }
    }
    public void ChangeMusicVolume()
    {
        foreach (var v in musicPlayers) {
            v.ChangeVolume(musicVolume);
        }
    }
}
