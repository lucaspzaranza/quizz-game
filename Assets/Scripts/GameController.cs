using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject _rightAnswerMenu;
    [SerializeField] private GameObject _wrongAnswerMenu;
    [SerializeField] private GameObject _endScreen;
    [SerializeField] private GameObject _currentQuestionMenu;
    [SerializeField] private GameObject _questionMenuPrefab;
    [SerializeField] private QuizzQuestionsSO _questionsData;
    [SerializeField] private Transform _inGameTransform;
    [SerializeField] private TextMeshProUGUI _resultTMPRO;

    [SerializeField] private int _currentQuestion;
    public int CurrentQuestion => _currentQuestion;

    [SerializeField] private int _rightAnswersCount;
    public int RightAnswersCount => _rightAnswersCount;

    private int _questionCount;
    private List<string> _optionsLetters = new List<string>()
    {
       "A) ",
       "B) ",
       "C) ",
       "D) "
    };

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
                GetComponentInChildren<QuestionOptionsGroupManager>();
            options.QuestionIndex = questionCount;

            QuestionUITexts questionUI = newQuestionInstance.GetComponent<QuestionUITexts>();
            questionUI.Title.text = question.Statement;
            for (int i = 0; i < question.Options.Count; i++)
            {
                questionUI.Options[i].text = _optionsLetters[i] + question.Options[i];
            }

            if (questionCount > 0)
                newQuestionInstance.SetActive(false);
            questionCount++;
        }
        _currentQuestion = 0;
        _questionCount = questionCount;
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
        if (_currentQuestion < _questionCount)
        {
            _currentQuestionMenu = _inGameTransform.GetChild(_currentQuestion).gameObject;
            _currentQuestionMenu.SetActive(true);
        }
        else
        {
            UpdateEndGameScreenResults();
            _endScreen.SetActive(true);
        }
    }

    public void UpdateEndGameScreenResults()
    {
        _resultTMPRO.text = $"{_rightAnswersCount}";
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    private void OnDisable()
    {
        QuestionOptionsGroupManager.OnAnswerSent -= CheckQuestion;
    }
}
