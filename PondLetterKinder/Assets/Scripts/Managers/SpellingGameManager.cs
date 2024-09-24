using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpellingGameManager : MonoBehaviour, IGameManager
{
    public static SpellingGameManager inst;

    private readonly List<string> alphabet = new()
    {
        "A", "B", "C", "D", "E", "F", "G", "H",
        "I", "J", "K", "L", "M", "N", "O", "P",
        "Q", "R", "S", "T", "U", "V", "W", "X",
        "Y", "Z"
    };

    private readonly List<string> _lettersToMatch = new();
    private readonly List<SpawnPoint> lilyPadSpawns = new();

    public bool GameOver { get; set; } = false;

    [Header("Turtle Parameters")]
    [SerializeField] private GameObject turtlePrefab;
    [SerializeField] private Transform turtleParent;
    private readonly List<Transform> turtleSpawns = new();
    private readonly List<Turtle> turtles = new();
    private int currTurtleIndex = 0;
    private int currTurtSpawnIndex;

    [Header("SFX")]
    [SerializeField] private AudioClip correct;
    [SerializeField] private AudioClip incorrect;
    [SerializeField] private AudioClip gameOver;

    private void Awake()
    {
        inst = this;
        lilyPadSpawns.AddRange(FindObjectsOfType<SpawnPoint>(true));
        
        foreach (Transform child in turtleParent)
            turtleSpawns.Add(child);
    }

    public void StartGame()
    {
        GameObject.Find("Main Camera").GetComponent<CameraMovement>().HandleMovement();

        Pooler.OnStart();

        if (FindObjectOfType<SoundManager>() != null)
            SoundManager.StopBackground();

        foreach (SpawnPoint sp in lilyPadSpawns)
        {
            sp.gameObject.SetActive(true);
            sp.StartSpawning();
        }

        NextLetter();
    }

    private void NextLetter()
    {
        SpawnTurtle();
        turtles[currTurtleIndex].Letter = _lettersToMatch[0];
        (turtles[currTurtleIndex] as SpellingTurtle).IsTurtleToMatch = true;
        turtles[currTurtleIndex].HandleResurface();
    }

    private void SpawnTurtle()
    {
        var turt = Instantiate(turtlePrefab, turtleSpawns[currTurtSpawnIndex]);
        turtles.Add(turt.GetComponentInChildren<Turtle>());
    }


    public string AssignLetter()
    {
        float i = Random.Range(0f, 1f);

        if (!GameOver)
            return i <= 0.2 ? _lettersToMatch[0] : alphabet[Random.Range(0, 25)];
        else
            return alphabet[Random.Range(0, 25)];
    }

    public void SetName(string name)
    {
        for (int i = 0; i < name.Length; i++)
        {
            _lettersToMatch.Add(name[i].ToString().ToUpper());
        }

        currTurtSpawnIndex = 9 - _lettersToMatch.Count;
        Invoke(nameof(StartGame), 0.5f);
    }

    public void CompareLetter(string letter)
    {
        if (GameOver)
            return;

        if (letter == _lettersToMatch[0])
        {
            _lettersToMatch.RemoveAt(0);
            (turtles[currTurtleIndex] as SpellingTurtle).IsTurtleToMatch = false;

            if (_lettersToMatch.Count == 0)
            {
                GameOver = true;

                if (SoundManager.inst != null)
                    SoundManager.PlaySound(gameOver);

                EventManager.InvokeGameOver();
            }
            else
            {
                if (SoundManager.inst != null)
                    SoundManager.PlaySound(correct);

                currTurtleIndex++;
                currTurtSpawnIndex += 2;
                NextLetter();
            }
        }
        else { if (SoundManager.inst != null)
                SoundManager.PlaySound(incorrect); }
    }
}
