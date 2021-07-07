using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        HealthSystemI healthSystem = new HealthSystemI(100);
        //healthSystem.Damage(10);
        //Debug.Log("Health" + healthSystem.GetHealth2());
        //healthSystem.Heal(10);

    }

   
}
