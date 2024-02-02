using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Question
{
    [SerializeField] private string _statement;
    public string Statement => _statement;

    [SerializeField] private List<string> _options;
    public List<string> Options => _options;

    [SerializeField] private int _rightAnswer;
    public int RightAnswer => _rightAnswer;
}
