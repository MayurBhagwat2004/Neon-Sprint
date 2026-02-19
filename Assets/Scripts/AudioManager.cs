using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    [Header("AudioSources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;
    public AudioClip[] soundClips;
    public bool isMusicOn;
    public Sprite[] soundButtonsrcImages;
    public Button soundButton;

    void OnEnable()
    {
        GameEvents.OnSpeedIncreased += PlaySpeedIncreaseSfx;
    }

    void OnDisable()
    {
        GameEvents.OnSpeedIncreased -= PlaySpeedIncreaseSfx;
    }
    void Awake()
    {
        if (Instance != this && Instance != null) Destroy(gameObject);
        else Instance = this;

        musicSource = transform.GetChild(0).GetComponent<AudioSource>(); //Getting the AudioSource component from first child
        sfxSource = transform.GetChild(1).GetComponent<AudioSource>(); //Getting the AudioSource component from second child
        
        int musicStatue = PlayerPrefs.GetInt("MusicEnabled");
        isMusicOn = musicStatue == 1;
        
        UpdateMusicState();


    }

    private void UpdateMusicState()
    {
        if (isMusicOn)
        {
            PlayerPrefs.SetInt("MusicEnabled", 1);
            musicSource.UnPause();
            if (!musicSource.isPlaying) musicSource.Play();

            soundButton.image.sprite = soundButtonsrcImages[0]; //Sound On Sprite
        }
        else
        {
            PlayerPrefs.SetInt("MusicEnabled", 0);
            musicSource.Pause();

            soundButton.image.sprite = soundButtonsrcImages[1]; //Sound Off Sprite
        }
    }

    public void ToggleMusic()
    {
        isMusicOn = !isMusicOn; // Switch the state
        UpdateMusicState();
    }
    

    public void PlayGameOverMusic()
    {
        musicSource.clip = Array.Find(soundClips, clip => clip.name == "GameOverMusic");
        musicSource.Play();
    }

    private void PlaySpeedIncreaseSfx()
    {
        sfxSource.clip = Array.Find(soundClips,clip => clip.name == "SpeedIncreaseSfx");        
        sfxSource.Play();
    }


}
