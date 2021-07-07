using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrafficTimer : MonoBehaviour
{
    public float currentTime = 0f;
    public float startingTime = 99f;
    public Text countdownText;

    public TrafficTrouble gameManager;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = startingTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTime > 0)
        {
            currentTime -= 1 * Time.deltaTime;
            countdownText.text = currentTime.ToString("0");

            if (currentTime <= 10)
            {
                countdownText.color = Color.red;
            }
        }
        else if (currentTime <= 0)
        {
            //Include method to end the game when timer hits 0
            gameManager.TimeIsOver();
            Destroy(gameObject);
        }
    }
}
