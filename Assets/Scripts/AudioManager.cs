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
    void Awake()
    {
        if (Instance != this && Instance != null) Destroy(gameObject);
        else Instance = this;

        musicSource = transform.GetChild(0).GetComponent<AudioSource>(); //Getting the AudioSource component from first child
        sfxSource = transform.GetChild(1).GetComponent<AudioSource>(); // Getting the AudioSource component from second child

        PlayMusic();
    }

    public void HandleMusic()
    {
        if (PlayerPrefs.GetInt("MusicEnabled") == 1) PauseMusic();
        else PlayMusic();
    }
    private void PlayMusic()
    {
        PlayerPrefs.SetInt("MusicEnabled", 1);
        if (musicSource != null)
        {
            musicSource.Play();
            soundButton.image.sprite = soundButtonsrcImages[0];

        }
    }
    private void PauseMusic()
    {
        PlayerPrefs.SetInt("MusicEnabled", 0);
        if (musicSource != null)
        {
            musicSource.Pause();
            soundButton.image.sprite = soundButtonsrcImages[1];
        }
    }


    

}
