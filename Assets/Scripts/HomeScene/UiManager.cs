using System.Collections;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    [Header("UI Groups")]
    [SerializeField] private CanvasGroup homeGroup;
    [SerializeField] private CanvasGroup settingsGroup;

    [SerializeField] private float fadeDuration = 0.3f; //Transition duration for the buttons

    public void OpenSettings()
    {
        StartCoroutine(FadeUI(homeGroup,0,false));
        StartCoroutine(FadeUI(settingsGroup,1,true));
    }

    public void CloseSettings()
    {
        StartCoroutine(FadeUI(settingsGroup,0,false));
        StartCoroutine(FadeUI(homeGroup,1,true));
    }

    private IEnumerator FadeUI(CanvasGroup group,float targetAlpha,bool isOpening)
    {
        float startAlpha = group.alpha;
        float time = 0;

        //If we are opening, enable interactions immediately
        if (isOpening)
        {
            group.interactable = true;
            group.blocksRaycasts = true;
        }

        //If we are closing, disable interactions immediately
        else
        {
            group.interactable = false;
            group.blocksRaycasts = false;
        }

        while(time < fadeDuration)
        {
            time += Time.deltaTime;
            group.alpha = Mathf.Lerp(startAlpha,targetAlpha,time/fadeDuration);
            yield return null;
        }

        group.alpha = targetAlpha;
    }
    
}
