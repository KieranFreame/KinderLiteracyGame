using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimilarLettersDatabase
{
    private readonly List<string> SimilarLettersI = new()
    {
        "V", "W", "M", "N"
    };

    private readonly List<string> SimilarLettersII = new()
    {
        "E", "F"
    };

    private readonly List<string> SimilarLettersIII = new()
    {
        "R", "P"
    };

    private readonly List<string> SimilarLettersIV = new()
    {
        "B", "D"
    };

    private readonly List<string> SimilarLettersV  = new()
    {
        "U", "Q", "O", "C", "G", "D"
    };

    private readonly List<string> SimilarLettersVI = new()
    {
       "X", "Y"
    };

    public List<string> GetSimilarLetters(int index)
    {
        return index switch
        {
            0 => SimilarLettersI,
            1 => SimilarLettersII,
            2 => SimilarLettersIII,
            3 => SimilarLettersIV,
            4 => SimilarLettersV,
            5 => SimilarLettersVI,
            _ => null,
        };
    }
}
