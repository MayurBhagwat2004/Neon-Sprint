using UnityEngine;
using UnityEngine.UI;

public class ButtonFuncAssigner : MonoBehaviour
{
    public enum ButtonType
    {
        Play,Settings,Quit,Back,Home,Store
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

                case ButtonType.Store:
                    currentButton.onClick.AddListener(GoToStoreFunc);
                    break;

                case ButtonType.Quit:
                    SceneController.Instance.QuitGame();
                    break;

                default:
                break;
            }
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

    private void GoToStoreFunc()
    {
        SceneController.Instance.StartLoadingScene(SceneController.GameScenes.Store);
    }

    

}
