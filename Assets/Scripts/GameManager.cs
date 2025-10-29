using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using TMPro;
using UnityEngine.EventSystems;
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
    public UpdateScore updateScoreInstance;

    void Awake()
    {
        if (Instance != this && Instance != null) Destroy(gameObject);
        else Instance = this;

        isPlayerAlive = true;
        gameOverObj.SetActive(false);

        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        
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
        score++;
        scoreText.text = score.ToString();
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
    public void SendBackHome()
    {
        SceneManager.LoadScene("HomeScene");
    }
    
}
