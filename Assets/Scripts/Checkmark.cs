using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkmark : MonoBehaviour
{
    public static Action<Checkmark> OnCheckmarkActivated;
    public static Action OnNoCheckmarkSelected;
    [SerializeField] private GameObject _check;
    public GameObject Check => _check;

    public void ToggleCheckActivation()
    {
        _check.SetActive(!_check.activeSelf);

        if (_check.activeSelf)
            OnCheckmarkActivated?.Invoke(this);
        else
            OnNoCheckmarkSelected?.Invoke();
    }
}
