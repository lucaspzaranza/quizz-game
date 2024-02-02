using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject _rightAnswerMenu;
    [SerializeField] private GameObject _wrongAnswerMenu;
    [SerializeField] private GameObject _currentQuestionMenu;
    [SerializeField] private GameObject _questionMenuPrefab;
    [SerializeField] private QuizzQuestionsSO _questionsData;
    [SerializeField] private Transform _inGameTransform;

    [SerializeField] private int _currentQuestion;
    public int CurrentQuestion => _currentQuestion;

    [SerializeField] private int _rightAnswersCount;
    public int RightAnswersCount => _rightAnswersCount;

    private void OnEnable()
    {
        QuestionOptionsGroupManager.OnAnswerSent += CheckQuestion;
    }

    private void Start()
    {
        InstantiateQuestions();
    }

    public void InstantiateQuestions()
    {
        int questionCount = 0;
        foreach(var question in _questionsData.Questions)
        {
            GameObject newQuestionInstance = Instantiate(_questionMenuPrefab, _inGameTransform);
            if(questionCount == 0)
                _currentQuestionMenu = newQuestionInstance;

            newQuestionInstance.name = $"Question {questionCount + 1}";

            var options = newQuestionInstance.transform.
                GetChild(1).GetComponent<QuestionOptionsGroupManager>();
            options.QuestionIndex = questionCount;

            QuestionUITexts questionUI = newQuestionInstance.GetComponent<QuestionUITexts>();
            questionUI.Title.text = question.Statement;
            for (int i = 0; i < question.Options.Count; i++)
            {
                questionUI.Options[i].text = question.Options[i];
            }

            if (questionCount > 0)
                newQuestionInstance.SetActive(false);
            questionCount++;
        }
        _currentQuestion = 0;
    }

    public void CheckQuestion(int questionIndex, QuestionOptionsGroupManager questionGroup)
    {
        _currentQuestionMenu.SetActive(false);

        if(questionGroup.SelectedOption == _questionsData.Questions[questionIndex].RightAnswer)
        {
            _rightAnswerMenu.SetActive(true);
            _rightAnswersCount++;
        }
        else
            _wrongAnswerMenu.SetActive(true);
    }

    public void GetNextQuestion()
    {
        _currentQuestion++;
        _currentQuestionMenu = _inGameTransform.GetChild(_currentQuestion).gameObject;
        _currentQuestionMenu.SetActive(true);
    }

    private void OnDisable()
    {
        QuestionOptionsGroupManager.OnAnswerSent -= CheckQuestion;
    }
}
