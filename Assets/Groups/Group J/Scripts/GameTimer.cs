using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    public float timeRemaining = 90;
    public bool timerIsRunning = false;
    public Text timeText;
    public Transform cylinderSlices;
    public List<Transform> allChildren = new List<Transform>();
    private List<bool> allChildrenBoolean = new List<bool>();
    public float maxTimeUntilPlateDestruction = 10;
    private float timerPlate = 0;
    public float timerStartPlateDestruction = 90;
    public bool randomDestructionTimer = true;
    private bool nextRandomDestructionTime = true;
    private GameManagerJ gameplayManager;


    void Awake()
    {
        gameplayManager = GameObject.FindObjectOfType<GameManagerJ>();
    }
    private void Start()
    {

        // Starts the timer automatically
        timerIsRunning = true;

        //Set Timer for plate destruction
        if (maxTimeUntilPlateDestruction > timeRemaining)
        {
            maxTimeUntilPlateDestruction = timeRemaining;
        }
        if (timerStartPlateDestruction > timeRemaining)
        {
            timerStartPlateDestruction = timeRemaining;
        }
        timerPlate = maxTimeUntilPlateDestruction;

        // Get children of cylinderSlices
        {
            foreach (Transform child in cylinderSlices)
            {
                allChildren.Add(child);
                allChildrenBoolean.Add(true);
            }
        }
    }

    void Update()
    {
        if (timerIsRunning)
        {
           if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
                if (timeRemaining < timerStartPlateDestruction)
                {
                    if (timerPlate < 0)
                    {
                        Transform platepart = plateDistruction(0);
                        if (platepart != null)
                        {
                            StartCoroutine(DestroyPlatformComponent(platepart,5));
                        }
                        timerPlate = maxTimeUntilPlateDestruction;
                        nextRandomDestructionTime = true;

                    }
                    else
                    {
                        if (randomDestructionTimer == true && nextRandomDestructionTime == true)
                        {
                            timerPlate = Random.Range(0, maxTimeUntilPlateDestruction);
                            nextRandomDestructionTime = false;
                        }
                        timerPlate -= Time.deltaTime;
                    }
                }
            }
            else
            {
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;
                gameplayManager.gameFinished = true;
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    //This method is using itself. The loop will stop looping after 10 iterations!
    Transform plateDistruction(int count)
    {
        try
        {
            var rdm = Random.Range(0, allChildren.Count);
            int before = 0;
            int after = 0;
            if (rdm != 0 && rdm != 68)
            {
                before = rdm - 1;
                after = rdm + 1;
            }
            else
            {
                if (rdm == 0)
                {
                    before = 68;
                    after = 1;
                }
                else
                {
                    before = 67;
                    after = 0;
                }
            }

            if (allChildrenBoolean[rdm] == true & !(allChildrenBoolean[before] == false & allChildrenBoolean[after] == false))
            {
                Transform child = allChildren[rdm];
                allChildrenBoolean[rdm] = false;
                return child;
            }
            else
            {
                if (count > 10)
                {
                    plateDistruction(count++);
                }
            }

            return null;
        }
        
        catch { return null; }
    }

    IEnumerator DestroyPlatformComponent(Transform component, float seconds)
    {
        Debug.Log("start destroy");
        float elapsedTime = 0;
        while (elapsedTime < seconds) 
        {
            elapsedTime += Time.deltaTime;
            component.gameObject.transform.Rotate(Vector3.right * (5 * Time.deltaTime));
            component.gameObject.transform.position += (Vector3.down* (Time.deltaTime/5));
            Physics.IgnoreLayerCollision(component.gameObject.layer,21,true);
            //Debug.Log("Destroy: "+ Time.deltaTime);
            yield return new WaitForEndOfFrame();
            // component.transform.position -= transform.up * (5 * 1);
        } 
        
        Debug.Log("destroyed component");
        Destroy(component.gameObject);
    }



}
