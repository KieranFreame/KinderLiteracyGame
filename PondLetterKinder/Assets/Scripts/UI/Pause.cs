using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    private bool isPaused = false;

    private void Awake()
    {
        if (pausePanel.activeSelf)
            pausePanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            HandlePause();
    }

    public void HandlePause()
    {
        isPaused = !isPaused;

        Time.timeScale = (isPaused ? 0 : 1);
        pausePanel.SetActive(isPaused);
    }

    public void BackToMenu()
    {
        HandlePause();
        SceneTransitioner.inst.HandleChangeScene("Menu");
    }

    public void Restart()
    {
        HandlePause();
        SceneTransitioner.inst.HandleChangeScene(SceneManager.GetActiveScene().name);
    }
}
