using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    [SerializeField] private GameObject _creditsUI;

    private void OnMouseDown()
    {
        _creditsUI.SetActive(true);
    }
}
