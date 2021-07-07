using UnityEngine;
using System.Collections;

public class PowerupShield : PowerUp
{
    public override string Name { get { return "Shield"; } }
    private static int DURATION = 15;
    public override IEnumerator UsePowerup(GameObject player)
    {
        CarController controller = player.GetComponent<CarController>();
        PlayerStats ps = player.GetComponent<PlayerStats>();
        bool usedBrick = false;
        ps.StartShield();        

        if (ps.hasWhiteBrick)
        {
            usedBrick = true;
            ps.hasShield = true;
            ps.UsedPowerup();
        } else
        {
            ps.hasShield = true;
            ps.UsedPowerup();
            yield return new WaitForSeconds(DURATION);
            ps.hasShield = false;
            ps.StopShield();
        }
    }
}