using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestionUITexts : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _title;
    public TextMeshProUGUI Title => _title;

    [SerializeField] private List<TextMeshProUGUI> _options;
    public List<TextMeshProUGUI> Options => _options;
}
