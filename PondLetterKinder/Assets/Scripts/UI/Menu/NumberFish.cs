using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class NumberFish : MonoBehaviour
{
    public int AssignedNum { get; set; } = int.MaxValue;
    [SerializeField] private TMP_Text numText;

    [Header("Movement Parameters")]
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform stopPoint;
    [SerializeField] private Transform exitPoint;
    [SerializeField] private float timeToMove;

    private void OnEnable()
    {
        EventManager.OnCorrectAnswer += EndRound;
        EventManager.OnNewRound += StartRound;
    }

    private void OnDisable()
    {
        EventManager.OnCorrectAnswer -= EndRound;
        EventManager.OnNewRound -= StartRound;
    }

    public void StartRound()
    {   
        StartCoroutine(MoveFish(startPoint, stopPoint));
        
    }

    public void EndRound()
    {
        EventManager.OnIncorrectAnswer -= Incorrect;

        numText.gameObject.SetActive(false);
        StartCoroutine(MoveFish(stopPoint, exitPoint));
    }

    private void OnMouseDown()
    {
        EventManager.OnIncorrectAnswer += Incorrect;
        numText.gameObject.SetActive(false);
        NumbersManager.inst.CompareNumbers(AssignedNum);
    }

    private void Incorrect()
    {
        EventManager.OnIncorrectAnswer -= Incorrect;
        StartCoroutine(MoveFish(stopPoint, exitPoint));
    } 

    private IEnumerator MoveFish(Transform start, Transform target)
    {
        if (transform.position != start.position)
            yield break;

        Vector3 startPos = start.position;
        Vector3 targetPos = target.position;

        float timeElapsed = 0;

        while (timeElapsed < timeToMove)
        {
            transform.position = Vector3.Lerp(startPos, targetPos, timeElapsed / timeToMove);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPos;

        if (targetPos == stopPoint.position)
        {
            numText.text = AssignedNum.ToString();
            numText.gameObject.SetActive(true);
        }
        else
            transform.position = startPoint.position;
    }
}
