using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class FpsManager : MonoBehaviour
{
    public static FpsManager Instance;
    public TextMeshProUGUI fpsCounterText;
    private int currentTargetIndex = 0;
    private int[] fpsOptions = {30,60,120,-1};
    private int monitorRefresh;
    void Awake()
    {
        if(Instance != null && Instance != this) Destroy(gameObject);
        else Instance = this;

        
    }


  
    private void Start()
    {

        monitorRefresh = (int)Screen.currentResolution.refreshRateRatio.value;
        
        if (monitorRefresh <= 60) SetFPS(0);
        else if(monitorRefresh > 60) SetFPS(2);
        else SetFPS(1);
    }

    public void ToggleFPS()
    {
        currentTargetIndex++;
        if(currentTargetIndex >= fpsOptions.Length) currentTargetIndex = 0;

        SetFPS(currentTargetIndex);

    
    }
    
    private void SetFPS(int index)
    {
        currentTargetIndex = index;
        int target = fpsOptions[currentTargetIndex];

        if(target == -1)
        {
            Debug.Log("Max");
            fpsCounterText.text = "MAX";
        }
        else
        {
            fpsCounterText.text = target.ToString();
        }

        Application.targetFrameRate = target;
        QualitySettings.vSyncCount = 0;
    }


}
