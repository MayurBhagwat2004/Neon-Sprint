using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    [Header("AudioSources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;
    public AudioClip[] soundClips;
    public bool isMusicOn;

    void Awake()
    {
        if (Instance != this && Instance != null) Destroy(gameObject);
        else Instance = this;
        DontDestroyOnLoad(this);

        musicSource = transform.GetChild(0).GetComponent<AudioSource>(); //Getting the AudioSource component from first child
        sfxSource = transform.GetChild(1).GetComponent<AudioSource>(); // Getting the AudioSource component from second child

        isMusicOn = PlayerPrefs.GetInt("MusicEnabled", 1) == 1; //Setting the MusicEnabled playerpref variable to 1 by default to play music

    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += ChangeMusic;
    }

    private void Osable()
    {
        SceneManager.sceneLoaded -= ChangeMusic;       
    }
    void Start()
    {
    
    
    }

    void Update()
    {

    }

    // Music controlling functions
    public void ChangeMusic(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (!isMusicOn) return; //Checking if the isMusicOn is 1 or else return

        string sceneName = scene.name;

        if (sceneName == "Home")
        {
            musicSource.clip = Array.Find(soundClips, s => s.name == "HomeMusic"); //Finding if there is any clip name HomeMusic to load in musicSource
        }
        else if (sceneName == "LevelScene")
        {
            musicSource.clip = Array.Find(soundClips, s => s.name == "LevelMusic");
        }
        musicSource.Play();
        AudioClip audioClip = Array.Find(soundClips, clip => clip.name == "ClickSfx"); //Finding if there is any clip name ClickSfc to laod in sfxSource        
        sfxSource.clip = audioClip;
    }

    public void PlayMusic()
    {
        PlayerPrefs.SetInt("MusicEnabled",1);
        if (musicSource != null)
            musicSource.Play();
    }
    public void PauseMusic()
    {
        PlayerPrefs.SetInt("MusicEnabled",0);
        if (musicSource != null)
            musicSource.Pause();
    }
    
    public void PlayClickSound()
    {
        sfxSource.Play();
    }
}
