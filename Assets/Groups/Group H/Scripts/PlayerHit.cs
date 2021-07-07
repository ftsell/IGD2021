using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHit : MonoBehaviour
{
    // the delay in seconds
    private float invincibleTime = 2.4f;
    private float blinkingTime = 0.4f;

    // the audio file for the sound that the figure should make when getting hit
    public AudioClip explodeAudioClip;
    public AudioClip playerTurnsIntoGhost;

    public CapsuleCollider collider;
    public GameObject minifigCharacter;
    private bool isHidden;
    private bool invincible;
    private bool stillAlive;

    public Health health; //Calling reduceHealth function from Health script

    // Start is called before the first frame update
    void Start()
    {
        isHidden = false;
        invincible = false;
        stillAlive = true;
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Vehicle" && !invincible)
        {
            takeDamage();
        }
    }

    void takeDamage()
    {
        if (!invincible)
        {
            stillAlive = health.reduceHealth();
            invincible = true;
            if (stillAlive)
            {
                StartCoroutine(EnableInvincibility(invincibleTime));
            }
            else
            {
                GetComponent<AudioSource>().PlayOneShot(playerTurnsIntoGhost);
                StartCoroutine(EnableInvincibility(99));
            }
        }
    }

    IEnumerator EnableInvincibility(float time)
    {
        // deactivate collisions
        //collider.enabled = false;
        GetComponent<AudioSource>().PlayOneShot(explodeAudioClip);

        // make player blink
        for (float i = 0; i < time; i += blinkingTime)
        {
            ToggleVisibility();
            yield return new WaitForSeconds(blinkingTime);
        }
        // activate collisions
        //collider.enabled = true;
        invincible = false;
    }

    void ToggleVisibility()
    {
        if (isHidden)
        {
            minifigCharacter.transform.localScale = Vector3.one;
        }
        else
        {
            minifigCharacter.transform.localScale = Vector3.zero;
        }
        isHidden = !isHidden;
    }
}
