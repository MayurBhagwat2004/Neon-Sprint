using System.Collections;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    [Header("UI Groups")]
    [SerializeField] private CanvasGroup homeGroup;
    [SerializeField] private CanvasGroup settingsGroup;

    [SerializeField] private float fadeDuration = 0.3f; //Transition duration for the buttons
    [SerializeField] private float transitionTime = 0.4f;

    public void OpenSettings()
    {
        StartCoroutine(OpenPanelRoutine(homeGroup,1,0));
        StartCoroutine(OpenPanelRoutine(settingsGroup,0,1));
    }

    public void CloseSettings()
    {
        StartCoroutine(OpenPanelRoutine(settingsGroup,1,0));
        StartCoroutine(OpenPanelRoutine(homeGroup,0,1));
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

    private IEnumerator OpenPanelRoutine(CanvasGroup group,float initialValue,float targetValue)
    {
        float elapsedTime = 0f;
        group.alpha = initialValue;

        while (elapsedTime < transitionTime)
        {
            float t = elapsedTime / transitionTime;
            group.alpha = Mathf.Lerp(initialValue,targetValue,t); //Incrementing the alpha value gradually

            elapsedTime += Time.unscaledDeltaTime;
            yield return null;
        }
        group.alpha = targetValue;

        group.interactable = true; //Make the buttons available for clicking
        group.blocksRaycasts = true;//Detect the clicks on the button
    }
    
}
