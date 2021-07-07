using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystemI

{
    private int _health;
    public HealthSystemI(int health)
    {
        _health = health;

    }

    public int GetHealth2()
    {
        return _health;
    }


    public void Damage(int damageAmount)
    {
        _health -= damageAmount; 
    }

    public void Heal(int healAmount)
    {
        _health += healAmount;
    }

}
