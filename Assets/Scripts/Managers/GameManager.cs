using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;
using System;
public class GameManager : MonoBehaviour
{
    private enum SceneNames
    {
        Home, Level, Store
    }
    public static GameManager Instance;
    #region Panels
    [SerializeField] private CanvasGroup pausePanel; //GameObject for the pause panel
    [SerializeField] private CanvasGroup gameUpperPanel; //GameObject for the upper ui in the level
    #endregion
    #region Game Started Variables
    [Header("Game Started Variables")]
    public TextMeshProUGUI touchScreenText;
    public float fadingSpeed = 1.5f;
    [SerializeField] private bool gameStarted;
    public bool GameStarted => gameStarted;
    public bool gameEnded;
    public bool isGamePaused;
    #endregion

    #region Distance Covered Variables
    [Header("Distance Covered Variables")]
    public float distanceCovered;
    [SerializeField] private TextMeshProUGUI distanceCoveredText; //Text for distance covered
    [Range(1f, 10f)]
    [SerializeField] private float distanceCoveringSpeed = 5f; //Speed for calculating the distance
    public float DistanceCoveringSpeed => distanceCoveringSpeed;

    [SerializeField] private GameObject energyBarSliderObj;
    private Slider energyBarSlider;
    #endregion

    #region  Warning Timer Variables
    [Header("Warning Timer Variables")]
    public TextMeshProUGUI timerText;
    [SerializeField] private float waitTime = 5f;
    private bool playerLiftedFinger;
    public bool PlayerLiftedFinger
    {
        set { playerLiftedFinger = value; }
    }
    #endregion
    void OnEnable()
    {
        LevelEvents.OnGameStarted += HandleGameStarted;
        LevelEvents.OnGameOver += GameEnded;
    }
    void OnDisable()
    {
        LevelEvents.OnGameStarted -= HandleGameStarted;
        LevelEvents.OnGameOver -= GameEnded;
    
    }
    void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
    }
    void Start()
    {
        Time.timeScale = 1f; //UnFreeze the game


        if (gameUpperPanel != null) gameUpperPanel.gameObject.SetActive(true); //Activate the upper panel

        if (pausePanel != null) pausePanel.gameObject.SetActive(false); //Disable the pause menu panel
        
        if (timerText != null) timerText.gameObject.SetActive(false);// Disable the timer text when game starts

        if(energyBarSliderObj != null) energyBarSlider = energyBarSliderObj.GetComponent<Slider>(); //Get the slider
    }

    void Update()
    {
        
        if (gameStarted) return;

        StartTheGame();

    }

    public void GameEnded()
    {
        if(gameEnded) return;
        gameEnded = true;

        DisableObjects(gameUpperPanel.gameObject); //Disable the upper panel
        DisableObjects(energyBarSliderObj); //Disable the slider

    }

    private void DisableObjects(GameObject objToDisable)
    {
        objToDisable.SetActive(false);
    }
    public void StartShowingTimer()
    {
        if (!playerLiftedFinger) return; //If player again touches the screen then do not show timer

        StartCoroutine(StartShowingTimerRoutine());
    }

    public void StopShowingTimer()
    {
        timerText.gameObject.SetActive(false);
    }
    private IEnumerator StartShowingTimerRoutine()
    {
        timerText.gameObject.SetActive(true);

        float remainingTime = waitTime;
        while (remainingTime >= 0)
        {
            if (!playerLiftedFinger) break;

            timerText.text = "Touch The Screen Before: " + remainingTime.ToString() + " s";

            yield return new WaitForSeconds(1f);

            remainingTime--;

            if (remainingTime == 0 && playerLiftedFinger)
            {
                timerText.gameObject.SetActive(false); //Disable the timer text
                GameEnded();
            }


        }


    }

    public void ResumeGame()
    {
        if (!isGamePaused) return; // Return if the game is already resumed

        Time.timeScale = 1f; //Resume the game

        UiManager.Instance.OpenGameUpperPanel(); //Start the fade-in animation for the upper panel
        // OpenGameUpperPanel(); 
        isGamePaused = false;

    }

    public void PauseGame()
    {
        if (isGamePaused) return; //Return if the game is already paused

        Time.timeScale = 0f; //Pause the game

        UiManager.Instance.OpenPausePanel(); //Start the fade-in animation for the pause panel
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

    private void StartTheGame()
    {
        //For mobile and pc input
        if (Pointer.current != null)
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
        while (!gameEnded)
        {
            if (!isGamePaused && !playerLiftedFinger)
            {
                currentDistance += distanceCoveringSpeed * Time.deltaTime;
                
                distanceCovered = currentDistance; //Storing the current score

                distanceCoveredText.text = currentDistance.ToString("N0") + "m";
            }
            yield return null;

        }
    }

}

