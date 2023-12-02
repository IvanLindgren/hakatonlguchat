using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Message : MonoBehaviour
{
    public TextMeshProUGUI textMessage;
    private void Start()
    {
        TextMeshProUGUI[] textComponents = GetComponentsInChildren<TextMeshProUGUI>();
        if (textComponents.Length > 0)
        {
            textMessage = textComponents[0];
        }
    }
}
