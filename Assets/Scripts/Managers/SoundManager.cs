using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    public Slider volumeSlider;
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
        if (PlayerPrefs.HasKey(SOUND_KEY))
        {
            PlayerPrefs.SetInt(SOUND_KEY,1);
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
            if(scene.name == SceneNames.Home.ToString())
            {
                musicSource.clip = musicClips.Find(clip => clip.name == "HomeMusic");
                musicSource.loop = true;
                musicSource.Play();

                if(volumeSlider == null)
                {
                    volumeSlider = GameObject.Find("VolumeSlider").GetComponent<Slider>();
                }
            }
            else if(scene.name == SceneNames.Level.ToString())
            {
                musicSource.clip = musicClips.Find(clip => clip.name == "LevelMusic");
                musicSource.loop = true;
                musicSource.Play();
            }
        }
    }

    public void HandleMusicVolume()
    {
        if(volumeSlider != null)
        {
            musicSource.volume = volumeSlider.value;
            if(musicSource.volume == 0)
            {
                PlayerPrefs.SetInt(SOUND_KEY,0);
                soundStatus = 0;
            }
            else
            {
                PlayerPrefs.SetInt(SOUND_KEY,1);
                soundStatus = 1;

            }
        }
        else
        {
            Debug.Log("Provide the volume slider in inspector");
        }
    }
    private void StartPlayingMusic()
    {
        if(PlayerPrefs.GetInt(SOUND_KEY) == 0)
        {
            musicSource.Play();
            PlayerPrefs.SetInt(SOUND_KEY,1);
        }
        else return;
    }

    private void StopPlayingMusic()
    {
        if(PlayerPrefs.GetInt(SOUND_KEY) == 1)
        {
            musicSource.Pause(); 
            PlayerPrefs.SetInt(SOUND_KEY,0);
        }
        else return;
    }

    public void PlayClickSound()
    {
        if(PlayerPrefs.GetInt(SOUND_KEY) == 1)
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
        if(PlayerPrefs.GetInt(SOUND_KEY) == 1)
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
        if(soundStatus == 0) return;

        AudioClip clip = sfxClips.Find(c => c.name == SfxClips.EnergyBarSfx.ToString());
        if(clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }

    public void PlayGameOverSfx()
    {
        if(soundStatus == 0) return;

        AudioClip clip = sfxClips.Find(c => c.name == SfxClips.GameOverSfx.ToString());

        if(clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }
}
