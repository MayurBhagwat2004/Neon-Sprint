using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public Player player;
    private bool playerTouchedScreen;
    public PlayerInput touchInput;
    public bool canPlayGame;
    public int score;
    [SerializeField]private TextMeshProUGUI scoreText;
    void Awake()
    {
        if (Instance != this && Instance != null) Destroy(gameObject);
        else Instance = this;
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
