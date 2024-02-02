using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuizzQuestions", menuName = "Quizz/QuestionsSO")]
public class QuizzQuestionsSO : ScriptableObject
{
    [SerializeField] private List<Question> _questions = new List<Question>();
    public List<Question> Questions => _questions;
}
