using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {


    public Text displayedQuestion;
    public Text timeForQuestion;

    public Slider progressSlider;

    public GameObject questionDisplayBox;
    public GameObject endScreen;


    public QuestionData[] questionPool;

    private int questionIndex;
    private float timeRemaining;


	
	void Start () {

        ShowQuestion();

    }
	
    private void ShowQuestion()
    {

        QuestionData questionData = questionPool [questionIndex];
        displayedQuestion.text = questionData.questionText;

        for (int i = 0; i < questionData.answers.Length; i++)
        {



        }

    }


    public void AnswerButtonClicked(bool isCorrect)
    {
        if (isCorrect)
        {
            progressSlider.value += 1f;
        }

        if(questionPool.Length > questionIndex + 1)
        {
            questionIndex++;
            ShowQuestion();
        }
        else
        {
            EndScreen();
        }

    }

    public void EndScreen()
    {

        questionDisplayBox.SetActive(false);
        endScreen.SetActive(true);

    }




	// Update is called once per frame
	void Update () {
		
	}
}
