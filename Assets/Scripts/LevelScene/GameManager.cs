using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private CanvasGroup pausePanel; //GameObject for the pause panel
    [SerializeField] private CanvasGroup gameUpperPanel; //GameObject for the upper ui in the level
    public float transitionTime = 0.4f;

    public bool isGamePaused; //Boolean for checking the status of the game
    private enum SceneNames
    {
        Home,Level,Store
    }

    void Awake()
    {
        if(Instance != null && Instance !=this) Destroy(this);
        else Instance = this;
    }
    void Start()
    {
        if(gameUpperPanel!=null) gameUpperPanel.gameObject.SetActive(true);
        if(pausePanel!=null) pausePanel.gameObject.SetActive(false);
    }

    void Update()
    {
        
    }

    public void ResumeGame()
    {
        if(!isGamePaused) return; // Return if the game is already resumed

        Time.timeScale = 1f; //Resume the game

        OpenGameUpperPanel(); //Start the fade-in animation for the upper panel
        isGamePaused = false;
    
    }

    public void PauseGame()
    {
        if(isGamePaused) return; //Return if the game is already paused

        Time.timeScale = 0f; //Pause the game

        OpenPausePanel(); //Start the fade-in animation for the pause panel
        isGamePaused = true;

    }
    public void GoToScene(string sceneName)
    {
        switch (sceneName)
        {
            case nameof(SceneNames.Home):
                SceneManager.LoadScene(nameof(SceneNames.Home));
                break;
            case nameof(SceneNames.Store):
                SceneManager.LoadScene(nameof(SceneNames.Store));
                break;
            case nameof(SceneNames.Level):
                SceneManager.LoadScene(nameof(SceneNames.Level));
                break;

        }
    }
    public void OpenPausePanel()
    {
        pausePanel.gameObject.SetActive(true);
        pausePanel.blocksRaycasts = true; // Detect the clicks from the user

        StartCoroutine(OpenPanelRoutine(pausePanel,0f,1f)); //Open the pause menu panel
        StartCoroutine(OpenPanelRoutine(gameUpperPanel,1f,0f)); //Close the game upper panel

    }

    public void OpenGameUpperPanel()
    {
        gameUpperPanel.gameObject.SetActive(true);
        gameUpperPanel.blocksRaycasts = true; //Detect the clicks from the user

        StartCoroutine(OpenPanelRoutine(gameUpperPanel,0f,1f)); //Open the game upper panel
        StartCoroutine(OpenPanelRoutine(pausePanel,1f,0f)); //Close the pause menu panel
    }

    private IEnumerator OpenPanelRoutine(CanvasGroup group,float initialValue,float targetValue)
    {
        float elapsedTime = 0f;
        group.alpha = initialValue;

        while (elapsedTime < transitionTime)
        {
            float t = elapsedTime / transitionTime;
            group.alpha = Mathf.Lerp(initialValue,targetValue,t); //Incrementing the alpha value gradually

            elapsedTime += Time.unscaledDeltaTime;
            yield return null;
        }
        group.alpha = targetValue;

        group.interactable = true; //Make the buttons available for clicking
    }
}
