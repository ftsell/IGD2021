using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int health = 3;
    public int maxHealth = 3;
    public int numberOfHearts = 3;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    public int ownID;

    public TrafficTrouble gameManager;
    public AudioClip increasingHealth;

    void Start()
    {
        gameManager.SubmitHealth(ownID, health);
    }

    public bool reduceHealth()
    {
        if (health > 0)
        {
            health--;
            gameManager.SubmitHealth(ownID, health);
        }
        // return if player is still alive
        return health > 0;
    }

    public void increaseHealth()
    {
        if (health < maxHealth && health > 0)
        {
            health++;
            GetComponent<AudioSource>().PlayOneShot(increasingHealth);
            gameManager.SubmitHealth(ownID, health);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (health > numberOfHearts)
        {
            health = numberOfHearts;
        }

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {//Show full heart
                hearts[i].sprite = fullHeart;
            }
            else
            {//Show empty heart
                hearts[i].sprite = emptyHeart;
            }

            //Make hearts invisible if we reduce the number of total hearts
            if (i < numberOfHearts)
            {//Show hearts
                hearts[i].enabled = true;
            }
            else
            {//Hide hearts
                hearts[i].enabled = false;
            }
        }   
    }
}
