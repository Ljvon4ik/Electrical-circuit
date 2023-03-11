using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIInput : MonoBehaviour
{
    [SerializeField]
    TMP_InputField inputField;
    [SerializeField]
    Button _checkAnswerButton;
    int _responseCharacterLimit = 4;
    string _correctAnswer;
    UIInfo _uiInfo;
    string _correctAnswerText = "Great! Correct answer!";
    string _incorrectAnswerText = "Oh no";

    private void Start()
    {
        _checkAnswerButton.onClick.AddListener(CheckAnswer);
        inputField.characterLimit = _responseCharacterLimit;
        _uiInfo = GetComponent<UIInfo>();

        //Temporary solution due to a study project
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            _correctAnswer = "8";
        }
    }

    void CheckAnswer()
    {
        if (inputField.text == _correctAnswer)
            CorrectAnswer();
        else
            IncorrectAnswer();
    }

    void CorrectAnswer()
    {
        _uiInfo.DisplayInfo(_correctAnswerText);
    }

    void IncorrectAnswer()
    {
        _uiInfo.DisplayInfo(_incorrectAnswerText);
    }
}
