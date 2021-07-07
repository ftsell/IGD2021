using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class PowerupSpeed : PowerUp
{
    public override string Name { get { return "Speed"; } }
    private static int DURATION = 5;
    private static int MULTIPLIER = 2;
    private static int BRICKPOWER = 15;
    public override IEnumerator UsePowerup(GameObject player)
    {
        CarController controller = player.GetComponent<CarController>();
        PlayerStats ps = player.GetComponent<PlayerStats>();
        bool usedBrick = false;
        ps.StartSpeed();

        if(player.TryGetComponent(out NavMeshAgent agent))
        {

            if (ps.hasWhiteBrick)
            {
                usedBrick = true;
                agent.acceleration *= MULTIPLIER;
                agent.speed *= MULTIPLIER;
            }

            agent.acceleration += BRICKPOWER;
            agent.speed += BRICKPOWER;

            ps.UsedPowerup();

            yield return new WaitForSeconds(DURATION);

            ps.StopSpeed();

            agent.acceleration -= BRICKPOWER;
            agent.speed -= BRICKPOWER;

            if (usedBrick)
            {
                agent.acceleration /= MULTIPLIER;
                agent.speed /= MULTIPLIER;
            }
        } else {
            if (ps.hasWhiteBrick)
            {
                usedBrick = true;
                controller.maxVelocity += BRICKPOWER;
                controller.maxAcceleration += BRICKPOWER;
            }

            controller.maxVelocity *= MULTIPLIER;
            controller.maxAcceleration *= MULTIPLIER;

            ps.UsedPowerup();

            yield return new WaitForSeconds(DURATION);

            ps.StopSpeed();

            controller.maxVelocity /= MULTIPLIER;
            controller.maxAcceleration /= MULTIPLIER;

            if (usedBrick)
            {
                controller.maxVelocity -= BRICKPOWER;
                controller.maxAcceleration -= BRICKPOWER;
            }
        }


    }

}
