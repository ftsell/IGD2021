using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp_Speed : MonoBehaviour
{
    private float secondsToUse = 10;
    private float initalPlayerSpeed;
    private float initalPlayerAcceleration;

    public void RemovePowerUp(MinifigControllerJ controller)
    {
        controller.maxForwardSpeed = initalPlayerSpeed;
        controller.acceleration = initalPlayerAcceleration;
        controller.gameObject.GetComponent<JPlayerStats>().powerUpSpeed_IsActive = false;
        Debug.Log("Removed");
    }

    public IEnumerator ApplyPowerUp(MinifigControllerJ controller)
    {
        if (!controller.gameObject.GetComponent<JPlayerStats>().powerUpSpeed_IsActive)
        {
            initalPlayerAcceleration = controller.acceleration;
            initalPlayerSpeed = controller.maxForwardSpeed;
            controller.maxForwardSpeed *= 2;
            controller.acceleration *= 2;
            controller.gameObject.GetComponent<JPlayerStats>().powerUpSpeed_IsActive = true;
            yield return new WaitForSeconds(secondsToUse);
            RemovePowerUp(controller);
        }
    }
}
