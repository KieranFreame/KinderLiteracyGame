using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellingTurtle : Turtle
{
    public bool IsTurtleToMatch { get; set; }

    protected override void Awake()
    {
        base.Awake();

        _aboveWaterPos = transform.localPosition;
        _belowWaterPos = new Vector3(transform.localPosition.x, -1, transform.localPosition.z);

        if (!isAboveWater)
            transform.localPosition = _belowWaterPos;
    }

    public void Update()
    {
        if (!isAboveWater)
            return;

        if (!IsTurtleToMatch)
            _txtBox.color = Color.green;
    }

    protected override IEnumerator Dive()
    {
        Vector3 currPosition = _aboveWaterPos;
        Vector3 targetPosition = _belowWaterPos;
        float yPos;
        float timeElapsed = 0;

        while (timeElapsed < timeToMove)
        {
            yPos = Mathf.Lerp(currPosition.y, targetPosition.y, timeElapsed / timeToMove);
            transform.localPosition = new Vector3(transform.localPosition.x, yPos, transform.localPosition.z);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        isAboveWater = false;
        transform.localPosition = targetPosition;
    }

    protected override IEnumerator Resurface()
    {
        Vector3 currPosition = _belowWaterPos;
        Vector3 targetPosition = _aboveWaterPos;
        float yPos;
        float timeElapsed = 0;

        while (timeElapsed < timeToMove)
        {
            yPos = Mathf.Lerp(currPosition.y, targetPosition.y, timeElapsed / timeToMove);
            transform.localPosition = new Vector3(transform.localPosition.x, yPos, transform.localPosition.z);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        isAboveWater = true;
        transform.localPosition = targetPosition;
    }
}
