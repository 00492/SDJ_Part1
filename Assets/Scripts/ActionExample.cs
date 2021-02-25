using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ActionExample : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _firstText;
    [SerializeField] private TextMeshProUGUI _secondText;

    private Action _autoChangeText;
    private Action<string> _manualChangeText;


    void Start()
    {
        _autoChangeText += ChangeFirstOne;
        _autoChangeText += ChangeSecondOne;

        _manualChangeText += ChangeTextWithNewString;
    }

    private void ChangeFirstOne()
    {
        _firstText.SetText("This is a new Text");
    }

    private void ChangeSecondOne()
    {
        _secondText.SetText("This is a new Text");
    }

    public void ChangeTextWithNewString(string newText)
    {
        _firstText.SetText(newText);
        _secondText.SetText(newText);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            _autoChangeText?.Invoke();
        }
        else if(Input.GetKeyDown(KeyCode.M))
        {
            _manualChangeText?.Invoke("Manually changed both");
        }


    }

    private void OnDestroy()
    {
        _autoChangeText -= ChangeFirstOne;
        _autoChangeText -= ChangeSecondOne;

        _manualChangeText -= ChangeTextWithNewString;
    }
}
