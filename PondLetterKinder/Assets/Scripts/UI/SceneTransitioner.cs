using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitioner : MonoBehaviour
{
    public static SceneTransitioner inst;
    private readonly List<CurtainMovement> curtains = new(2);

    private void Awake()
    {
        if (inst == null) inst = this;
        else Destroy(gameObject);

        curtains.AddRange(FindObjectsOfType<CurtainMovement>());
    }

    public void HandleChangeScene(string sceneName)
    {
        StartCoroutine(ChangeScene(sceneName));
    }

    IEnumerator ChangeScene(string sceneName)
    {
        for (int i = 0; i < curtains.Count; i++)
            curtains[i].HandleMovement();

        yield return new WaitForSeconds(curtains[0].timeToMove + 2f);

        SceneManager.LoadScene(sceneName);
        SoundManager.StartBackground();

        for (int i = 0; i < curtains.Count; i++)
            curtains[i].HandleMovement();
    }
}
