using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemImpact : MonoBehaviour
{
    private bool collided;
    public GameObject explosionEffect;

    public void OnTriggerStay(Collider other)
    {
        Debug.Log("collison fireball");
        if (other.gameObject.tag == "Player")
        {
 
            Debug.Log("Hit target");
            other.gameObject.GetComponent<AIController>().AddImpact(this.transform.forward * 45);
            collided = true;
            Instantiate(explosionEffect, new Vector3(other.transform.position.x, other.transform.position.y + 1, other.transform.position.z), transform.rotation);
            Destroy(gameObject);
        }
    }
    }
