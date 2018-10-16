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
    public GameObject hideQA;
    public GameObject timePanel;

    [Header("Script Reference")]
    private QuestionData[] questionPool;
    public AnswerPool answerButtonPool;
    private DataController dataControl;
    public RoundData currentData;

    [Header("Variables")]
    public bool roundOneComplete;
    private bool isRoundActive;
    public int questionIndex;
    public int questionIndexSecondary;
    private float timeRemaining;
    private int playerScore;
    public Transform answerParent;
    private List<GameObject> answerButtonGameObjects = new List<GameObject>();
    


	
	void Start () {

        dataControl = FindObjectOfType<DataController>();
        currentData = dataControl.GetRoundDataPrimary();
        questionPool = currentData.questions;
        timeRemaining = currentData.timeLimitPerQuestion;

        Time.timeScale = 1;

        playerScore = 0;

        questionIndex = 0;

        isRoundActive = true;

        roundOneComplete = false;

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

    public void AnswerButtonClickedPrimary(bool isCorrect)
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
            EndRound();           
        }
    }

    public void EndRound()
    {
        roundOneComplete = true;
        isRoundActive = false;
        RemoveAnswerButton();
        questionDisplayBox.SetActive(false);
        StartCoroutine("delayTimer");
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
            questionDisplayBox.SetActive(true);
        }

        if (timeRemaining <= 0f)
        {
            EndRound();
        }

        if (roundOneComplete)
        {
            currentData = dataControl.GetSecondaryRoundData();
            questionPool = currentData.questions;
        }
        else
        {
            currentData = dataControl.GetRoundDataPrimary();
        }

        if (Input.GetKeyDown("h"))
        {

            hideQA.SetActive(false);
            timePanel.SetActive(false);

        }
        else if (Input.GetKeyUp("h"))
        {
            hideQA.SetActive(true);
            timePanel.SetActive(true);
        }
    }


    IEnumerator delayTimer()
    {   
        //Play Animation Hear
        yield return new WaitForSeconds(10f);
        Debug.Log("waithing finished");
        //play More animation
        isRoundActive = true;
        questionIndex = 0;
        ShowQuestion();
    }
}
