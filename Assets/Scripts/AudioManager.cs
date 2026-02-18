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
    public AudioClip[] soundClips;
    public bool isMusicOn;
    public Sprite[] soundButtonsrcImages;
    public Button soundButton;
    void Awake()
    {
        if (Instance != this && Instance != null) Destroy(gameObject);
        else Instance = this;

        musicSource = transform.GetChild(0).GetComponent<AudioSource>(); //Getting the AudioSource component from first child

        int musicStatue = PlayerPrefs.GetInt("MusicEnabled", 1);
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
    private void PlayMusic()
    {
        PlayerPrefs.SetInt("MusicEnabled", 1);
        if (musicSource != null)
        {
            isMusicOn = true;
            musicSource.Play();
            soundButton.image.sprite = soundButtonsrcImages[0];

        }
    }
    private void PauseMusic()
    {
        PlayerPrefs.SetInt("MusicEnabled", 0);
        if (musicSource != null)
        {
            isMusicOn = false;
            musicSource.Pause();
            soundButton.image.sprite = soundButtonsrcImages[1];
        }
    }

    public void PlayGameOverMusic()
    {
        musicSource.clip = Array.Find(soundClips, clip => clip.name == "GameOverMusic");
        musicSource.Play();
    }

    private void PlaySpeedIncreaseSfx()
    {

    }


}
