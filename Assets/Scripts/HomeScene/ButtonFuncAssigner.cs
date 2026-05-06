using UnityEngine;
using UnityEngine.UI;

public class ButtonFuncAssigner : MonoBehaviour
{
    public enum ButtonType
    {
        Play,Settings,Quit,Back,Home,Store,Restart,Volume
    }

    public ButtonType buttonType;
    public Button currentButton;
    void Start()
    {
        if (GetComponent<Button>() != null)
        {
            currentButton = GetComponent<Button>();
        }

        if(SceneController.Instance != null)
        {
            switch (buttonType)
            {
                case ButtonType.Play:
                    currentButton.onClick.AddListener(GoToLevelFunc);
                    break;

                case ButtonType.Home:
                    currentButton.onClick.AddListener(GoToHomeFunc);
                    break;
                case ButtonType.Settings:
                    currentButton.onClick.AddListener(OpenSettingsFunc);
                    break;
                case ButtonType.Store:
                    currentButton.onClick.AddListener(GoToStoreFunc);
                    break;
                case ButtonType.Restart:
                    currentButton.onClick.AddListener(RestartLevel);
                    break;
                case ButtonType.Quit:
                    currentButton.onClick.AddListener(QuitGameFunc);
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

    private void GoToLevelFunc()
    {
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

    

}
