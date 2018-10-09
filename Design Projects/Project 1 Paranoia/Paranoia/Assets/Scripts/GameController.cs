using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    [Header("Text Variables")]
    public Text displayedQuestion;
    public Text timeForQuestion;
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
    private int questionIndex;
    private int timeRemaining;
    private int playerScore;
    public Transform answerParent;
    private List<GameObject> answerButtonGameObjects = new List<GameObject>();


	
	void Start () {

        dataControl = FindObjectOfType<DataController>();
        currentData = dataControl.GetRoundData();
        questionPool = currentData.questions;
        timeRemaining = currentData.timeLimitPerQuestion;

        StartCoroutine("Countdown");
        Time.timeScale = 1;

        playerScore = 0;
        questionIndex = 0;

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
            //EndScreen();
        }

    }
    public void EndScreen()
    {

        questionDisplayBox.SetActive(false);
        endScreen.SetActive(true);

    }

    IEnumerator Countdown()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            timeRemaining--;
        }
    }

    // Update is called once per frame
    void Update () {
        timeRemainingDisplay.text = ("Time: " + timeRemaining);

    }
}
