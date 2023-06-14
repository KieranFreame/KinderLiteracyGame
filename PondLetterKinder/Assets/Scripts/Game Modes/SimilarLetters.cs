using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimilarLetters
{
    private readonly SimilarLettersDatabase database = new();
    List<string> letters = new();
    private string _letterToMatch;

    public SimilarLetters() { }

    public void SetupGame()
    {
        int i = Random.Range(0, 6);

        letters.AddRange(database.GetSimilarLetters(i));
        TurtleManager.inst.SpawnTurtles(letters.Count);

        Turtle turtle = TurtleManager.inst.Turtles[Random.Range(0, TurtleManager.inst.Turtles.Count)];
        turtle.Letter = _letterToMatch = MatchLetterManager.inst.SetLetterToMatch(letters);

        letters.Remove(_letterToMatch);

        // Randomly Generate a Letter for the remaining turtles
        foreach (Turtle t in TurtleManager.inst.Turtles)
        {
            if (t != turtle)
            {
                string l = letters[Random.Range(0, letters.Count)];

                t.Letter = l;

                letters.Remove(l);
            }
        }

        TurtleManager.MassResurface();
    }
}
