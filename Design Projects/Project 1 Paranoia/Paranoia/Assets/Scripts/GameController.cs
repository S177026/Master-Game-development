using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    [Header("Text Variables")]
    public Text displayedQuestion;
    public Text timeRemainingDisplay;

    [Header("UI Elements")]
    public Slider progressSlider;

    [Header("GameObject")]
    public GameObject questionDisplayBox;
    public GameObject endScreen;

    [Header("Script Reference")]
    private QuestionData[] questionPool;
    public AnswerPool answerButtonPool;
    private DataController dataControl;
    private RoundData currentData;

    [Header("Variables")]
    private bool isRoundActive;
    private int questionIndex;
    private float timeRemaining;
    private int playerScore;
    public Transform answerParent;
    private List<GameObject> answerButtonGameObjects = new List<GameObject>();


	
	void Start () {

        dataControl = FindObjectOfType<DataController>();
        currentData = dataControl.GetRoundData();
        questionPool = currentData.questions;
        timeRemaining = currentData.timeLimitPerQuestion;

        Time.timeScale = 1;

        playerScore = 0;
        questionIndex = 0;

        isRoundActive = true;

        ShowQuestion();

    }
	
    private void ShowQuestion()
    {

        RemoveAnswerButton();
        QuestionData questionData = questionPool [questionIndex];
        displayedQuestion.text = questionData.questionText;

        for (int i = 0; i < questionData.answers.Length; i++)
        {
            GameObject answerButtonGameObject = answerButtonPool.GetObject();
            answerButtonGameObjects.Add(answerButtonGameObject);
            answerButtonGameObject.transform.SetParent(answerParent);

            AnswerButton answerButton = answerButtonGameObject.GetComponent<AnswerButton>();
            answerButton.AnswerSetup(questionData.answers[i]);
        }

    }

    private void RemoveAnswerButton()
    {
        while (answerButtonGameObjects.Count > 0)
        {

            answerButtonPool.ReturnObject(answerButtonGameObjects[0]);
            answerButtonGameObjects.RemoveAt(0);

        }
    }


    public void AnswerButtonClicked(bool isCorrect)
    {
        if (isCorrect)
        {
            timeRemaining = currentData.timeLimitPerQuestion;
            playerScore += currentData.pointsAddedForAnswer;          
            progressSlider.value += 1f;

        }
        else if (!isCorrect)
        {
            playerScore -= currentData.pointsTakenForAnnswer;
            progressSlider.value -= 1f;
        }

        if(questionPool.Length > questionIndex + 1)
        {
            questionIndex++;
            ShowQuestion();
        }
        else
        {
            //EndRound();
        }

    }
    public void EndRound()
    {
        isRoundActive = false;

        questionDisplayBox.SetActive(false);     

    }

    private void UpdateRemainingTime()
    {

        timeRemainingDisplay.text = "Time: " + Mathf.Round(timeRemaining).ToString();

    }



    // Update is called once per frame
    void Update () {
        if (isRoundActive)
        {
            timeRemaining -= Time.deltaTime;
            UpdateRemainingTime();
        }

        if (timeRemaining <= 0f)
        {
            EndRound();
        }      
    }
}
