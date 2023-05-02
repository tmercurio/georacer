// Completely written by Thomas Mercurio for the quiz at the end of the game

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

    Dictionary<int, string> a1_options = new Dictionary<int, string> () {
        {1, "Dublin"}, {2, "Spain"}, {3, "Mediterranean"}, {4, "Hungary"}};

    Dictionary<int, string> b1_options = new Dictionary<int, string> () {
        {1, "Cork"}, {2, "France"}, {3, "Red"}, {4, "Romania"}};

    Dictionary<int, string> questions1 = new Dictionary<int, string> () {
        {1, "What is the capital of Ireland?"}, {2, "Which of these countries borders Italy?"},
        {3, "Which sea separates Europe and Africa?"}, {4, "Budapest is the capital of which country?"}};


    Dictionary<int, string> a2_options = new Dictionary<int, string> () {
        {1, "Alberta"}, {2, "Kinshasa"}, {3, "Bolivia"}, {4, "Java"}};

    Dictionary<int, string> b2_options = new Dictionary<int, string> () {
        {1, "Ontario"}, {2, "Windhoek"}, {3, "Paraguay"}, {4, "Honshu"}};

    Dictionary<int, string> questions2 = new Dictionary<int, string> () {
        {1, "Which of these Canadian provinces is farther west?"}, {2, "What is the capital of Namibia?"},
        {3, "Which country borders Peru?"}, {4, "What is the most populous island in the world?"}};


    public void Start() {
        if (MainManager.Instance.level == 2) {
            option_a.text = a2_options[curQuestion];
            option_b.text = b2_options[curQuestion];
            question.text = questions2[curQuestion];
        }
    }

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

        if (MainManager.Instance.level == 1) {
            option_a.text = a1_options[curQuestion];
            option_b.text = b1_options[curQuestion];
            question.text = questions1[curQuestion];
        }
        else {
            option_a.text = a2_options[curQuestion];
            option_b.text = b2_options[curQuestion];
            question.text = questions2[curQuestion];
        }

        //question.text = l1Questions["question"][curQuestion];
        //option_a.text = l1Questions["a"][curQuestion];
        //option_b.text = l1Questions["b"][curQuestion];
    }

    public void endQuiz() {
        MainManager.Instance.numCorrect = correct;
        SceneManager.LoadScene("QuizFinish");
    }
}
