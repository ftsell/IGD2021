using UnityEngine;
using System.Collections;

public class PowerupAttack : PowerUp {

    private MissileManager missileManager;
    private GameManager_E gameManager;

    public PowerupAttack(MissileManager missileManager, GameManager_E gameManager)
    {
        this.missileManager = missileManager;
        this.gameManager = gameManager;
    }

    public override string Name { get { return "Attack"; } }
    public override IEnumerator UsePowerup(GameObject player)
    {
        CarController controller = player.GetComponent<CarController>();
        PlayerStats ps = player.GetComponent<PlayerStats>();
    
        // apply powerup actions
        Missile missile = missileManager.CreateMissile(player.transform, ps.hasWhiteBrick, player);

        if (ps.hasWhiteBrick)
        {
            Transform playerToAttack = GetPlayerToAttack(player.transform);
            missile.Follow(playerToAttack);
        } else
        {
            missile.Shoot();
        }

        ps.UsedPowerup();

        yield return new WaitForSeconds(15);

        // undo actions

        // if not already destroyed
        if(missile != null)
        {
            missile.Destroy();
        }
    }

    private Transform GetPlayerToAttack(Transform ownPlayer)
    {
        int ownPosition = gameManager.GetPositionByPlayer(ownPlayer);
        if (ownPosition == 1)
        {
            return gameManager.GetPlayerByPosition(2);
        }
        else
        {
            return gameManager.GetPlayerByPosition(ownPosition - 1);
        }
    }

}
