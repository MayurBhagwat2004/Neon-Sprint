using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public Player player;
    private bool playerTouchedScreen;
    public PlayerInput touchInput;
    public bool canPlayGame;
    void Awake()
    {
        if (Instance != this && Instance != null) Destroy(gameObject);
        else Instance = this;
    }

    void Start()
    {
        
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
