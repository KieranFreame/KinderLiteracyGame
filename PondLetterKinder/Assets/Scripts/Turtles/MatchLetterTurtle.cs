using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchLetterTurtle : Turtle
{
    private bool isGameOver = false;
    public TextMesh TurtText { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        isGameOver = false;

        _aboveWaterPos = new Vector3(transform.position.x, 0, transform.position.z);
        _belowWaterPos = new Vector3(transform.position.x, -1, transform.position.z);

        if (!isAboveWater)
            transform.position = _belowWaterPos;

        TurtText = GetComponentInChildren<TextMesh>();
    }

    private void OnEnable()
    {
        isGameOver = false;
        TurtleManager.inst.Dive += HandleDive;
        TurtleManager.inst.Resurface += HandleResurface;
        EventManager.OnGameOver += GameOver;

    }

    private void OnDisable()
    {
        TurtleManager.inst.Dive -= HandleDive;
        TurtleManager.inst.Resurface -= HandleResurface;
        EventManager.OnIncorrectAnswer -= HandleDive;
        EventManager.OnGameOver -= GameOver;
    }

    private void GameOver() => isGameOver = true;

    private void OnMouseDown()
    {
        if (isGameOver) { return; }

        if (MatchLetterManager.inst.Difficulty != Difficulty.Hard)
            EventManager.OnIncorrectAnswer += HandleDive;

        MatchLetterManager.inst.CompareLetters(this);
    }
}
