using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NameInputHandler : MonoBehaviour
{
    private TMP_InputField nameInputField;

    private void Awake()
    {
        nameInputField = GetComponentInChildren<TMP_InputField>();
    }

    public void SetPlayerName()
    {
        if (string.IsNullOrEmpty(nameInputField.text) || nameInputField == null)
        {
            Debug.Log("Can't Set Name");
            return;
        }

        SpellingGameManager.inst.SetName(nameInputField.text);

        transform.parent.gameObject.SetActive(false);
    }
}
