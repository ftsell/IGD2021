using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerUpType { Speed, Shield, Fireball}
public class MainPowerUp : MonoBehaviour
{
    public PowerUpType PowerUpType;
    public GameObject pickUpEffect;


    private void OnTriggerEnter(Collider other)
    {
        other.enabled = false;
        Debug.Log("Coll");
        if (other.tag == ("Player"))
        {
            other.enabled = true;
            Debug.Log("PickUp");
            PickUp(other);

        }
        else
        {
            other.enabled = false;
        }
    }

    void PickUp(Collider player)
    {
        MinifigControllerJ controller = player.GetComponent<MinifigControllerJ>();
        controller.GetComponent<JPlayerStats>().AddPowerUp(PowerUpType);
        Instantiate(pickUpEffect, new Vector3(player.transform.position.x, player.transform.position.y + 1, player.transform.position.z), transform.rotation);
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == ("Obstacle"))
        {
            Physics.IgnoreLayerCollision(gameObject.layer, 21, true);
        }


    }

    IEnumerator ActivateInvincibility()
    {
        Physics.IgnoreLayerCollision(gameObject.layer, 21, true);


        yield return new WaitForSeconds(3);

        Physics.IgnoreLayerCollision(gameObject.layer, 21, false);
    }
}
