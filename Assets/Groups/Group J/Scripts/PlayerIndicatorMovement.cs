using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIndicatorMovement : MonoBehaviour
{
    float maxTopStep = 0.5f;
    float startPoint = 0;
    bool moveUp = true;
    private void Start()
    {
        startPoint = gameObject.transform.position.y;
    }
    void Update()
    {
        if(gameObject.transform.position.y <= startPoint+maxTopStep && moveUp)
        {
            gameObject.transform.position += transform.up * (Time.deltaTime/2);
        }
        else if(gameObject.transform.position.y >= startPoint)
        {
            moveUp = false;
            gameObject.transform.position -= transform.up * (Time.deltaTime/2);
        }
        else
        {
            moveUp = true;
        }
        
    }
}
