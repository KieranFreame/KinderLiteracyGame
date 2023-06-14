using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IMatch
{
    public Difficulty Difficulty { get; set; }
    public event UnityAction OnCorrectAnswer;
}
