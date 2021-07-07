using UnityEngine;
using System.Collections;

public class PowerupReverseSteer : PowerUp
{
    public GameManager_E gameManager;
    public int DELAY = 1;
    public int DURATION = 5;
    public int WHITE_BRICK_DURATION = 10;

    public PowerupReverseSteer(GameManager_E gameManager_e)
    {
        gameManager = gameManager_e;
    }

    public override string Name { get { return "ReverseSteer"; } }
    public override IEnumerator UsePowerup(GameObject player)
    {
        int activatorPosition = gameManager.GetPositionByPlayer(player.GetComponent<Transform>());

        PlayerStats aps = player.GetComponent<PlayerStats>();

        int targetPosition = activatorPosition - 1;
        if (targetPosition == 0) { targetPosition = 2; }

        Transform targetPlayer = gameManager.GetPlayerByPosition(targetPosition);

        PlayerStats ps = player.GetComponent<PlayerStats>();
        ps.UsedPowerup();

        CarController targetCarController = targetPlayer.GetComponent<CarController>();

        PlayerStats tps = targetPlayer.GetComponent<PlayerStats>();
        if(!tps.hasShield)
        {
            yield return new WaitForSeconds(DELAY);
            tps.StartReverseSteer();
            targetCarController.steeringReversed = true;
            tps.hasShield = false;
        }
        tps.StopShield();

        if (aps.hasWhiteBrick)
        {
            yield return new WaitForSeconds(WHITE_BRICK_DURATION);
        } else
        {
            yield return new WaitForSeconds(DURATION);
        }

        targetCarController.steeringReversed = false;
        tps.StopReverseSteer();
    }

}
