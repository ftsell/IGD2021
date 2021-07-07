using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
{
    public bool AI = false;
    public float Speed;
    public float Tilt;
    public Boundary Boundary;
    public WeaponSystem[] WeaponSystems;
    public Enums.DestroyType Type;

    private Vector2 Movement;
    private float SpeedOriginal;
    private int CurrentWeaponIndex = 0;

    private GameObject Target;
    private Vector3 ChaseTargetPosition;
    private AIState State;
    private enum AIState
    {
        Roaming,
        ChaseTarget
    }

    AudioSource AudioSource;

    private void Start()
    {
        string controlScheme = GetComponent<PlayerInput>().defaultControlScheme;
        GetComponent<PlayerInput>().SwitchCurrentControlScheme(controlScheme, Keyboard.current);
        State = AIState.Roaming;
        if (AI)
        {
            StartCoroutine(Roaming());
            Target = this.gameObject;
        }
    }

    private void Awake()
    {
        AudioSource = GetComponent<AudioSource>();

        
    }
    private void Update()
    {
        if (AI)
        {
            switch (State)
            {
                default:
                case AIState.Roaming:
                    FindTarget();
                    break;
                case AIState.ChaseTarget:
                    ChaseTarget();
                    break;
            }
            
        }
    }

    IEnumerator Roaming()
    {
        while (AI && (State == AIState.Roaming))
        {
            //MovingDirection [-1,1] in x and z
            Movement = new Vector2(Random.Range(0, 2) * -Mathf.Sign(transform.position.x), Random.Range(0, 2) * -Mathf.Sign(transform.position.z));
            //Maneuver time - Moving time
            yield return new WaitForSeconds(Random.Range(1f, 2f));
            Movement = new Vector2(0f, 0f);
            //Wait until next Movement
            yield return new WaitForSeconds(Random.Range(0f, 0.5f));
            FindTarget();
        }
    }

    //for physics
    void FixedUpdate()
    {

        Vector3 movement = new Vector3(Movement.x, 0.0f, Movement.y);
        GetComponent<Rigidbody>().velocity = movement * Speed;
        
        GetComponent<Rigidbody>().position = new Vector3
        (
            Mathf.Clamp(GetComponent<Rigidbody>().position.x, Boundary.xMin, Boundary.xMax),
            0.0f,
            Mathf.Clamp(GetComponent<Rigidbody>().position.z, Boundary.zMin, Boundary.zMax)
        );

        GetComponent<Rigidbody>().rotation = Quaternion.Euler(0.0f, 0.0f, GetComponent<Rigidbody>().velocity.x * -Tilt);
    }

    public void SetSpeedBoostOn(float speedMultiplier)
    {
        SpeedOriginal = Speed;
        Speed *= speedMultiplier;
    }

    public void SetSpeedBoostOff()
    {
        Speed = SpeedOriginal;
    }
    
    public void UpdateWeaponByIndex(int index)
    {
        CurrentWeaponIndex = Mathf.Clamp(index, 0, WeaponSystems.Length - 1);
    }
    
    public int GetCurrentWeaponIndex()
    {
        return CurrentWeaponIndex;
    }

    private void FindTarget()
    {
        float minTargetRange = 10f;
        GameObject closest = FindClosestEnemyOfValidType();
        if (closest == null) return;
        
        if (Mathf.Abs((closest.transform.position - transform.position).z) > minTargetRange)
        {
            Target = closest;
            State = AIState.ChaseTarget;
        }
    }

    private void ChaseTarget()
    {
        if (Target == null)
        {
            State = AIState.Roaming;
            StartCoroutine(Roaming());
            return;
        }
        // Move our position a step closer to the target.
        float step = Speed * Time.deltaTime; // calculate distance to move

        
        if (Vector3.Distance(transform.position, ChaseTargetPosition) <= 0.5f)
        {
            float offsetX = Random.Range(-3f, 3f);
            float offsetZ = Random.Range(30f, 45f);
            Vector3 offsetPosition = new Vector3(Mathf.Clamp(Target.transform.position.x + offsetX, Boundary.xMin, Boundary.xMax),
                0.0f,
                Mathf.Clamp(Target.transform.position.z - offsetZ, Boundary.zMin, Boundary.zMax)
                );
            ChaseTargetPosition = offsetPosition;
        }
        GetComponent<Rigidbody>().position = Vector3.MoveTowards(transform.position, ChaseTargetPosition, step);
        Shoot();
    }

    public GameObject FindClosestEnemyOfValidType()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        // If no enemies found at all directly return nothing
        // This happens if there simply is no object tagged "Enemy" in the scene
        if (enemies.Length == 0)
        {
            //Debug.LogWarning("No enemies found!", this);
            return null;
        }

        // If there is only exactly one anyway skip the rest and return it directly
        if (enemies.Length == 1)
        {
            //dont chase what u cant destroy
            if(enemies[0].GetComponent<Enemy_G>().CanBeDestroyedBy == Enums.DestroyType.All)
            {
                return enemies[0];
            }else if(enemies[0].GetComponent<Enemy_G>().CanBeDestroyedBy != Type)
            {
                return null;
            }
            
        }

        enemies.OrderBy(go => (transform.position - go.transform.position).sqrMagnitude);
        
        foreach(GameObject go in enemies)
        {
            Enums.DestroyType enemyType = go.GetComponent<Enemy_G>().CanBeDestroyedBy;
            if (enemyType == Enums.DestroyType.All || enemyType == Type)
            {
                return go;
            }
        }

        return null;
    }

    private void Shoot()
    {
        string tag = transform.gameObject.tag;
        WeaponSystems[CurrentWeaponIndex].Fire(tag);
    }

    private void OnMoveDpad(InputValue value)
    {
        if (AI) return;

        Vector2 input = value.Get<Vector2>();
        input.Normalize();
        Movement = input;
        if (input.y > 0)
        {
            // (upwards) pressed
        }
        if (input.y < 0)
        {
            // (downwards) pressed

        }
    }
    private void OnMenu()
    {

    }

    private void OnNorthPress()
    {

    }

    private void OnNorthRelease()
    {

    }

    private void OnEastPress()
    {

    }

    private void OnEastRelease()
    {

    }

    private void OnSouthPress()
    {
        if (AI) return;
        Shoot();
    }
    private void OnSouthRelease()
    {

    }

    private void OnWestPress()
    {

    }

    private void OnWestRelease()
    {

    }
}
