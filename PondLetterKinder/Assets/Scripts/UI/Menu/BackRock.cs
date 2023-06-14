using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackRock : MonoBehaviour
{
    private CameraMovement cam;

    private void Awake()
    {
        cam = FindObjectOfType<CameraMovement>();
    }

    private void OnMouseDown()
    {
        cam.HandleMovement(true);
    }
}
