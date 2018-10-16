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
    public GameObject hideQA;
    public GameObject timePanel;
    public GameObject gameOverSprite;
    public GameObject introText;

    [Header("Script Reference")]
    private QuestionData[] questionPool;
    public AnswerPool answerButtonPool;
    private DataController dataControl;
    public RoundData currentData;

    [Header("Variables")]
    public bool introPlaying;
    public bool roundOneComplete;
    private bool isRoundActive;
    public int questionIndex;
    public int questionIndexSecondary;
    private float timeRemaining;
    private int playerScore;
    public Transform answerParent;
    private List<GameObject> answerButtonGameObjects = new List<GameObject>();

    [Header("Question Variables")]
    public bool optionOneIsCorrect;
    public bool optionTwoIsCorrect;

    public Animator anim;

	void Start () {

        dataControl = FindObjectOfType<DataController>();
        currentData = dataControl.GetRoundDataPrimary();
        questionPool = currentData.questions;
        timeRemaining = currentData.timeLimitPerQuestion;


        Time.timeScale = 1;

        introPlaying = true;

        playerScore = 0;

        questionIndex = 0;

        isRoundActive = true;

        roundOneComplete = false;

        introText.SetActive(true);

        StartCoroutine("StartIntro");      
    }
	
    private void ShowQuestion()
    {
        introPlaying = false;
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
            timeRemaining = currentData.timeLimitPerQuestion;
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

        if (introPlaying)
        {
            timeRemaining = 15f;
        }

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
            timeRemaining = 0f;
            GameOver();
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

    public void GameOver()
    {
        gameOverSprite.SetActive(true);
    }

    IEnumerator StartIntro()
    {
        yield return new WaitForSeconds(10f);
        introText.SetActive(false);
        ShowQuestion();
    }

    IEnumerator delayTimer()
    {
        anim.SetBool("ShowPhoto", true);
        yield return new WaitForSeconds(2f);
        Debug.Log("waithing finished");
        //play More animation
        isRoundActive = true;
        questionIndex = 0;
        ShowQuestion();
    }
}
