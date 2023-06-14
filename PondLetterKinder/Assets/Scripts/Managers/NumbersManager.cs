using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class NumbersManager : MonoBehaviour, IGameManager
{
    public static NumbersManager inst;
    private int numberToMatch;
    [SerializeField] private TMP_Text lilypad;

    [Header ("Audio Parameters")]
    [SerializeField] private AudioClip correct;
    [SerializeField] private AudioClip incorrect;

    [Header("Round Parameters")]
    [SerializeField] private int maxRounds = 10;
    int roundCount = 0;

    public bool GameOver { get; set; } = false;

    readonly List<NumberFish> fishList = new();

    private void Awake()
    {
        if (inst == null)
            inst = this;
        else
            Destroy(this);
    }

    public Difficulty Difficulty { get; set; } = Difficulty.None;

    public void StartGame()
    {
        EventManager.InvokeStartGame();
        MatchGameOverUI.StartNewGame -= StartGame;

        fishList.AddRange(FindObjectsOfType<NumberFish>());

        if (FindObjectOfType<SoundManager>() != null)
            SoundManager.StopBackground();

        roundCount = 0;
        GameOver = false;

        StartNewRound();
    }

    private void StartNewRound()
    {
        if (GameOver) { return; }

        lilypad.color = Color.white;

        roundCount++;

        if (roundCount > maxRounds)
        {
            GameOver = true;
            MatchGameOverUI.StartNewGame += StartGame;
            EventManager.InvokeGameOver();
            return;
        }

        Setup();
    }

    private void Setup()
    {
        int numToAssign;
        var fish = fishList[Random.Range(0, fishList.Count)];
        fish.AssignedNum = numberToMatch = Random.Range(1, 21);

        lilypad.text = numberToMatch.ToString();

        foreach (var f in fishList)
        {
            if (f == fish) continue;

            do{
                numToAssign = Random.Range(1, 21);
            } while (numToAssign == numberToMatch);

            f.AssignedNum = numToAssign;
        }

        EventManager.InvokeNewRound();
    }

    public void CompareNumbers(int number)
    {
        if (number == numberToMatch)
        {
            lilypad.color = Color.green;

            if (FindObjectOfType<SoundManager>() != null)
                SoundManager.PlaySound(correct);

            EventManager.InvokeCorrectAnswer();
        }
        else
        {
            lilypad.color = Color.red;

            if (FindObjectOfType<SoundManager>() != null)
                SoundManager.PlaySound(incorrect);

            EventManager.InvokeIncorrectAnswer();

            if (Difficulty != Difficulty.Hard)
                return;
        }

        Invoke(nameof(StartNewRound), 3.5f);
    }
}
