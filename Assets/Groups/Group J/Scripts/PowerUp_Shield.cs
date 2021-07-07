using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp_Shield : MonoBehaviour
{
    private float secondsToUse = 10;
    public void RemovePowerUp(MinifigControllerJ controller)
    {
        controller.punchable = true;
        Physics.IgnoreLayerCollision(controller.gameObject.layer, 21, false);
        Debug.Log("Removed shied");
    }

    public IEnumerator ApplyPowerUp(GameObject shieldPrefab,MinifigControllerJ controller)
    {
        //player.getcomponent.punchable = false??
        // und wenn einmal gepunched dann shild zerstören?
        var shield  =CreateShield(shieldPrefab,controller);
        controller.punchable = false;
        Physics.IgnoreLayerCollision(controller.gameObject.layer, 21, true);
        yield return new WaitForSeconds(secondsToUse);
        Destroy(shield);
        RemovePowerUp(controller);

    }

    GameObject CreateShield(GameObject shieldPrefab, MinifigControllerJ player)
    {
        var projectileObj = Instantiate(shieldPrefab,new Vector3(player.transform.position.x, player.transform.position.y+1, player.transform.position.z), Quaternion.identity) as GameObject;
        projectileObj.transform.SetParent(player.transform);
        return projectileObj;
    }
}
