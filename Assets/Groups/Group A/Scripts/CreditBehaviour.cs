using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditBehaviour : MonoBehaviour
{
    
    Rigidbody rigidbody;
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "MiddlePlattform")
        {
            rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        }
        if(collision.gameObject.tag == "Respawn")
        {
            Destroy(this.gameObject);
        }
    }
}
