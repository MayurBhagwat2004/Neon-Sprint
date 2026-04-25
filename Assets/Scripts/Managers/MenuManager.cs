using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private enum GameScenes{Home,Level,Store}

    void Update()
    {
        
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

    public void ChangeGraphics(int graphicIndex)
    {
        QualitySettings.SetQualityLevel(graphicIndex,true); 
        Debug.Log("Current graphics at: "+ QualitySettings.names[graphicIndex]);
    }


}
