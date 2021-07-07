using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StartCountdown : MonoBehaviour
{

    public GameObject countDown;

   public void StartToCount()
    {
        Debug.Log("start to count");
        countDown.SetActive(true);
        StartCoroutine(CountDownRoutine());
    }

    IEnumerator CountDownRoutine()
    {
        yield return new WaitForSeconds(0.5f);
        countDown.GetComponent<Text>().text = "3";
        countDown.SetActive(true);

        yield return new WaitForSeconds(1f);
        countDown.SetActive(false);
        countDown.GetComponent<Text>().text = "2";
        countDown.SetActive(true);


        yield return new WaitForSeconds(1f);
        countDown.SetActive(false);
        countDown.GetComponent<Text>().text = "1";
        countDown.SetActive(true);

        yield return new WaitForSeconds(1f);
        countDown.SetActive(false);
        countDown.GetComponent<Text>().text = "";
        countDown.SetActive(true);

        GameObject.Find("Kart WASD").GetComponent<CarController>().EnableControl();
        GameObject.Find("Kart ZGHJ").GetComponent<CarController>().EnableControl();
        GameObject.Find("Kart PLÖA").GetComponent<CarController>().EnableControl();
        GameObject.Find("Kart Num").GetComponent<CarController>().EnableControl();
        GameManager_E.Instance.CreateAI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
