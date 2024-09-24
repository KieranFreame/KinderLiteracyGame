using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SpellingGameOverUI : GameOverUI
{
    private void OnEnable()
    {
        EventManager.OnGameOver += HandleGameOver;
    }

    private void OnDisable()
    {
        EventManager.OnGameOver -= HandleGameOver;
    }
}
