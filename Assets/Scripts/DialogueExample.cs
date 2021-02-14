using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueExample : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _tmproQuestion;
    [SerializeField] private TextMeshProUGUI _tmproAnswer;
    [SerializeField] private DialogueData _data;

    public int _questionIndex;
    public int _answerIndex;


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.D))
        {
            _tmproQuestion.SetText(_data._questions[_questionIndex]._text);
            _tmproAnswer.SetText(_data._questions[_questionIndex]._answer[_answerIndex]._text);
        }
    }
}
