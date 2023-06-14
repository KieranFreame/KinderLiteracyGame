using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurtainMovement : MonoBehaviour
{
    [SerializeField] private Vector2 closedPosition;
    [SerializeField] private Vector2 openPosition;
    public float timeToMove;
    private bool CurtainClosed = true;

    [SerializeField] private AudioClip curtainsDraw;

    private void Start()
    {
        Invoke(nameof(HandleMovement), 1f);
    }

    public void HandleMovement()
    {
        StartCoroutine(MoveCurtain());
    }

    private IEnumerator MoveCurtain()
    {
        Vector2 startingPos = (CurtainClosed ? closedPosition : openPosition);
        Vector2 targetPos = (CurtainClosed ? openPosition : closedPosition);
        SoundManager.PlaySound(curtainsDraw);
        float timeElapsed = 0;

        while (timeElapsed < timeToMove)
        {
            transform.localPosition = Vector2.Lerp(startingPos, targetPos, timeElapsed / timeToMove);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = targetPos;
        CurtainClosed = !CurtainClosed;
    }
}
