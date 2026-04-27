using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance;
    [Header("Loading Screen UI")]
    public CanvasGroup loadingScreenGroup;
    public Slider loadingScreenSlider;
    public TextMeshProUGUI loadingText;

    public enum GameScenes { Home, Level, Store }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    void Start()
    {
        if (loadingScreenGroup != null)
        {
            loadingScreenGroup.alpha = 0;
            loadingScreenGroup.blocksRaycasts = false;
            loadingScreenGroup.interactable = false;

            loadingScreenGroup.gameObject.SetActive(false);

        }
    }



    public void StartLoadingSceneFromString(string sceneName)
    {
        if (Enum.TryParse(sceneName, out GameScenes gameScenes))
        {
            StartLoadingScene(gameScenes);
        }
    }

    public void StartLoadingScene(GameScenes sceneName)
    {
        StartCoroutine(LoadSceneAsyncRoutine(sceneName));
    }


    private IEnumerator LoadSceneAsyncRoutine(GameScenes sceneName)
    {
        loadingScreenGroup.alpha = 1;
        loadingScreenGroup.blocksRaycasts = true;
        loadingScreenGroup.interactable = true;

        string levelName = sceneName.ToString(); //Convert the enum to string

        AsyncOperation operation = SceneManager.LoadSceneAsync(levelName);

        while (operation.progress < 0.9f)
        {
            float visualProgress = Mathf.Clamp01(operation.progress);

            if (loadingScreenSlider != null) loadingScreenSlider.value = visualProgress; //Update the slider value based on progress

            if (loadingText != null) loadingText.text = "Loading: " + visualProgress;

            yield return null;
        }

        loadingScreenGroup.alpha = 0;
        loadingScreenGroup.blocksRaycasts = false;
        loadingScreenGroup.interactable = false;

    }
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; //Quitting the game in the editor
#else
                Application.Quit(); //Quitting the game on android device
#endif
    }
}
