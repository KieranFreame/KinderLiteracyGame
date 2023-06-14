using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MatchLetterManager : MonoBehaviour, IGameManager
{
    public static MatchLetterManager inst;
    private TextMesh rock;
    public string LetterToMatch { get; private set; }
    public Difficulty Difficulty { get; set; } = Difficulty.None;
    public string GameType { get; set; } = string.Empty;

    [SerializeField] private AudioClip correct;
    [SerializeField] private AudioClip incorrect;
    [SerializeField] private int maxRounds = 10;
    private int roundCount;

    private RandomLetters _randomLetters;
    private SimilarLetters _similarLetters;

    public bool GameOver { get; set; } = false;

    //UI Elements
    private MatchGameOverUI _gameOverUI;
    private RoundCountUI _roundCountUI;

    private void Awake()
    {
        if (inst == null)
            inst = this;
        else
            Destroy(this);

        rock = GameObject.Find("RockForDisplay").GetComponentInChildren<TextMesh>();

        _gameOverUI = FindObjectOfType<MatchGameOverUI>();
        _roundCountUI = FindObjectOfType<RoundCountUI>();
    }
    public void StartGame()
    {
        _roundCountUI.EnableUI();

        Pooler.OnStart();

        if (FindObjectOfType<SoundManager>() != null )
            SoundManager.StopBackground();

        roundCount = 0;
        GameOver = false;

        MatchGameOverUI.StartNewGame -= StartGame;

        StartNewRound();
    }
    private void StartNewRound()
    {
        if (GameOver) { return; }

        rock.color = Color.white;

        roundCount++;

        if (roundCount > maxRounds) //i.e. roundCount is 11
        {
            MatchGameOverUI.StartNewGame += StartGame;
            GameOver = true;

            EventManager.InvokeGameOver();
            return;
        }

        EventManager.InvokeNewRound();
        switch (GameType)
        {
            case "random letters":
                _randomLetters ??= new(Difficulty);
                _randomLetters.SetupGame();
                break;
            case "similar letters":
                _similarLetters ??= new();
                _similarLetters.SetupGame();
                break;
        }
        
    }
    public string SetLetterToMatch(List<string> letterPool)
    {
        LetterToMatch = letterPool[Random.Range(0, letterPool.Count)];
        rock.text = LetterToMatch;
        return LetterToMatch;
    }
    public void CompareLetters(Turtle turtle)
    {
        if (turtle.Letter == LetterToMatch)
        {
            rock.color = Color.green;

            if (FindObjectOfType<SoundManager>() != null)
                SoundManager.PlaySound(correct);

            EventManager.InvokeCorrectAnswer();
            Invoke(nameof(StartNewRound), 1f);
        }
        else
        {
            rock.color = Color.red;

            if (FindObjectOfType<SoundManager>() != null)
                SoundManager.PlaySound(incorrect);

            EventManager.InvokeIncorrectAnswer();

            if (Difficulty == Difficulty.Hard)
            {
                Invoke(nameof(StartNewRound), 1f);
            }
        }    
    }

    private void HandleGameOver()
    {
        
    }
}
