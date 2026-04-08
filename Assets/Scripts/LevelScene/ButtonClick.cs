using UnityEngine;
using UnityEngine.UI;

public class ButtonClick : MonoBehaviour
{
    public Button button;
    public bool openPausePanel;
    public bool goToHome;
    void Start()
    {
        button = GetComponent<Button>();
    }

    void Update()
    {
    }

}
