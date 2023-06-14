using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuFish : MonoBehaviour
{
    [SerializeField] private string sceneName;

    [SerializeField] private bool moveLeft;
    [SerializeField] private float timeToMove;

    [SerializeField] private MenuFish otherFish;
    [SerializeField] private MenuTurtle menuTurtle;

    public bool canMove = true;

    private void OnMouseDown()
    {
        if (canMove)
        {
            transform.Find("Text (TMP)").gameObject.SetActive(false);
            otherFish.canMove = false;
            menuTurtle.canMove = false;
            StartCoroutine(MoveFish());
        }
    }

    private IEnumerator MoveFish()
    {
        Vector3 startPos = transform.position;
        Vector3 targetPos = (moveLeft ?
            new Vector3(startPos.x, startPos.y, -120) : new Vector3(startPos.x, startPos.y, -55));

        float timeElapsed = 0;

        while (timeElapsed < timeToMove)
        {
            transform.position = Vector3.Lerp(startPos, targetPos, timeElapsed / timeToMove);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPos;

        if (sceneName != string.Empty)
            SceneTransitioner.inst.HandleChangeScene(sceneName);
        else
            Debug.Log("Changing Scenes!");
    }
}
