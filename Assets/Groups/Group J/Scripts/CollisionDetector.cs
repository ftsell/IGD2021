using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class CollisionDetector : MonoBehaviour
{
    public AudioClip hitSound;
    AudioSource audioSource;
    public GameObject model;
    [SerializeField]
    private float durationSeconds;
    [SerializeField]
    private float deltaTime;
    private GameManagerJ gameplayManager;
    public bool isTeam1 = false;
    public bool isTeam2 = false;
    public bool dead = false;
    private CapsuleCollider cp;
    string controlScheme;

    void Awake()
    {
       gameplayManager = GameObject.FindObjectOfType<GameManagerJ>();
    }
    public void Start()
    {
        cp = this.GetComponent<CapsuleCollider>();
        audioSource = GetComponent<AudioSource>();
        durationSeconds = 1.5f;
        deltaTime = 0.15f;
        PlayerPrefs.SetInt("playerDeaths", 0);
        controlScheme = GetComponent<PlayerInput>().defaultControlScheme;
    }

    public void Update()
    {
        //if (Physics.Raycast(transform.position + Vector3.down * 0.001f, Vector3.down, 0.3f))
        //{
        //    Debug.DrawRay(transform.position + Vector3.down * 0.001f, Vector3.down * (0.3f), Color.red);
        //    Debug.Log("Grounded");
        //hitLava();
        //}



        //if (controlScheme == "AI" && Physics.Raycast(transform.position + Vector3.down * 0.001f, Vector3.down, 0.75f) == false)
        //{
        //   Debug.DrawRay(transform.position + Vector3.down * 0.001f, Vector3.down * (0.75f), Color.red);
        //   Debug.Log("notGrounded");
        //   //hitLava();
        //}
    }

    public void hitLava()
    {
        Debug.Log("Player Died");
        dead = true;
        if (isTeam1)
            gameplayManager.team1LavaDeath++;
        else
            gameplayManager.team2LavaDeath++;

        if (gameplayManager.team1LavaDeath == 2)
            gameplayManager.gameFinished = true;

        if (gameplayManager.team2LavaDeath == 2)
            gameplayManager.gameFinished = true;

        this.gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == ("Obstacle"))
        {
            StartCoroutine(ActivateInvincibility());
            gameplayManager.UpdateDeath(isTeam1);
        }

        if(collision.gameObject.tag == ("Lava"))
        {
            Debug.Log("Player Died");
            dead = true;
            if (isTeam1)
                gameplayManager.team1LavaDeath++;
            else
                gameplayManager.team2LavaDeath++;

            if (gameplayManager.team1LavaDeath == 2)
                gameplayManager.gameFinished = true;

            if (gameplayManager.team2LavaDeath == 2)
                gameplayManager.gameFinished = true;

            this.gameObject.SetActive(false);
        }
    }

    IEnumerator ActivateInvincibility()
    {
        Physics.IgnoreLayerCollision(gameObject.layer, 21, true);

        if (hitSound)
        {
            audioSource.PlayOneShot(hitSound);
        }

        for (float i = 0; i < durationSeconds; i += deltaTime)
        {
            if (model.transform.localScale == Vector3.one)
            {
                ScaleModelTo(Vector3.zero);
            }
            else
            {
                ScaleModelTo(Vector3.one);
            }

            yield return new WaitForSeconds(deltaTime);
        }
        Physics.IgnoreLayerCollision(gameObject.layer, 21, false);
        ScaleModelTo(Vector3.one);
    }

    private void ScaleModelTo(Vector3 scale)
    {
        model.transform.localScale = scale;
    }
}
