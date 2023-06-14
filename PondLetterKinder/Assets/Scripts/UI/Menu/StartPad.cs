using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPad : MonoBehaviour
{
    private CameraMovement cam;

    private void Awake()
    {
        cam = FindObjectOfType<CameraMovement>();
    }

    private void OnMouseDown()
    {
        if (GameObject.Find("CreditsUI") != null && GameObject.Find("CreditsUI").activeSelf) { GameObject.Find("CreditsUI").SetActive(false); }
        cam.HandleMovement();
    }
}
