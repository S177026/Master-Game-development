﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerButton : MonoBehaviour {


    public Text answerText;

    private AnswerData answerData;
    private GameController gameController;

	// Use this for initialization
	void Start ()
    {

        gameController = FindObjectOfType<GameController>();

	}

    public void AnswerSetup (AnswerData data)
    {

        answerData = data;
        answerText.text = answerData.answerText;

    }

    public void HandleClickAnswer()
    {
        gameController.AnswerButtonClicked(answerData.isCorrect);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
