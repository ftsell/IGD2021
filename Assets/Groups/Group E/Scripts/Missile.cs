using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
public class Missile : MonoBehaviour
{

    private Rigidbody rb;
    private NavMeshAgent navMeshAgent;
    private Transform toFollow = null;
    private GameObject missileOwner;

    public float missileSpeed = 150.0f;
    public GameObject explosionPrefab;


    public void Init(GameObject missileOwner)
    {
        rb = GetComponent<Rigidbody>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        this.missileOwner = missileOwner;
    }

    private void FixedUpdate()
    { 
        {
            if(toFollow != null)
            {
                navMeshAgent.SetDestination(toFollow.position);
            }       
        }
    }


    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            PlayerStats ps = collision.gameObject.GetComponent<PlayerStats>();
            Rigidbody playerRb = collision.gameObject.GetComponent<Rigidbody>();
            CarController playerController = collision.gameObject.GetComponent<CarController>();
            
            if(collision.gameObject.TryGetComponent(out NavAgentScript_E agent) && (!ps.hasShield))
            {
                agent.DisableAgentTemp();
            }

            if(ps.hasShield)
            {
                ps.SlowRemoveShield();
                ps.StopShield();
            } else
            {
                if (toFollow != null && !collision.gameObject.Equals(missileOwner) || toFollow == null)
                {
                    playerRb.AddExplosionForce(25000f, gameObject.transform.position, 8.0f);
                    playerController.StopCar();
                }
            }

            if (toFollow != null && !collision.gameObject.Equals(missileOwner) || toFollow == null)
            {
                AnimateExplosion(gameObject.transform);
                Destroy();
            }
        } 
    }

    private void AnimateExplosion(Transform explosionTransform)
    {
        GameObject explosionObject = Instantiate(explosionPrefab, explosionTransform.position, explosionTransform.rotation) as GameObject;
        explosionObject.SetActive(true);
        Destroy(explosionObject, 5.0f);
    }

    public void Shoot()
    {
        rb.velocity = transform.forward.normalized * missileSpeed;
    }

    public void Follow(Transform toFollow)
    {
        this.toFollow = toFollow;
        navMeshAgent.updatePosition = true;
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

}
