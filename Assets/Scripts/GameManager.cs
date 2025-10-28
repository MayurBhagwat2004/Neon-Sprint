using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public Player player;
    public bool isPlayerAlive;
    private bool playerTouchedScreen;
    public PlayerInput touchInput;
    public bool canPlayGame;
    public int score;
    [SerializeField] private TextMeshProUGUI scoreText;
    public GameObject gameOverObj;
    public GameObject upperUiObj;

    [SerializeField] private TextMeshProUGUI latestScoreText;
    void Awake()
    {
        if (Instance != this && Instance != null) Destroy(gameObject);
        else Instance = this;

        isPlayerAlive = true;
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

    public void PlayerDied()
    {
        isPlayerAlive = false;
        canPlayGame = false;
        PlayerPrefs.SetInt("Score", score);
        latestScoreText.text = PlayerPrefs.GetInt("Score").ToString();
        ShowGameOverUI();
    }

    public void ShowGameOverUI()
    {
        gameOverObj.SetActive(true);
        upperUiObj.SetActive(false);
    }
    public bool CanStartGame()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        if (Input.GetMouseButtonDown(0) && !playerTouchedScreen)
        {
            playerTouchedScreen = true;
            canPlayGame = playerTouchedScreen;
            GameObject.Find("TouchScreenParent").SetActive(false);
            player.canJumpAgain = true;
        }
#else
        if(Touchscreen.current!=null && Touchscreen.current.primaryTouch.press.isPressed && !playerTouchedScreen){
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
