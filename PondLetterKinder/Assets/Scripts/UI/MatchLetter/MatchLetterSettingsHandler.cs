using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MatchLetterSettingsHandler : MonoBehaviour
{
    private CameraMovement _cam;
    [SerializeField] private TMP_Dropdown difficulty;
    private void Awake()
    {
        _cam = FindObjectOfType<CameraMovement>();
    }

    public void SetDifficulty(TMP_Text difficulty)
    {
        switch (difficulty.text.ToLower())
        {
            case "easy":
                MatchLetterManager.inst.Difficulty = Difficulty.Easy;
                break;
            case "hard":
                MatchLetterManager.inst.Difficulty = Difficulty.Hard;
                break;
            default:
                MatchLetterManager.inst.Difficulty = Difficulty.None;
                break;
        }
    }

    public void SetGameType(TMP_Text gameType)
    {
        MatchLetterManager.inst.GameType = gameType.text.ToLower();
    }

    public void HandleStartGame()
    {
        if (MatchLetterManager.inst.Difficulty == Difficulty.None)
            return;

        if (MatchLetterManager.inst.GameType == string.Empty || MatchLetterManager.inst.GameType == "select game mode")
            return;

        StartCoroutine(StartGame());
    }

    private IEnumerator StartGame()
    {
        _cam.HandleMovement(true);

        yield return new WaitForSeconds(_cam.timeToMove);

        MatchLetterManager.inst.StartGame();
    }
}
