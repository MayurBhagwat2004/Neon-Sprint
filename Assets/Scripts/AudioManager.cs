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

    void Awake()
    {
        if (Instance != this && Instance != null) Destroy(gameObject);
        else Instance = this;
        DontDestroyOnLoad(this);
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
        musicSource = transform.GetChild(0).GetComponent<AudioSource>(); //Getting the AudioSource component from first child
        sfxSource = transform.GetChild(1).GetComponent<AudioSource>(); // Getting the AudioSource component from second child
    
    
    }

    void Update()
    {

    }

    // Music controlling functions
    public void ChangeMusic(Scene scene, LoadSceneMode loadSceneMode)
    {
        string sceneName = scene.name;

        if (sceneName == "Home")
        {
            musicSource.clip = Array.Find(soundClips, s => s.name == "HomeMusic"); //Finding if there is any clip name HomeMusic to load in musicSource
        }

        musicSource.Play();
    }
    
    public void PlayClickSound()
    {
        AudioClip audioClip = Array.Find(soundClips,clip => clip.name == "ButtonClickSfx");        
        musicSource.PlayOneShot(audioClip);
    }
}
