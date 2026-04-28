using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBarManager : MonoBehaviour
{
    [SerializeField] private Slider energyBar;
    [Header("Slider filling variables")]
    private readonly float maximumBarValue = 1f;
    private readonly float minimumBarValue = 0f;
    [SerializeField] private float duration = .2f;
    public float energyBarDecreasingSpeed = 0.02f;
    public float incrementValue = 0.5f; //Value to fill the energy bar
    public float decrementValue = 0.5f; //Value to decrement the energy bar
    void OnEnable()
    {
        LevelEvents.OnEnergyBarAcquired += IncreaseEnergyBar;
        LevelEvents.OnObstacleHit += DecreaseEnergyBar;
        LevelEvents.OnGameStarted += SlowlyDecreaseEnergyBar;
    }

    void OnDisable()
    {
        LevelEvents.OnEnergyBarAcquired -= IncreaseEnergyBar;
        LevelEvents.OnObstacleHit -= DecreaseEnergyBar;
        LevelEvents.OnGameStarted -= SlowlyDecreaseEnergyBar;
        
    }

    void Start()
    {
        if (energyBar == null)
        {
            energyBar = transform.GetChild(0).GetComponent<Slider>();
        }

        SetInitialBarValue();
    }

    

    private void SetInitialBarValue()
    {
        energyBar.maxValue = maximumBarValue;
        energyBar.minValue = minimumBarValue;
        
        energyBar.value = maximumBarValue;
    }

    public void IncreaseEnergyBar()
    {
        StartCoroutine(ChangeEnergyBarRoutine(incrementValue));
    }

    public void DecreaseEnergyBar()
    {
        StartCoroutine(ChangeEnergyBarRoutine(-decrementValue));
    }

    public void SlowlyDecreaseEnergyBar()
    {
        StartCoroutine(SlowlyDecreaseEnergyBarRoutine());
    }

    private IEnumerator ChangeEnergyBarRoutine(float amountToChange)
    {
        float elapsedTime = 0f;

        float startValue = energyBar.value;
        float targetValue = Mathf.Clamp(startValue + amountToChange,minimumBarValue,maximumBarValue);
        
        while (elapsedTime < duration)
        {
            float t = elapsedTime/duration;
            
            energyBar.value = Mathf.Lerp(startValue,targetValue,t);
            
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        energyBar.value = targetValue;

    }

    private IEnumerator SlowlyDecreaseEnergyBarRoutine()
    {
        while (energyBar.value > 0)
        {
            if(GameManager.Instance.gameEnded) break; //Stop decreasing the energy bar when the game is ended

            energyBar.value -= energyBarDecreasingSpeed * Time.deltaTime;
            yield return null;            
        }

        energyBar.value = 0f;

        LevelEvents.GameOver(); //Trigger the game over event

    }

}



