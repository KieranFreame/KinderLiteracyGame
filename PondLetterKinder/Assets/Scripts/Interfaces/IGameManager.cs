using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IGameManager
{
    public static IGameManager inst;

    public bool GameOver { get; set; }
    
    public event UnityAction OnGameStart { add { } remove { } }
    public event UnityAction<int> NewRoundStarted { add { } remove { } }
    public event UnityAction OnGameOver { add { } remove { } }
    public void StartGame();
}
