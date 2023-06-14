using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Header("Movement Toggles")]
    //[SerializeField] private bool shouldMove;
    [SerializeField] private bool shouldRotate;
    private bool shouldReverse = false;

    [Header("Movement Parameters")]
    [SerializeField] private Vector3 startPos;
    [SerializeField] private Vector3 endPos;

    [Header("Rotation Parameters")]
    [SerializeField] private Quaternion startRot;
    [SerializeField] private Quaternion endRot;

    public float timeToMove;

    public void HandleMovement(bool reverse = false)
    {
        shouldReverse = reverse;

        if (shouldRotate)
            StartCoroutine(MoveAndRotateCamera());
        else
            StartCoroutine(MoveCamera());
    }

    private IEnumerator MoveCamera()
    {
        float timeElapsed = 0;

        while (timeElapsed < timeToMove)
        {
            transform.position = (shouldReverse ? Vector3.Lerp(endPos, startPos, timeElapsed / timeToMove) : Vector3.Lerp(startPos, endPos, timeElapsed / timeToMove));
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = shouldReverse ? startPos : endPos;
    }
    
    private IEnumerator MoveAndRotateCamera()
    {
        float timeElapsed = 0;

        while (timeElapsed < timeToMove)
        {
            transform.position = (shouldReverse ? Vector3.Lerp(endPos, startPos, timeElapsed / timeToMove) : Vector3.Lerp(startPos, endPos, timeElapsed / timeToMove));
            transform.rotation = (shouldReverse ? Quaternion.Lerp(endRot, startRot, timeElapsed / timeToMove) : Quaternion.Lerp(startRot, endRot, timeElapsed / timeToMove));

            timeElapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = shouldReverse ? startPos : endPos;
        transform.rotation = shouldReverse ? startRot : endRot;
    }


}
