using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerCountdown : MonoBehaviour
{
    public GameObject textDisplay;
    public GameManager_E gameManager_E;
    public int secondsLeft = 60;
    private bool takingAway = false;
    private bool timerStarted = false;
    private bool gameHasFinished = false;
    private List<Transform> carPositions;

    public static TimerCountdown Instance;

    public void Awake()
    {
        Instance = this;
    }

    //Counter
    IEnumerator TimerTake()
    {
        takingAway = true;
        yield return new WaitForSeconds(1);
        secondsLeft -= 1;
        if(secondsLeft < 10)
        {
            textDisplay.GetComponent<Text>().text = "00:0" + secondsLeft;
        } else
        {
            textDisplay.GetComponent<Text>().text = "00:" + secondsLeft;
        }
        takingAway = false;
    }

    //Starts timer 
    public void startTimer()
    {
        timerStarted = true;
    }

    private void setPlaces()
    {
        gameManager_E.secondPlace = gameManager_E.GetPositionByPlayer(gameManager_E.carTransformList[1]);
        gameManager_E.thirdPlace = gameManager_E.GetPositionByPlayer(gameManager_E.carTransformList[2]);
        gameManager_E.fourthPlace = gameManager_E.GetPositionByPlayer(gameManager_E.carTransformList[3]);


    }

    public void Update()
    {
        if(timerStarted == true && takingAway == false && secondsLeft > 0)
        {
            StartCoroutine(TimerTake());
        } else if((secondsLeft == 0 && gameHasFinished == false) || (!gameHasFinished && gameManager_E.totalWinners == 4))
        {
            this.setPlaces();
            textDisplay.GetComponent<Text>().enabled = false;
            gameHasFinished = true;
            KartRacingGame.Instance.finishGame();
        }
    }
}
