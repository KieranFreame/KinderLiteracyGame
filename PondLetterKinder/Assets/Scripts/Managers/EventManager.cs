using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    public static event UnityAction OnStartGame;
    public static event UnityAction OnNewRound;
    public static event UnityAction OnCorrectAnswer;
    public static event UnityAction OnIncorrectAnswer;
    public static event UnityAction OnGameOver;

    public static void InvokeStartGame() => OnStartGame?.Invoke();
    public static void InvokeCorrectAnswer() => OnCorrectAnswer?.Invoke();
    public static void InvokeIncorrectAnswer() => OnIncorrectAnswer?.Invoke();
    public static void InvokeGameOver() => OnGameOver?.Invoke();
    public static void InvokeNewRound() => OnNewRound?.Invoke();
}
