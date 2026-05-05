using TMPro;
using UnityEngine;

public class HighScoreDisplay : MonoBehaviour
{
    public TextMeshProUGUI highScoreText;
    private int highScore;
    private const string HIGHSCORE_KEY = "HighScore";
    void Start()
    {
        if (PlayerPrefs.HasKey(HIGHSCORE_KEY)) //Check if the key exists
        {
            highScore = PlayerPrefs.GetInt(HIGHSCORE_KEY); //Get the highscore and if not present set the default highscore as 0
            highScoreText.text = highScore + "m";
        }
        else
        {
            PlayerPrefs.SetInt(HIGHSCORE_KEY,0);
            highScoreText.text = "0m";
        }
    }

}
