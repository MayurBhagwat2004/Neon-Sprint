using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private bool playerTouchedScreen;
    
    void Start()
    {
        
    }

    void Update()
    {

    }
    public bool CanStartGame()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        if (Input.GetMouseButtonDown(0))
            playerTouchedScreen = true;
#else
        
        #endif
        return playerTouchedScreen;
    }
    
    public void PlayGame()
    {
        SceneManager.LoadScene("LevelScene");
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
Application.Quit();
#endif

    }
    
}
