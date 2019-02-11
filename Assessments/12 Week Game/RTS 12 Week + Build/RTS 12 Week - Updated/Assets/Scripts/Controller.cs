using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{
    public int timeLeft = 10;
    public Text countdown;

    public bool isGameOver;
    public GameObject gameOver;

    public Button restartButton;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("LoseTime");
        Time.timeScale = 1;
        gameOver.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        countdown.text = ("Time Left: " + timeLeft);

        if(timeLeft <= 0)
        {           
            isGameOver = true;
            GameOver();
        }
    }
    public void GameOver()
    {
        if (isGameOver)
        {
            StopCoroutine("LoseTime");
            gameOver.gameObject.SetActive(true);
        }
    }

    public void Retry()
    {
        SceneManager.LoadScene("Prototype");
    }

    IEnumerator LoseTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            timeLeft--;
        }

    }
}
