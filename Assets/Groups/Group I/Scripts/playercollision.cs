
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class playercollision : MonoBehaviour
{
    MinifigController player;
    public AudioSource playSoundGravity;
    public AudioSource playSoundSpeed;
    //public AudioSource playSoundHealth;
    //public int currentHealth = 100;

    private bool finished = false;
    private bool isAi = false;

    private void Start()
    {
        player = (MinifigController)this.GetComponent("MinifigController");
        //HealtSystem of Player
        //HealthSystemI healthSystem = new HealthSystemI(100);
        //currentHealth = 100;
    }

    //public void HealthUp()
    //{
    //    HealthSystemI healthSystem = new HealthSystemI(currentHealth);
    //    healthSystem.Heal(10);
    //    currentHealth = healthSystem.GetHealth2();
    //    Debug.Log("Health" + healthSystem.GetHealth2());
    //}

    //public void HealthDown()
    //{
    //    HealthSystemI healthSystem = new HealthSystemI(currentHealth);
    //    healthSystem.Damage(10);
    //    currentHealth = healthSystem.GetHealth2();
    //    Debug.Log("Health" + healthSystem.GetHealth2());
    //}

    void OnTriggerEnter(Collider col)
    {
        GameObject pickup = col.gameObject;

        //if (pickup.tag == "ItemCollect")
        //{
        //    PickUp(pickup);
        //}

        if (pickup.tag == "LowGravity")
        {
            PickUp(pickup);
            player.gravity *= 0.9f;
            playSoundGravity.Play();
        }

        if (pickup.tag == "HighSpeed")
        {
            PickUp(pickup);
            player.maxForwardSpeed += 0.75f;
            playSoundSpeed.Play();
        }

        //if (pickup.tag == "PlusLife")
        //{
        //    PickUp(pickup);
        //    HealthUp();
        //    playSoundHealth.Play();
        //}

        //if (pickup.tag == "Respawn")
        //{
        //    HealthDown();
            
        //}

        if (pickup.tag == "Finish")
        {
            // Do NOT use PickUp() here!!!
            finished = true;
            player.enabled = false;

            Debug.Log(this.name + " finished!");
        }

    }
    public float getZPos()
    {
        return transform.position.z;
    }

    public bool hasFinished()
    {
        return finished;
    }

    public void enableAi()
    {
        isAi = true;

        MinifigController cont = GetComponent<MinifigController>();
        NavMeshScript navScript = GetComponent<NavMeshScript>();
        NavMeshAgent navAgent = GetComponent<NavMeshAgent>();

        cont.enabled = false;
        navScript.enabled = true;
        navAgent.enabled = true;
    }

    private void PickUp(GameObject pickup)
    {
        Debug.Log("Picked up: " + pickup.tag);
        Destroy(pickup);
    }
}
