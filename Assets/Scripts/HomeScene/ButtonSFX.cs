using UnityEngine;
using UnityEngine.UI;
public class ButtonSFX : MonoBehaviour
{
    public Button button;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(PlayClickSound);
    }

    public void PlayClickSound()
    {
        if(SoundManager.Instance == null) return; //Return if the sound manager is not available

        SoundManager.Instance.PlayClickSound(); //Play the button click sound
    }

} 
 