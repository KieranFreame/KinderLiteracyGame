using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Lilypad : MonoBehaviour
{
    private TextMesh _txtBox;
    [SerializeField] private float travelTime;

    private void Awake()
    {
        _txtBox = GetComponentInChildren<TextMesh>();
    }

    private void OnEnable()
    {
        if (SpellingGameManager.inst == null)
            return;

        _txtBox.text = SpellingGameManager.inst.AssignLetter();
        StartCoroutine(MoveToScreenEdge());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private void OnMouseDown()
    {
        SpellingGameManager.inst.CompareLetter(_txtBox.text);
        Pooler.Despawn(gameObject);
    }

    private IEnumerator MoveToScreenEdge()
    {
        Vector3 startPos = transform.position;
        Vector3 targetPos = new (-startPos.x, startPos.y, startPos.z);
        float timeElapsed = 0;
        float posX;

        while (timeElapsed < travelTime)
        {
            posX = Mathf.Lerp(startPos.x, targetPos.x, timeElapsed / travelTime);
            transform.localPosition = new Vector3(posX, transform.position.y, transform.position.z);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        Pooler.Despawn(gameObject);
    }
}
