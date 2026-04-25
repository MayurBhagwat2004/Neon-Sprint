using UnityEngine;
using System;
public class LevelEvents : MonoBehaviour
{
    public static event Action OnGameStarted;
    public static event Action OnSpeedIncreased;
    public static event Action OnGameOver;
    public static event Action OnObstacleHit;
    public static event Action OnPlayerLiftedFinger;
    public static event Action OnEnergyBarAcquired;
    public static void StartTheGame()
    {
        OnGameStarted?.Invoke(); //Fires the event indicating to start the game
    }

    public static void GameOver()
    {
        OnGameOver?.Invoke();
    }
    public static void InvokeTheSpeedModifier()
    {
        OnSpeedIncreased?.Invoke();
    }

    public static void InvokeGameOver()
    {
        OnGameOver?.Invoke();
    }

    public static void OnObstacleHitted()
    {
        OnObstacleHit?.Invoke();
    }

    public static void OnPlayerRemovedFinger()
    {
        OnPlayerLiftedFinger?.Invoke();
    }

    public static void OnEnergyBarDetected()
    {
        OnEnergyBarAcquired?.Invoke();
    }
}
