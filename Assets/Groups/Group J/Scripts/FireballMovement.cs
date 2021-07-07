using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballMovement : MonoBehaviour
{
    public float speed;
    public float fireRate;

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position += transform.forward * (speed * Time.deltaTime);
    }

}
