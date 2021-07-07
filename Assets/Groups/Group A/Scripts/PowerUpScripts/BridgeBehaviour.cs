using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeBehaviour : MonoBehaviour
{
    public float secsToDespawn = 15f;
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
    private void Despawn()
    {
        Destroy(this.gameObject);
    }
}
