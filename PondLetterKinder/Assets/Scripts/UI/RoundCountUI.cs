using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoundCountUI : MonoBehaviour
{
    private int currRound = 0;
    [SerializeField] private TMP_Text roundCountText;

    private void OnEnable()
    {
        EventManager.OnStartGame += EnableUI;
        EventManager.OnNewRound += UpdateUI;
        EventManager.OnGameOver += DisableUI;
    }

    private void OnDisable()
    {
        EventManager.OnStartGame -= EnableUI;
        EventManager.OnNewRound -= UpdateUI;
        EventManager.OnGameOver -= DisableUI;
    }

    public void UpdateUI()
    {
        currRound++;
        roundCountText.text = $"{currRound}/10";
    }

    public void EnableUI()
    {
        roundCountText.gameObject.SetActive(true);
    }

    public void DisableUI()
    {
        roundCountText.gameObject.SetActive(false);
    }
}
