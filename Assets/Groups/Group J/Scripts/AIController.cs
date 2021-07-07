using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class AIController : MonoBehaviour
{
    private float mass = 3.0f;
    private float hitForce = 3.0f;
    public Vector3 impact = Vector3.zero;
    private Animator animator;
    public GameObject Minifig;
    private CharacterController controller;
    private AudioClip explosion;
    private AudioSource audio;

    public float lookRadius = 5f;
    public List<GameObject> players = new List<GameObject>();
    private List<Transform> targets;
    private NavMeshAgent agent;
    private MinifigControllerJ controllerJ;
    string controlScheme;
    private bool collided = false;
    public GameObject obstacle;
    private Transform obstacleTransform;
    private CollisionDetector collisionDetector;
    public int ownTeam;

    public float wanderRadius;
    public float wanderTimer;
    private float timer;
    private Rigidbody rb;
    private BoxCollider box; private GameManagerJ gameplayManager;
    public bool isAI = false;
    private GameObject AI;

    void Awake()
    {
        gameplayManager = GameObject.FindObjectOfType<GameManagerJ>();
    }

    // Start is called before the first frame update
    void Start()
    {


        box = obstacle.GetComponent<BoxCollider>();
        rb = this.GetComponent<Rigidbody>();
        collisionDetector = this.GetComponent<CollisionDetector>();

        if (collisionDetector.isTeam1 == true)
        {
            ownTeam = 1;
        }
        else
        {
            ownTeam = 2;
        }

        controller = GetComponent<CharacterController>();
        animator = Minifig.GetComponent<Animator>();
        controllerJ = GetComponent<MinifigControllerJ>();
        explosion = controllerJ.explodeAudioClip;
        audio = GetComponent<AudioSource>();
        obstacleTransform = obstacle.transform;

        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (go.Equals(this.gameObject))
                continue;
            players.Add(go);
        }

        controlScheme = GetComponent<PlayerInput>().defaultControlScheme;
        if (controlScheme == "AI")
        {
            controllerJ.enabled = false;
        }
        agent = GetComponent<NavMeshAgent>();

        timer = wanderTimer;
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameplayManager.gameFinished)
        {
            for (int i = players.Count - 1; i >= 0; i--)
            {
                if (players[i].GetComponent<AIController>().ownTeam == this.ownTeam)
                {
                    players.RemoveAt(i);
                }
            }

            if (controlScheme == "AI")
            {
                isAI = true;

                timer += Time.deltaTime;
                controllerJ.PlaySpecialAnimation(MinifigControllerJ.SpecialAnimation.Walk);

                if (timer >= wanderTimer && agent.enabled == true)
                {
                    controllerJ.PlaySpecialAnimation(MinifigControllerJ.SpecialAnimation.Walk);
                    Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
                    agent.SetDestination(newPos);
                    timer = 0;
                }

                if (rb.IsSleeping())
                {
                    controllerJ.PlaySpecialAnimation(MinifigControllerJ.SpecialAnimation.Idle_Light);
                    //Debug.Log("sleeping");
                }

                for (int i = players.Count - 1; i >= 0; i--)
                {
                    if (!players[i].activeSelf)
                    {
                        players.RemoveAt(i);
                    }
                }

                targets = players.Select(go => go.transform).ToList();

                Transform tMin = null;
                float minDist = Mathf.Infinity;
                Vector3 currentPos = transform.position;

                foreach (Transform t in targets)
                {
                    float dist = Vector3.Distance(t.position, currentPos);
                    if (dist < minDist)
                    {
                        tMin = t;
                        minDist = dist;
                    }
                }

                float distance = Vector3.Distance(tMin.position, transform.position);
                float distanceObstacle = Vector3.Distance(obstacleTransform.position, transform.position);

                box.enabled = true;

                if (distanceObstacle <= 6 && !this.animator.GetCurrentAnimatorStateInfo(0).IsName("Punch"))
                {
                    Debug.Log("Jump");
                    controllerJ.PlaySpecialAnimation(MinifigControllerJ.SpecialAnimation.Jump);
                    //Physics.IgnoreLayerCollision(gameObject.layer, 21, true);// TryUseFireball();
                    if (this.animator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
                    {
                        box.enabled = false;
                    }
                }

                if (distance <= lookRadius && players.Count != 0 && agent.enabled == true)
                {
                    agent.SetDestination(tMin.position);
                    controllerJ.PlaySpecialAnimation(MinifigControllerJ.SpecialAnimation.Walk);

                    if (distance <= agent.stoppingDistance && !this.animator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
                    {
                        Debug.Log("Punch");
                        controllerJ.PlaySpecialAnimation(MinifigControllerJ.SpecialAnimation.Punch); //TryUseFireball();
                    }
                }
            }


            if (impact.magnitude > 0.2)
            {
                controller.Move(impact * Time.deltaTime);
            }
            impact = Vector3.Lerp(impact, Vector3.zero, 5 * Time.deltaTime);

            if (controllerJ.punchable == true)
            {
                controllerJ.PlaySpecialAnimation(MinifigControllerJ.SpecialAnimation.Punch, explosion);
                TryUseFireball();
                controllerJ.punchable = false;
            }
        }
    }
    public void TryUseFireball()
    {
        Debug.Log("Try use fireball");
        if (gameObject.GetComponent<JPlayerStats>().fireballCount > 0)
        {
            gameObject.GetComponent<ShootFireball>().Shoot();
        }
    }
    public void AddImpact(Vector3 force)
    {
        var dir = force.normalized;
        impact += dir.normalized * force.magnitude / mass;
    }

    public void OnTriggerStay(Collider other)
    {
        if (this.animator.GetCurrentAnimatorStateInfo(0).IsName("Punch") && other.gameObject.tag == "Player" && other.gameObject.layer != this.gameObject.layer)
        {
            Debug.Log("trigger");
            other.gameObject.GetComponent<AIController>().AddImpact(this.transform.forward * hitForce);
            // TryUseFireball();
            audio.clip = explosion;
            audio.Play();

            if (other.gameObject.GetComponent<AIController>().isAI == true)
            {
                AI = other.gameObject;
                other.gameObject.GetComponent<NavMeshAgent>().enabled = false;
                other.gameObject.GetComponent<MinifigControllerJ>().enabled = true;
                StartCoroutine(moveAgain());
            }
        }
    }

    IEnumerator moveAgain()
    {
        yield return new WaitForSeconds(0.5f);

        AI.GetComponent<MinifigControllerJ>().enabled = false;
        AI.gameObject.GetComponent<NavMeshAgent>().enabled = true;
        AI.GetComponent<MinifigControllerJ>().PlaySpecialAnimation(MinifigControllerJ.SpecialAnimation.Walk);
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
