using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] protected GameObject gameOverPanel;

    public virtual void HandleGameOver()
    {
        gameOverPanel.SetActive(true);
    }

    public virtual void HandleNewGame()
    {
        Time.timeScale = 1;
        gameOverPanel.SetActive(false);
    }
    public void HandleRestart()
    {
        Time.timeScale = 1;
        gameOverPanel.SetActive(false);
        SceneTransitioner.inst.HandleChangeScene(SceneManager.GetActiveScene().name);
    }
    public void HandleReturnToMenu()
    {
        Time.timeScale = 1;
        gameOverPanel.SetActive(false);
        SceneTransitioner.inst.HandleChangeScene("Menu");
    }
}
