using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class SoundManager : MonoBehaviour
{
    private static SoundManager _instance;

    public static SoundManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType(typeof(SoundManager)) as SoundManager;
            }
            return _instance;
        }
    }
    public AudioSource diaryDropSound;
    public AudioSource diaryOpenSound;
    public AudioSource diaryFilpSound;
    public AudioSource mirrorCrackingSound;
    public AudioSource pianoBGM;

    private AudioSource bgmAudioSource;
    private bool onEffectSound;
    private bool onBGMSound;

    public AudioClip BGM;

    void Start()
    {
        onEffectSound = Convert.ToBoolean(PlayerPrefs.GetInt("effectsound", 1));
        onBGMSound = Convert.ToBoolean(PlayerPrefs.GetInt("bgmsound", 1));

        bgmAudioSource = gameObject.AddComponent<AudioSource>();
        bgmAudioSource.loop = true;
        bgmAudioSource.clip = BGM;
        PlayBGM();
    }

    public void PlayBGM()
    {
        if(onBGMSound == true)
            bgmAudioSource.Play();
    }

    public void SetBGMVolume(float volume)
    {
        bgmAudioSource.volume = volume;
    }
    public float GetBGMVolume()
    {
        return bgmAudioSource.volume;
    }

    public void FlipSound()
    {
        diaryFilpSound.Play();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
