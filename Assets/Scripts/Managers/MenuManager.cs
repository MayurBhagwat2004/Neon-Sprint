using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;
    void Awake()
    {
        if(Instance != this && Instance != null) Destroy(this);
        else Instance = this;        
    }

    public void ChangeGraphics(int graphicIndex)
    {
        QualitySettings.SetQualityLevel(graphicIndex,true); 
    }


}
