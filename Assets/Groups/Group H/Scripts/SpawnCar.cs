using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCar : MonoBehaviour
{
    public List<GameObject> cars;
    public int waitingSeconds = 2;
    public float gameTimer;

    // Start is called before the first frame update
    void Start()
    {
        gameTimer=Time.time+50.0f;
        StartCoroutine(carSpawn());
    }

    IEnumerator carSpawn()
    {
        while (true)
        {
            if (gameTimer > Time.time)
            {
                int xPos = 0;
                int zPos = 0;
                int lane = Random.Range(-2, 3) * 2;
                int direction = Random.Range(0, 3);
                int rotation = direction * 90;
                if (direction % 2 == 0)
                {
                    xPos = lane;
                }
                else
                {
                    zPos = lane;
                }
                int carnumber = Random.Range(0, cars.Count - 1);
                Quaternion target = Quaternion.Euler(0, rotation, 0);
                GameObject newCar = Instantiate(cars[carnumber], new Vector3(xPos, 0, zPos), target);
                Destroy(newCar, 3);
                yield return new WaitForSeconds(waitingSeconds);
            }
            else
            {
                int xPos1 = 0;
                int zPos1 = 0;
                int xPos2 = 0;
                int zPos2 = 0;
                int lane1 = Random.Range(0, 3) * 2;
                int lane2 = Random.Range(-2, 0) * 2;
                int direction = Random.Range(0, 3);
                int rotation = direction * 90;
                if (direction % 2 == 0)
                {
                    xPos1 = lane1;
                    xPos2 = lane2;
                }
                else
                {
                    zPos1 = lane1;
                    zPos2 = lane2;
                }
                int carnumber1 = Random.Range(0, cars.Count - 1);
                int carnumber2 = Random.Range(0, cars.Count - 1);
                Quaternion target = Quaternion.Euler(0, rotation, 0);
                GameObject newCar1 = Instantiate(cars[carnumber1], new Vector3(xPos1, 0, zPos1), target);
                GameObject newCar2 = Instantiate(cars[carnumber2], new Vector3(xPos2, 0, zPos2), target);
                Destroy(newCar1, 3);
                Destroy(newCar2, 3);
                yield return new WaitForSeconds(waitingSeconds);
            }
        }
    }
}
