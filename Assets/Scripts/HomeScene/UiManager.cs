using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    public static UiManager Instance;
    [Header("UI Groups")]
    [SerializeField] private CanvasGroup homeGroup;
    [SerializeField] private CanvasGroup settingsGroup;
    [SerializeField] private CanvasGroup gameOverGroup;

    [SerializeField] private float transitionTime = 0.4f;

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneChanged;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneChanged;
        
    }
    private void OnSceneChanged(Scene scene,LoadSceneMode loadSceneMode)
    {
        if(SceneManager.GetActiveScene().name == "Level")
        {
            LevelEvents.OnGameOver += OpenGameOverPanel;
        }
    }
    private void Awake() 
    {
        if(Instance != this && Instance !=null) Destroy(this);
        else Instance = this;
    }

    void Start()
    {
        if(gameOverGroup!=null)
        {
            gameOverGroup.gameObject.SetActive(false); //Disable the panel at the beginning of the game
        }
    }
    public void OpenSettings()
    {
        StartCoroutine(OpenPanelRoutine(homeGroup,1,0));
        StartCoroutine(OpenPanelRoutine(settingsGroup,0,1));
    }

    public void CloseSettings()
    {
        StartCoroutine(OpenPanelRoutine(settingsGroup,1,0));
        StartCoroutine(OpenPanelRoutine(homeGroup,0,1));
    }

    public void OpenGameOverPanel()
    {
        StartCoroutine(OpenPanelRoutine(gameOverGroup,0,1));
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
        group.blocksRaycasts = true;//Detect the clicks on the button
    }
    
}
