using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    Rigidbody rb;
    bool state;
    bool wait;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        state = true;
        wait = false;
    }

    private void Rising()
    {
        rb.useGravity = false;
        Vector3 position = gameObject.transform.position;

        if (position.y < 5.0f)
        {
            position.y += 0.1f;
            gameObject.transform.position = position;
        } else 
        {
            state = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(wait)
        {
            // Do nothing
        }
        else if(state)
        {
            Rising();
        } else 
        {
            StartCoroutine(Falling());
        } 
    }

    private IEnumerator Falling()
    {
        wait = true;
        yield return new WaitForSeconds(2);
        rb.useGravity = true;
        yield return new WaitForSeconds(4);

        if (gameObject.transform.position.y <= -3.4f)
        {
            wait = false;
            state = true;
        }
    }
}
