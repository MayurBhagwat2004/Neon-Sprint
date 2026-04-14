using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using TMPro;
public class GameManager : MonoBehaviour
{
    private enum SceneNames
    {
        Home,Level,Store
    }
    public static GameManager Instance;
    [SerializeField] private CanvasGroup pausePanel; //GameObject for the pause panel
    [SerializeField] private CanvasGroup gameUpperPanel; //GameObject for the upper ui in the level
    private bool gameStarted;
    public bool isGamePaused;
    [SerializeField] private float transitionTime = 0.5f;

    [Header("Distance Covered Variables")]
    [SerializeField] private TextMeshProUGUI distanceCoveredText; //Text for distance covered
    [SerializeField] private float distanceCoveringSpeed = 5f;

    void OnEnable()
    {
        LevelEvents.OnGameStarted += HandleGameStarted;
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
        if(gameStarted) return;
        StartTheGame();
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

    private void StartTheGame()
    {
        //For mobile and pc input
        if(Pointer.current != null)
        {
            if (Pointer.current.press.wasPressedThisFrame && !gameStarted)
            {
                LevelEvents.StartTheGame();
                gameStarted = true;
            }
        }
    }

    public void HandleGameStarted()
    {
        StartCoroutine(StartCalculatingDistanceRoutine());
    }
    private IEnumerator StartCalculatingDistanceRoutine()
    {
        float currentDistance = 0f;
        while (true)
        {
            if (!isGamePaused)
            {
                currentDistance += distanceCoveringSpeed * Time.deltaTime;
                distanceCoveredText.text = currentDistance.ToString("N0") + "m";
            }
            yield return null;
                
            }
    }
    }

