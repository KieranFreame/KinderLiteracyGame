using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NumberSettingsHandler : MonoBehaviour
{
    private CameraMovement _cam;
    [SerializeField] private TMP_Dropdown difficulty;
    private void Awake()
    {
        _cam = FindObjectOfType<CameraMovement>();
    }

    public void SetDifficulty(TMP_Text difficulty)
    {
        NumbersManager.inst.Difficulty = difficulty.text.ToLower() switch
        {
            "easy" => Difficulty.Easy,
            "hard" => Difficulty.Hard,
            _ => Difficulty.None,
        };
    }

    public void HandleStartGame()
    {
        if (NumbersManager.inst.Difficulty == Difficulty.None)
            return;

        FindObjectOfType<MatchGameOverUI>().Difficulty = NumbersManager.inst.Difficulty;
        StartCoroutine(StartGame());
    }

    private IEnumerator StartGame()
    {
        _cam.HandleMovement();

        yield return new WaitForSeconds(_cam.timeToMove);

        NumbersManager.inst.StartGame();
    }
}
