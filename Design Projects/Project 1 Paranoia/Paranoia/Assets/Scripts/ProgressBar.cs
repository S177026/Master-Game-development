using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour {

    public Slider progressSlider;
    public bool isQuestionCorrect;

    public Button yesButton;
    public Button noButton;

	// Use this for initialization
	void Start () {

        yesButton.onClick.AddListener(ClickedYes);
        noButton.onClick.AddListener(ClickedNo);
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void ClickedYes()
    {
        isQuestionCorrect = true;

        if (isQuestionCorrect)
        {
            progressSlider.value += 1f;
        }      
    }

    void ClickedNo()
    {
        isQuestionCorrect = false;

        if (!isQuestionCorrect)
        {
            progressSlider.value -= 1f;
        }       
    }
}
