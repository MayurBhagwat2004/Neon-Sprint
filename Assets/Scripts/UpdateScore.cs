using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpdateScore : MonoBehaviour
{
    public TextMeshProUGUI highScoreText;
    void Start()
    {
        if (highScoreText != null)
            highScoreText.text = PlayerPrefs.GetInt("Score").ToString();
    }

    public void UpdateHighScore(int score)
    {
        highScoreText.text = score.ToString();
    }
    void Update()
    {
        
    }
}
