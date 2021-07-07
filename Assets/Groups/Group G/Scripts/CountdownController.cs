using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownController : MonoBehaviour
{
    public int CountdownTime = 3;
    public Text CountdownDisplay;
    public Canvas ControlsCanvas;

    private GameController_G GameController;
    void Start()
    {
        if (this.GetComponent<GameController_G>() != null) {
            GameController = this.GetComponent<GameController_G>();
        } else return;

        //GameController.SetPlayerControllersActive(false);
        StartCoroutine(CountdownToStart());
    }

    IEnumerator CountdownToStart()
    { 
        while(CountdownTime > 0)
        {
            CountdownDisplay.text = CountdownTime.ToString();
            yield return new WaitForSeconds(1f);
            CountdownTime--;

        }

        CountdownDisplay.text = "GO!";
        GameController.BeginGame();
        yield return new WaitForSeconds(1.5f);
        CountdownDisplay.gameObject.SetActive(false);
        ControlsCanvas.gameObject.SetActive(false);

    }
}
