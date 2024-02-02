using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionOptionsGroupManager : MonoBehaviour
{
    public static Action<int, QuestionOptionsGroupManager> OnAnswerSent;

    [SerializeField] private int _questionIndex;
    public int QuestionIndex
    {
        get => _questionIndex;
        set => _questionIndex = value;
    }

    [SerializeField] private List<Checkmark> _checkmarks;

    [SerializeField] private int _selectedOption = -1;
    public int SelectedOption
    {
        get => _selectedOption;
        set
        {
            _selectedOption = value;
        }
    }

    private void OnEnable()
    {
        Checkmark.OnCheckmarkActivated += HandleOnCheckActivated; 
        Checkmark.OnNoCheckmarkSelected += OnNoCheckmarkSelected;
    }

    private void HandleOnCheckActivated(Checkmark currentCheckmark)
    {
        foreach(var checkmark in _checkmarks)
        {
            if(!checkmark.Equals(currentCheckmark))
                checkmark.Check.SetActive(false);
        }

        _selectedOption = _checkmarks.IndexOf(currentCheckmark);
    }

    private void OnNoCheckmarkSelected()
    {
        _selectedOption = -1;
    }

    public void SendAnswer()
    {
        OnAnswerSent?.Invoke(QuestionIndex, this);
    }

    private void OnDisable()
    {
        Checkmark.OnCheckmarkActivated -= HandleOnCheckActivated;
        Checkmark.OnNoCheckmarkSelected -= OnNoCheckmarkSelected;
    }
}
