using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private enum GameScenes{Home,Level,Store}
    private const string SOUND_KEY = "soundStatus";
    private int soundStatus; //Variable to keep track of the music
    void Start()
    {
        soundStatus = PlayerPrefs.GetInt(SOUND_KEY,1);
    
        if(soundStatus == 0) SoundManager.Instance.StopPlayingMusic(); //Stop playing the sound if the music status is 0
        else SoundManager.Instance.StartPlayingMusic(); //Start playing the sound if the music status is 1
    
    }

    void Update()
    {
        
    }

    public void CallUpdateMusicStatus()
    {
        UpdateMusicStatus();
    }

    //Function to disable or enable the music
    private void UpdateMusicStatus()
    {
        if(soundStatus == 0)
        {
            soundStatus = 1; //Enable music
            PlayerPrefs.SetInt("soundStatus",soundStatus);
            SoundManager.Instance.StartPlayingMusic();
        }
        else
        {
            soundStatus = 0; //Disable music
            PlayerPrefs.SetInt("soundStatus",soundStatus);
            SoundManager.Instance.StopPlayingMusic();

        }
    }

    #region Menu button functions
    
    public void GoToLevel()
    {
        SceneManager.LoadScene(GameScenes.Level.ToString()); //Load the level scene
    }
    public void GoToStore()
    {
        SceneManager.LoadScene(GameScenes.Store.ToString()); //Load the store scene
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(GameScenes.Level.ToString()); //Load the next scene which is level
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; //Quitting the game in the editor
        #else
            Application.Quit(); //Quitting the game on android device
        #endif
    }

    #endregion
}
