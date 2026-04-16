using UnityEngine;
using System;
public class LevelEvents : MonoBehaviour
{
    public static event Action OnGameStarted;
    public static event Action OnSpeedIncreased;
    public static event Action OnGameOver;
    public static event Action OnObstacleHit;
    public static void StartTheGame()
    {
        OnGameStarted?.Invoke(); //Fires the event indicating to start the game
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
}
