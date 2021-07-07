using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp_Fireball : MonoBehaviour
{
    public void RemovePowerUp(MinifigControllerJ controller)
    {

        Debug.Log("Removed fireball");
    }

    public void ApplyPowerUp(MinifigControllerJ controller)
    {
        controller.gameObject.GetComponent<JPlayerStats>().fireballCount+=5;
        Debug.Log("Addd 5 Fireballs");

    }
}
