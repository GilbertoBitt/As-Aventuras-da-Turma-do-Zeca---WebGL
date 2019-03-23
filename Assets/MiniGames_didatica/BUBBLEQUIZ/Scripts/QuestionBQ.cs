using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "others/QuestionBQ")]
public class QuestionBQ : SerializedScriptableObject {

    [TextArea(1,5)]
    public string question;
    [ListDrawerSettings(ShowIndexLabels = true)]
    public AnswerBQ[] answers = new AnswerBQ[4];

    public void ShuffleAnswers() {
        answers.Suffle();
    }

    public void OnValidate() {
        bool hasCorrectMarked = HasCorrectValue();
        if (hasCorrectMarked) {
            HideCorrectProperty();
        } else {
            ShowCorrectProperty();
        }
    }

    public bool HasCorrectValue() {
        int tempCount = answers.Length;
        for (int i = 0; i < tempCount; i++) {
            if (answers[i].isCorrect) {
                return true;
            }
        }
        return false;
    }

    public void HideCorrectProperty() {
        int tempCount = answers.Length;
        for (int i = 0; i < tempCount; i++) {
            if (!answers[i].isCorrect) {
                answers[i].showBoolProperty = false;
            }
        }
    }

    public void ShowCorrectProperty() {
        int tempCount = answers.Length;
        for (int i = 0; i < tempCount; i++) {
            answers[i].showBoolProperty = true;
        }
    }

}
