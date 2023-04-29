// Completely written by Thomas Mercurio

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Quiz : MonoBehaviour
{
    public int curQuestion = 1;
    HashSet<int> a_answers = new HashSet<int>() {1, 3, 4};

    // Keeps track of the number of correct answers so far
    public int correct = 0;

    public TMP_Text option_a;
    public TMP_Text option_b;
    public TMP_Text question;

    Dictionary<int, string> a_options = new Dictionary<int, string> () {
        {1, "Dublin"}, {2, "Spain"}, {3, "Mediterranean"}, {4, "Romania"}};

    Dictionary<int, string> b_options = new Dictionary<int, string> () {
        {1, "Cork"}, {2, "France"}, {3, "Red"}, {4, "Hungary"}};

    Dictionary<int, string> questions = new Dictionary<int, string> () {
        {1, "What is the capital of Ireland?"}, {2, "Which of these countries borders Italy?"},
        {3, "Which sea separates Europe and Africa?"}, {4, "Bucharest is the capital of which country?"}};

    // Check if answer was correct when option A clicked
    public void checkAAnswer() {
        if (a_answers.Contains(curQuestion))
            correctAnswer();
        else
            incorrectAnswer();
    }

    // Check if answer was correct when option B clicked
    public void checkBAnswer() {
        if (!a_answers.Contains(curQuestion))
            correctAnswer();
        else
            incorrectAnswer();
    }

    public void correctAnswer() {
        correct++;
        Debug.Log("Correct");
        incQuestion();
    }

    public void incorrectAnswer() {
        Debug.Log("Incorrect");
        incQuestion();
    }

    public void incQuestion() {
        curQuestion++;
        if (curQuestion == 5) {
            endQuiz();
            return;
        }
        option_a.text = a_options[curQuestion];
        option_b.text = b_options[curQuestion];
        question.text = questions[curQuestion];
    }

    public void endQuiz() {
        MainManager.Instance.numCorrect = correct;
        SceneManager.LoadScene("QuizFinish");
    }
}
