using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupPowerUp : MonoBehaviour
{
    public BasePowerUp powerUp;
    public string Identifier;
    public float secsToDespawn = 15f;
    private void Awake()
    {
        switch (Identifier)
        {
            case "Bridge":
                powerUp = new BridgePowerUp();
                break;
            case "Trampoline":
                powerUp = new TrampolinePowerUp();
                break;
            case "BaseballBat":
                powerUp = new BatPowerUp();
                break;
        }
        
    }

    private void Start()
    {
        Invoke("Despawn", secsToDespawn);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Respawn")
        {
            Destroy(this.gameObject);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            MinifigControllerWTH controller = other.gameObject.GetComponent<MinifigControllerWTH>();
            controller.AddPowerUp(powerUp);
            Destroy(this.gameObject);
        }
    }

    private void Despawn()
    {
        Destroy(this.gameObject);
    }

}
