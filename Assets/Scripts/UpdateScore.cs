using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpdateScore : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    void Start()
    {
        if (scoreText != null)
            scoreText.text = PlayerPrefs.GetInt("Score").ToString() ;
    }

    void Update()
    {
        
    }
}
