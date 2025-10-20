using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private bool playerTouchedScreen;
    public PlayerInput touchInput;

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
            GameObject.Find("TouchScreenParent").SetActive(false);

        }
#else
        if(Touchscreen.current!=null && Touchscreen.current.primaryTouch.press.isPressed && !playerTouchedScreen){
            playerTouchedScreen = true;
            GameObject.Find("TouchScreenParent").SetActive(false);
            
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
