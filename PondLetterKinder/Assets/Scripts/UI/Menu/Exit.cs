using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    private void OnMouseDown()
    {
        Debug.Log("Exiting Game");
        Application.Quit();
    }
}
