using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    public int maxTime;
    public Text timerText;


	// Use this for initialization
	void Start () {
        StartCoroutine("Countdown");
        Time.timeScale = 1;
		
	}
	
	// Update is called once per frame
	void Update () {
        timerText.text = ("" + maxTime);	
	}

    IEnumerator Countdown()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            maxTime--;
        }
    }
}
