using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootFireball : MonoBehaviour
{

    public GameObject projectile;
    public Transform firePoint;
    public GameObject player;

    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.Space) && player.gameObject.GetComponent<JPlayerStats>().fireballCount>0)
        //{
        //    Shoot();
        //}    
   
    }

    public void Shoot()
    {
        if (player.gameObject.GetComponent<JPlayerStats>().fireballCount > 0)
        {
            player.gameObject.GetComponent<JPlayerStats>().fireballCount--;
            var fireball = CreateProjectile();
            fireball.transform.localRotation = player.transform.rotation;
            Debug.Log("Used Fireball. " + player.gameObject.GetComponent<JPlayerStats>().fireballCount + " remaining.");
        }
    }
     GameObject CreateProjectile()
    {
        var projectileObj = Instantiate(projectile, firePoint.position, Quaternion.identity) as GameObject;
        return projectileObj;
    }

}
