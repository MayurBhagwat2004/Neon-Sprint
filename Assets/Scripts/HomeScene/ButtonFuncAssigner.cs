using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonFuncAssigner : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public enum ButtonType
    {
        Play,Settings,Quit,Back,Home,Store,Restart,None,FPS
    }

    public ButtonType buttonType;
    public Button currentButton;

    [SerializeField] private float scaleFactor = 1.2f;
    [SerializeField] private float speed = 10f;

    private Vector3 originalSize;
    private Coroutine scaleRoutine;


    void Awake()
    {
        originalSize = transform.localScale;
    }
    void Start()
    {
        if (GetComponent<Button>() != null)
        {
            currentButton = GetComponent<Button>();
            originalSize = transform.localScale;
        }

        if(SceneController.Instance != null)
        {
            switch (buttonType)
            {
                case ButtonType.Play:
                    currentButton.onClick.AddListener(GoToLevelFunc); //Call the Level Scene Function
                    break;
                case ButtonType.None:
                    //Do nothing
                    break;
                case ButtonType.FPS:
                    currentButton.onClick.AddListener(ToogleFPS); //Call the toggle method
                    break;

                case ButtonType.Home:
                    currentButton.onClick.AddListener(GoToHomeFunc); //Call the home scene loading function
                    break;
                case ButtonType.Settings:
                    currentButton.onClick.AddListener(OpenSettingsFunc); //Call the open settings loading function
                    break;
                case ButtonType.Store:
                    currentButton.onClick.AddListener(GoToStoreFunc); //Call the store scene loading function
                    break;
                case ButtonType.Restart:
                    currentButton.onClick.AddListener(RestartLevel); //Call the scene reloading function
                    break;
                case ButtonType.Quit:
                    currentButton.onClick.AddListener(QuitGameFunc); //Call the quit game function
                    break;

                default:
                break;
            }
        }
        else
        {
            Debug.Log("Scene Controller is not present in the scene!!!");
        }

    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        StopCurrentCoroutine();
        scaleRoutine = StartCoroutine(ScaleTo(originalSize * scaleFactor)); //Call the coroutine to scale the button
    }
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        StopCurrentCoroutine();
        scaleRoutine = StartCoroutine(ScaleTo(originalSize)); //Call the function to resize the button to its original size

    }
    private void StopCurrentCoroutine()
    {
        if(scaleRoutine != null)
        {
            StopCoroutine(scaleRoutine);
        }
    }


    private IEnumerator ScaleTo(Vector3 targetScale)
    {
        while (Vector3.Distance(transform.localScale,targetScale) > 0.001f)
        {
            transform.localScale = Vector3.Lerp(transform.localScale,targetScale,speed * Time.unscaledDeltaTime);
            yield return null;
        }

        transform.localScale = targetScale;
    }
    private void GoToLevelFunc()
    {
        // if(currentButton.OnPointerEnter());
        SceneController.Instance.StartLoadingScene(SceneController.GameScenes.Level);
    }

    private void GoToHomeFunc()
    {
        SceneController.Instance.StartLoadingScene(SceneController.GameScenes.Home);
    }

    private void OpenSettingsFunc()
    {
        if(UiManager.Instance != null)
        {
            UiManager.Instance.OpenSettingsPanel();
        }
    }
    private void GoToStoreFunc()
    {
        SceneController.Instance.StartLoadingScene(SceneController.GameScenes.Store);
    }

    private void RestartLevel()
    {
        SceneController.Instance.StartLoadingScene(SceneController.GameScenes.Level);
    }

    private void QuitGameFunc()
    {
        SceneController.Instance.QuitGame();
    }

    void OnDisable()
    {
        transform.localScale = originalSize;
    }
    private void ToogleFPS()
    {
        FpsManager.Instance.ToggleFPS();
    }
}
