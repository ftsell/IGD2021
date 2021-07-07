using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JPlayerStats : MonoBehaviour
{
    public MinifigControllerJ player;
    PowerUp_Speed powerUpSpeed;
    PowerUp_Fireball powerUpFireball;
    PowerUp_Shield powerUpShield;
    public GameObject shield;
    public int fireballCount = 0;
    public bool powerUpSpeed_IsActive = false;
    private void Update()
    {
        
    }
    public void AddPowerUp(PowerUpType type)
    {
        switch (type)
        {
            case PowerUpType.Speed:
                powerUpSpeed = new PowerUp_Speed();
               StartCoroutine( powerUpSpeed.ApplyPowerUp(player));
                break;
            case PowerUpType.Shield:
                powerUpShield = new PowerUp_Shield();
                StartCoroutine(powerUpShield.ApplyPowerUp(shield,player));
                break;
            case PowerUpType.Fireball:
                Debug.Log("Add Fireballs");
                powerUpFireball = new PowerUp_Fireball();
                powerUpFireball.ApplyPowerUp(player);
                break;
            default:
                break;
        }
    }
}
