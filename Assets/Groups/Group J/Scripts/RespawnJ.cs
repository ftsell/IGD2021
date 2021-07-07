using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnJ : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] Items;
    public Vector3 spawnValue;
    public float spwanWait;
    public float spawnMaxWait;
    public float spawnMinWait;
    public int startWait;
    public bool stop;
    int rndItem;
    public float timeRemaining = 90;
    private bool timerIsRunning = false;

    private void Start()
    {

        // Starts the timer automatically
        timerIsRunning = true;
        StartCoroutine(WaitSpawner());
    }
    List<int> randomSpwanTime = new List<int>();
    // Update is called once per frame
    void Update()
    {
        spwanWait = Random.Range(spawnMinWait, spawnMaxWait);
    }
    IEnumerator WaitSpawner()
    {
        yield return new WaitForSeconds(startWait);
        while (!stop)
        {
            rndItem = Random.Range(0, Items.Length);
            Vector3 spawnPosition = new Vector3(Random.Range(-spawnValue.x, spawnValue.x),1, Random.Range(-spawnValue.z,spawnValue.z));
            Instantiate(Items[rndItem], spawnPosition+ transform.TransformPoint(0,0,0),gameObject.transform.rotation);

            yield return new WaitForSeconds(spwanWait);
        }
    }
}
