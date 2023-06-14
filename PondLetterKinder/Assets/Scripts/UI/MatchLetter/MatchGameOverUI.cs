using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class MatchGameOverUI : GameOverUI
{
    public Difficulty Difficulty { get; set; } = Difficulty.None;
    public int CorrectAnswers { get; set; } = 0;
    [SerializeField] private TMP_Text finalScoreText;
    private FireworksHandler fireworks;

    [HideInInspector] public int maxRoundCount;

    public static event UnityAction StartNewGame;

    [SerializeField] private AudioClip victory;

    private void OnEnable()
    {
        fireworks = FindObjectOfType<FireworksHandler>();

        EventManager.OnCorrectAnswer += Correct;
        EventManager.OnGameOver += HandleGameOver;
    }

    private void OnDisable()
    {
        EventManager.OnCorrectAnswer -= Correct;
        EventManager.OnGameOver -= HandleGameOver;
    }

    private void Correct() => CorrectAnswers++;

    public override void HandleGameOver()
    {
        base.HandleGameOver();

        if (Difficulty != Difficulty.Hard)
        {
            finalScoreText.enabled = false;
            StartCoroutine(fireworks.PlayFireworks());

            if (SoundManager.inst != null)
                SoundManager.PlaySound(victory);
        }
        else
        {
            finalScoreText.text = $"You scored {CorrectAnswers} / 10";
            if (CorrectAnswers > 5)
            {
                StartCoroutine(fireworks.PlayFireworks());

                if (SoundManager.inst != null)
                    SoundManager.PlaySound(victory);
            }
        }  
    }

    public override void HandleNewGame()
    {
        CorrectAnswers = 0;
        fireworks.StopFireworks();
        base.HandleNewGame();
        StartNewGame?.Invoke();
    } 
}
