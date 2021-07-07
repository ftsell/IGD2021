using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    [SerializeField] private Transform RespawnPointWSAD;
    public AudioSource playSoundRespawn;

    void OnTriggerEnter(Collider other)
    {
        print("Respawn");
        MinifigController otherController = (MinifigController)other.gameObject.GetComponent("MinifigController");
        otherController.TeleportTo(RespawnPointWSAD.transform.position);
        playSoundRespawn.Play();
        
    }
}
