using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Turtle : MonoBehaviour
{
    public string Letter { get; set; } = string.Empty;
    protected TextMesh _txtBox;

    [Header("Movement Parameters")]
    public bool isAboveWater = false;
    private bool isMoving = false;
    [SerializeField] protected float timeToMove = 0.5f;
    [SerializeField] protected Vector3 _aboveWaterPos;
    [SerializeField] protected Vector3 _belowWaterPos;

    protected virtual void Awake()
    {
        if (this is MenuTurtle)
            return;

        _txtBox = GetComponentInChildren<TextMesh>();
        _txtBox.text = string.Empty;
    }

    public void HandleDive()
    {
        if (!isAboveWater || isMoving) { return; }

        _txtBox.text = string.Empty;
        StartCoroutine(Dive());
    }

    public void HandleResurface()
    {
        if (isAboveWater || isMoving) { return; }

        _txtBox.text = Letter.ToUpper();
        StartCoroutine(Resurface());
    }

    protected virtual IEnumerator Dive()
    {
        Vector3 currPosition = _aboveWaterPos;
        Vector3 targetPosition = _belowWaterPos;
        float yPos;
        float timeElapsed = 0;
        isMoving = true;

        while (timeElapsed < timeToMove)
        {
            yPos = Mathf.Lerp(currPosition.y, targetPosition.y, timeElapsed / timeToMove);
            transform.position = new Vector3(transform.position.x, yPos, transform.position.z);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        isAboveWater = false;
        isMoving = false;
    }

    protected virtual IEnumerator Resurface()
    {
        Vector3 currPosition = _belowWaterPos;
        Vector3 targetPosition = _aboveWaterPos;
        float yPos;
        float timeElapsed = 0;
        isMoving = true;

        while (timeElapsed < timeToMove)
        {
            yPos = Mathf.Lerp(currPosition.y, targetPosition.y, timeElapsed / timeToMove);
            transform.position = new Vector3(transform.position.x, yPos, transform.position.z);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        isAboveWater = true;
        isMoving = false;
    }
}
