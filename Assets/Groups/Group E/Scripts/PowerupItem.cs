using System.Collections;
using UnityEngine;

public class PowerupItem : MonoBehaviour
{
    private GameManager_E gameManager;
    private MissileManager missileManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager_E>();
        missileManager = FindObjectOfType<MissileManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(Pickup(other));
        }
    }

    public IEnumerator Pickup(Collider player)
    {
        PowerUp[] powerupsArr = { new PowerupShield(), new PowerupSpeed(), new PowerupReverseSteer(gameManager), new PowerupAttack(missileManager, gameManager) };

        int rnd = Random.Range(0, powerupsArr.Length);
        PowerUp powerup = powerupsArr[rnd];

        PlayerStats ps = player.GetComponent<PlayerStats>();
        if (!(ps.hasPowerup))
        {
            ps.power = powerup;
            ps.hasPowerup = true;
           //ps.textPowerup.text = "Powerup: \n" + powerup.Name;

            switch (powerup.Name)
            {
                case "Speed":
                    ps.imagePowerupSpeed.enabled = true;
                    break;
                case "Attack":
                    ps.imagePowerupAttack.enabled = true;
                    break;
                case "ReverseSteer":
                    ps.imagePowerupReverse.enabled = true;
                    break;
                case "Shield":
                    ps.imagePowerupShield.enabled = true;
                    break;
                default:
                    break;
            }
        }

        GetComponent<Collider>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;

        yield return new WaitForSeconds(5);

        GetComponent<Collider>().enabled = true;
        GetComponent<MeshRenderer>().enabled = true;
    }
}
