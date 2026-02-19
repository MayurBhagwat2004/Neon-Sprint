using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.EventSystems;
using System.Collections;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public Player player;
    public bool isPlayerAlive;
    private bool playerTouchedScreen;
    public PlayerInput touchInput;
    public bool canPlayGame;
    public int score;
    [SerializeField] private TextMeshProUGUI scoreText,gameoverScoreText;
    public GameObject gameOverObj;
    public GameObject upperUiObj;
    public GameObject touchScreenUiObj;
    public GameObject speedIncreasedUiObj;
    public UpdateScore updateScoreInstance;

    [SerializeField] private int randomSpeedIncreasingNum;
    [SerializeField] private int randomSpeedIncreasingFactor;

    void Awake()
    {
        if (Instance != this && Instance != null) Destroy(gameObject);
        else Instance = this;

        isPlayerAlive = true;
        
        touchScreenUiObj.SetActive(true);
        gameOverObj.SetActive(false);
        speedIncreasedUiObj.SetActive(false);

        // QualitySettings.vSyncCount = 0;
        // Application.targetFrameRate = 60;

        randomSpeedIncreasingNum += Random.Range(score,score + randomSpeedIncreasingFactor);
        
    }

    void Start()
    {
        if (scoreText != null)
            scoreText.text = "0";
    }

    void Update()
    {
        if (!playerTouchedScreen) return;
    }

    public bool IsPointerOverUI() => EventSystem.current.IsPointerOverGameObject();


    public void PlayerDied()
    {
        isPlayerAlive = false;
        canPlayGame = false;
        if (score > PlayerPrefs.GetInt("Score"))
        {
            PlayerPrefs.SetInt("Score", score);
            updateScoreInstance.UpdateHighScore(PlayerPrefs.GetInt("Score"));

        }
        ShowGameOverUI();
        AudioManager.Instance.PlayGameOverMusic();
    }

    public void ShowGameOverUI()
    {
        StartCoroutine(ShowGameOverUIRoutine());
    }
    private IEnumerator ShowGameOverUIRoutine()
    {
        yield return new WaitForSeconds(1.3f);
        gameoverScoreText.text = score.ToString();
        gameOverObj.SetActive(true);
        upperUiObj.SetActive(false);
    }
    public bool CanStartGame()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        if (Input.GetMouseButtonDown(0) && !playerTouchedScreen && !IsPointerOverUI())
        {
            playerTouchedScreen = true;
            canPlayGame = playerTouchedScreen;
            GameObject.Find("TouchScreenParent").SetActive(false);
            player.canJumpAgain = true;
        }
#else
        if(Touchscreen.current!=null && Touchscreen.current.primaryTouch.press.isPressed && !playerTouchedScreen && !IsPointerOverUI()){
            playerTouchedScreen = true;
            canPlayGame = playerTouchedScreen;
            GameObject.Find("TouchScreenParent").SetActive(false);
            player.canJumpAgain = true;
            
        }
#endif
        return playerTouchedScreen;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void IncreaseScore()
    {
        IncreaseSpeed();
        score++;
        scoreText.text = score.ToString();

    }

    private void IncreaseSpeed()
    {
        if(score == randomSpeedIncreasingNum)
        {
            StartCoroutine(ShowSpeedIncreasedUI());
            GameEvents.OnSpeedIncreased?.Invoke();

            randomSpeedIncreasingNum += Random.Range(score,score + randomSpeedIncreasingFactor);

        }
        
    }

    private IEnumerator ShowSpeedIncreasedUI()
    {
        speedIncreasedUiObj.SetActive(true);
        yield return new WaitForSeconds(1f);
        speedIncreasedUiObj.SetActive(false);
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
