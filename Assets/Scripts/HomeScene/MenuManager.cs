using UnityEngine;

public class MenuManager : MonoBehaviour
{
    private int volumeStatus; //Variable to keep track of the music
    void Start()
    {
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
        if(volumeStatus == 0)
        {
            volumeStatus = 1; //Enable music
            PlayerPrefs.SetInt("SoundStatus",volumeStatus);
            SoundManager.Instance.StartPlayingMusic();
        }
        else
        {
            volumeStatus = 0; //Disable music
            PlayerPrefs.SetInt("SoundStatus",volumeStatus);
            SoundManager.Instance.StopPlayingMusic();

        }


    }
}
