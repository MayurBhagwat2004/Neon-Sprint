using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
public enum GameStatusTexts
{
    SpeedIncreased, SpeedDecreased, AbilityAcquired, CriticalHealth
}
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
    [SerializeField] private CanvasGroup gameStatusPanel; //Gameobject for the update of the game like speed increased, health decreased,etc
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
    #region  Status Updating Variables
    [Header("Status Updating Variables")]
    private string newMessage;
    private string lastMessage;
    public TextMeshProUGUI gameStatusText;
    private readonly Dictionary<GameStatusTexts, string> statusMessage = new Dictionary<GameStatusTexts, string>
    {
        {GameStatusTexts.CriticalHealth,"Critical Health!"},
        {GameStatusTexts.SpeedIncreased,"Speed Increased !!!"},
        {GameStatusTexts.SpeedDecreased,"Speed Decreased !!!"},
        {GameStatusTexts.AbilityAcquired,"Ability Acquired !!!"},
    };
    #endregion

    private Coroutine statusRoutine; //Storing the status coroutine
    void OnEnable()
    {
        LevelEvents.OnGameStarted += HandleGameStarted;
        LevelEvents.OnGameOver += GameEnded;
        LevelEvents.OnStatusUpdate += TriggerStatus;
        LevelEvents.OnCriticalHealth += TriggerStatus;

    }
    void OnDisable()
    {
        LevelEvents.OnGameStarted -= HandleGameStarted;
        LevelEvents.OnGameOver -= GameEnded;
        LevelEvents.OnStatusUpdate -= TriggerStatus;
        LevelEvents.OnCriticalHealth -= TriggerStatus;

    }
    void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
    }
    void Start()
    {
        Time.timeScale = 1f; //UnFreeze the game


        if (gameUpperPanel != null) gameUpperPanel.alpha = 1;//Activate the upper panel

        if (pausePanel != null) DisableObjects(pausePanel.gameObject); //Disable the pause menu panel

        if (timerText != null) DisableObjects(timerText.gameObject);// Disable the timer text when game starts

        if (energyBarSliderObj != null) energyBarSlider = energyBarSliderObj.GetComponent<Slider>(); //Get the slider

        if (gameStatusPanel != null) gameStatusPanel.alpha = 0;
    }

    void Update()
    {

        if (gameStarted) return;

        StartTheGame();

    }

    public void GameEnded()
    {
        if (gameEnded) return;
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
                LevelEvents.GameOver();
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

    private IEnumerator ShowUpdateRoutine(GameStatusTexts statusUpdateString)
    {

        float durationToDisplay = 1f;
        float fadeDuration = 2f;
        float elapsedTime = 0f;
        if (statusMessage.TryGetValue(statusUpdateString, out string message))
        {
            newMessage = message;
            if (newMessage != lastMessage)
            {
                gameStatusText.text = newMessage;
                lastMessage = newMessage; //Store the new message
            }

            gameStatusPanel.alpha = 1;
        }

        Debug.Log(message);


        yield return new WaitForSeconds(durationToDisplay);

        while (elapsedTime < durationToDisplay)
        {
            elapsedTime += Time.deltaTime;

            gameStatusPanel.alpha = 1 - (elapsedTime / fadeDuration);
            yield return null;
        }


        gameStatusPanel.alpha = 0;
        statusRoutine = null;
    }

    public void TriggerStatus(GameStatusTexts gameStatusTexts)
    {
        if (statusRoutine != null) StopCoroutine(statusRoutine);
        statusRoutine = StartCoroutine(ShowUpdateRoutine(gameStatusTexts));
    }
}

