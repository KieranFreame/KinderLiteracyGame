using System.Collections;
using UnityEngine;

public class MenuTurtle : Turtle
{
    [Header("MenuTurtle Parameters")]
    [SerializeField] private MenuFish spellFish;
    [SerializeField] private MenuFish letterFish;
    [SerializeField] private string sceneName;
    public bool canMove = true;

    // Start is called before the first frame update
    void Start()
    {
        isAboveWater = true;   
    }

    private void OnMouseDown()
    {
        if (!canMove) return;

        transform.Find("Text (TMP)").gameObject.SetActive(false);
        spellFish.canMove = false;
        letterFish.canMove = false;

        StartCoroutine(ChangeScene());
    }

    private IEnumerator ChangeScene()
    {
        yield return Dive();

        if (!string.IsNullOrEmpty(sceneName))
            SceneTransitioner.inst.HandleChangeScene(sceneName);
    }
}
