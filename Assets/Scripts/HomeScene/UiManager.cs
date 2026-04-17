using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UiManager : MonoBehaviour
{
    public static UiManager Instance;

    [SerializeField] private CanvasGroup pausePanel; //GameObject for the pause panel
    [SerializeField] private CanvasGroup gameUpperPanel; //GameObject for the upper ui in the level

    [Header("UI Groups")]
    [SerializeField] private CanvasGroup homeGroup;
    [SerializeField] private CanvasGroup settingsGroup;
    [SerializeField] private CanvasGroup gameOverGroup;

    [SerializeField] private float transitionTime = 0.4f;
    [SerializeField] private TextMeshProUGUI touchScreenText;
    [SerializeField] private TextMeshProUGUI distanceCoveredText;

    private bool isLevelScene; //Flag variable to check if the current scene is level
    private void Awake()
    {
        if (Instance != this && Instance != null) Destroy(this);
        else Instance = this;

        isLevelScene = SceneManager.GetActiveScene().name == "Level";

    }
    void OnEnable()
    {
        if (isLevelScene)
        {
            LevelEvents.OnGameOver += OpenGameOverPanel; //Subscribe to the game over event to show gameover panel  
        }
    }


    void OnDisable()
    {
        if (isLevelScene)
        {
            LevelEvents.OnGameOver -= OpenGameOverPanel; //Un-Subscribe to the game over event to show gameover panel  
        }
    }


    void Start()
    {
        if (gameOverGroup != null)
        {
            gameOverGroup.gameObject.SetActive(false); //Disable the panel at the beginning of the game
        }

        if (isLevelScene && touchScreenText != null)
        {
            ShowFadingEffectText(touchScreenText);
        }
    }

    // public void OpenSettings()
    // {
    //     StartCoroutine(OpenClosePanelRoutine(homeGroup, 1, 0));
    //     StartCoroutine(OpenClosePanelRoutine(settingsGroup, 0, 1));
    // }

    // public void CloseSettings()
    // {
    //     StartCoroutine(OpenClosePanelRoutine(settingsGroup, 1, 0));
    //     StartCoroutine(OpenClosePanelRoutine(homeGroup, 0, 1));
    // }

    private void UpdateScore()
    {
        distanceCoveredText.text = GameManager.Instance.distanceCovered.ToString("N0")+"m"; //Show the current distance travelled text when the game is over
    }

    public void OpenGameOverPanel()
    {
        gameOverGroup.gameObject.SetActive(true);
        StartCoroutine(OpenClosePanelRoutine(gameOverGroup, 0, 1));
        UpdateScore();
    }


    public void OpenPausePanel()
    {
        pausePanel.gameObject.SetActive(true);
        pausePanel.blocksRaycasts = true; // Detect the clicks from the user

        StartCoroutine(OpenClosePanelRoutine(pausePanel, 0f, 1f)); //Open the pause menu panel
        StartCoroutine(OpenClosePanelRoutine(gameUpperPanel, 1f, 0f)); //Close the game upper panel

    }

    public void OpenGameUpperPanel()
    {
        gameUpperPanel.gameObject.SetActive(true);
        gameUpperPanel.blocksRaycasts = true; //Detect the clicks from the user

        StartCoroutine(OpenClosePanelRoutine(gameUpperPanel, 0f, 1f)); //Open the game upper panel
        StartCoroutine(OpenClosePanelRoutine(pausePanel, 1f, 0f)); //Close the pause menu panel
    }
    
    private IEnumerator OpenClosePanelRoutine(CanvasGroup group, float initialValue, float targetValue)
    {
        float elapsedTime = 0f;
        group.alpha = initialValue;

        while (elapsedTime < transitionTime)
        {
            float t = elapsedTime / transitionTime;
            group.alpha = Mathf.Lerp(initialValue, targetValue, t); //Incrementing the alpha value gradually

            elapsedTime += Time.unscaledDeltaTime;
            yield return null;
        }
        group.alpha = targetValue;

        if (targetValue == 1)
        {
            group.interactable = true; //Make the buttons available for clicking
            group.blocksRaycasts = true;//Detect the clicks on the button

        }
    }

    private void ShowFadingEffectText(TextMeshProUGUI touchScreenText)
    {
        StartCoroutine(ShowFadingEffectRoutine(touchScreenText));
    }
    private IEnumerator ShowFadingEffectRoutine(TextMeshProUGUI textToAddEffect)
    {
        Color currentCol = textToAddEffect.color;
        float fadingSpeed = 0f;
        if (GameManager.Instance != null)
        {
            fadingSpeed = GameManager.Instance.fadingSpeed;
        }

        while (!GameManager.Instance.GameStarted)
        {

            currentCol.a = Mathf.PingPong(Time.time * fadingSpeed, 1f);

            textToAddEffect.color = currentCol;

            yield return null;
        }

        currentCol.a = 1f;
        textToAddEffect.color = currentCol;
        textToAddEffect.gameObject.SetActive(false);
    }

}
