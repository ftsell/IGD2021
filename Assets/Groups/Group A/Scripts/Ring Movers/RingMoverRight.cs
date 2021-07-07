using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingMoverRight : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            RingMoverManager.instance.tr_enter = Time.time;
            Debug.Log("Mover: first touch");
        }
    }

    private void OnTriggerStay(Collider other)
    {

        if (other.tag == "Player")
        {
            if (RingMoverManager.instance.tr_active)
            {
                if (Time.time <= RingMoverManager.instance.tr_enter + RingMoverManager.instance.dur)
                {
                    GenerateRings.instance.RotateRing(2, -RingMoverManager.instance.speed /2);
                    Debug.Log("Mover: turn the table");
                }

                else
                {
                    RingMoverManager.instance.tr_exit = Time.time;
                    RingMoverManager.instance.tr_active = false;
                    this.gameObject.SetActive(false);
                    Debug.Log("Mover: standest zu lang drauf");
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("geh runter top right");
            RingMoverManager.instance.tr_exit = Time.time;
            RingMoverManager.instance.tr_active = false;
            Debug.Log("Mover: Knopf verlassen");
        }

    }




}
