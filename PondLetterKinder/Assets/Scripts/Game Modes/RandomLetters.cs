using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomLetters
{
    private readonly List<string> alphabet = new()
    {
        "A", "B", "C", "D", "E", "F", "G", "H",
        "I", "J", "K", "L", "M", "N", "O", "P",
        "Q", "R", "S", "T", "U", "V", "W", "X",
        "Y", "Z"
    };
    private readonly Difficulty _difficulty;
    private string _letterToMatch;

    public RandomLetters(Difficulty difficulty)
    {
        _difficulty = difficulty;
    }

    public void SetupGame()
    {
        if (_difficulty == Difficulty.Easy)
            TurtleManager.inst.SpawnTurtles(3);
        else
            TurtleManager.inst.SpawnTurtles(Random.Range(4, 7));

        // Select a Letter from the Alphabet and assign to one of the Turtles
        Turtle turtle = TurtleManager.inst.Turtles[Random.Range(0, TurtleManager.inst.Turtles.Count)];
        turtle.Letter = _letterToMatch = MatchLetterManager.inst.SetLetterToMatch(alphabet);

        // Randomly Generate a Letter for the remaining turtles
        foreach (Turtle t in TurtleManager.inst.Turtles)
        {
            if (t != turtle)
            {
                string l = alphabet[Random.Range(0, 25)];

                while (l == _letterToMatch) //1 in 26, but need to catch it
                    l = alphabet[Random.Range(0, 25)];

                t.Letter = l;
            }
        }

        TurtleManager.MassResurface();
    }
}
