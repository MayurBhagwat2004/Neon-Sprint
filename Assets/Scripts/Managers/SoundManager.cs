using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    private enum SfxClips
    {
        ClickSfx,JumpSfx,EnergyBarSfx,ObstacleSfx,GameOverSfx
    };
    public static SoundManager Instance;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private List<AudioClip> musicClips;
    [SerializeField] private List<AudioClip> sfxClips;

    private const string SOUND_KEY = "SoundStatus";
    private int soundStatus; //Variable to keep track of the music


    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
            return;

        }
        if (PlayerPrefs.HasKey("SoundStatus"))
        {
            PlayerPrefs.SetInt("SoundStatus",1);
        }
    }

    void Start()
    {
        soundStatus = PlayerPrefs.GetInt(SOUND_KEY,1);
    
        if(soundStatus == 0) StopPlayingMusic(); //Stop playing the sound if the music status is 0
        else StartPlayingMusic(); //Start playing the sound if the music status is 1
    
    }
    void OnEnable()
    { 
        SceneManager.sceneLoaded += OnSceneLoaded; 
        LevelEvents.OnEnergyBarAcquired += PlayEnergySfx;
        LevelEvents.OnObstacleHit += PlayObstacleHitSfx;
        LevelEvents.OnGameOver += PlayGameOverSfx;
    }
    void OnDisable()
    { 
        SceneManager.sceneLoaded -= OnSceneLoaded; 
        LevelEvents.OnEnergyBarAcquired -= PlayEnergySfx;
        LevelEvents.OnObstacleHit -= PlayObstacleHitSfx;
        LevelEvents.OnGameOver -= PlayGameOverSfx;


    }

    void OnSceneLoaded(Scene scene,LoadSceneMode loadSceneMode)
    {
        if(PlayerPrefs.GetInt("SoundStatus") == 1) //Checking if the sound is enabled or not
        {
            if(scene.name == "Home")
            {
                musicSource.clip = musicClips.Find(clip => clip.name == "HomeMusic");
                musicSource.loop = true;
                musicSource.Play();
            }
            else if(scene.name == "Level")
            {
                musicSource.clip = musicClips.Find(clip => clip.name == "LevelMusic");
                musicSource.loop = true;
                musicSource.Play();
            }
        }
    }

    public void StartPlayingMusic()
    {
        if(PlayerPrefs.GetInt("SoundStatus") == 0)
        {
            musicSource.Play();
            PlayerPrefs.SetInt("SoundStatus",1);
        }
        else return;
    }

    public void StopPlayingMusic()
    {
        if(PlayerPrefs.GetInt("SoundStatus") == 1)
        {
            musicSource.Pause(); 
            PlayerPrefs.SetInt("SoundStatus",0);
        }
        else return;
    }

    public void PlayClickSound()
    {
        if(PlayerPrefs.GetInt("SoundStatus") == 1)
        {
            AudioClip clip = sfxClips.Find(c => c.name == SfxClips.ClickSfx.ToString());
            if(clip != null)
            {
                sfxSource.PlayOneShot(clip);
            }
        }
    }
    public void PlayJumpSfx()
    {
        if(PlayerPrefs.GetInt("SoundStatus") == 1)
        {
            AudioClip clip = sfxClips.Find(c => c.name == SfxClips.JumpSfx.ToString());
            if(clip != null)
            {
                sfxSource.PlayOneShot(clip);
            }
        }
    }

    public void PlayObstacleHitSfx()
    {
        if(PlayerPrefs.GetInt("SoundStatus") == 1)
        {
            AudioClip clip = sfxClips.Find(c => c.name == SfxClips.ObstacleSfx.ToString());
            if(clip != null)
            {
                sfxSource.PlayOneShot(clip);
            }
        }
    }

    public void PlayEnergySfx()
    {
        AudioClip clip = sfxClips.Find(c => c.name == SfxClips.EnergyBarSfx.ToString());
        if(clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }

    public void PlayGameOverSfx()
    {
        AudioClip clip = sfxClips.Find(c => c.name == SfxClips.GameOverSfx.ToString());

        if(clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }
}
