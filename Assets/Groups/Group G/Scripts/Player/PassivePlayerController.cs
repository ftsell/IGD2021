using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PassivePlayerController : MonoBehaviour
{
    public bool AI = false;

    public float Speed = 25.0f;
    public float Tilt = 1.0f;
    public Boundary Boundary;
    public WeaponSystem[] WeaponSystems;
    public float SlowRate = 30f;
    public float SlowDuration = 5f;
    public float SlowMultiplier = 0.1f;
    public Text SlowCooldownText;
    public Canvas SlowEffect;

    public GameObject[] WaveHazards;
    public Vector3 WaveSpawnValues;
    public int HazardCount = 3;
    public float WaveRate = 5.0f;
    public Text WaveCooldownText;
    public int TimeUntilLvlUp = 30;
    
    private float TimeCounter = 1.0f;
    private bool LevelSet = false;
    private int Level;
    private Vector2 Movement;
    private float NextWave;
    private float NextSlow;
    private int CurrentWeaponIndex = 0;
    private GameController_G GameController;

    private Vector3 ChasePlayerPosition;
    private float AIWaveWait;
    private float AISlowWait;

    AudioSource AudioSource;
    private void Awake()
    {
        AudioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            GameController = gameControllerObject.GetComponent<GameController_G>();
        }
        if (GameController == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }

        string controlScheme = GetComponent<PlayerInput>().defaultControlScheme;
        GetComponent<PlayerInput>().SwitchCurrentControlScheme(controlScheme, Keyboard.current);

        UpdateWeaponVisualization();

        if (AI)
        {
            ChasePlayerPosition = transform.position;
            StartCoroutine(SelectRandomWeapon());
            AIWaveWait = 3f;
            AISlowWait = 5f;
        }
    }

    private void Update()
    {
        if (AI)
        {
            ChasePlayer();
        
            if (Time.time > NextWave + AIWaveWait)
            {
                AIWaveWait = Random.Range(5f, 10f);
                NextWave = Time.time + WaveRate - Level;
                StartCoroutine(SpawnWave());
            }

            //slowing players and slowmotion effect
            if (Time.time > NextSlow + AISlowWait)
            {
                AISlowWait = Random.Range(10f, 15f);
                SlowPlayers();
                NextSlow = Time.time + SlowRate - Level;
            }
        }

        //cooldown for next slow
        float slowCooldown = (NextSlow - Time.time);
        if (slowCooldown > 0)
        {
            SlowCooldownText.text = "Slow: " + (int)slowCooldown;
        }
        else
        {
            SlowCooldownText.text = "Slow: READY";
        }

        //cooldown for next wave
        float waveCooldown = (NextWave - Time.time);
        if (waveCooldown > 0)
        {
            WaveCooldownText.text = "Spawn Wave: " + (int)waveCooldown;
        }
        else
        {
            WaveCooldownText.text = "Spawn Wave: READY";
        }
        
        TimeCounter += Time.deltaTime;
        if ((int) TimeCounter % TimeUntilLvlUp == 0 && LevelSet == false) 
        {
            Level += 1;
            LevelSet = true;
            //Debug.Log("Level Up!");
            
        }
        if ((int) TimeCounter % 2 == 1)
        {
            LevelSet = false;
        }


    }

    //for physics
    void FixedUpdate()
    {
        //only move on x-axis
        Vector3 movement = new Vector3(Movement.x, 0.0f, 0.0f);
        GetComponent<Rigidbody>().velocity = movement * Speed;

        GetComponent<Rigidbody>().position = new Vector3
        (
            Mathf.Clamp(GetComponent<Rigidbody>().position.x, Boundary.xMin, Boundary.xMax),
            0.0f,
            Mathf.Clamp(GetComponent<Rigidbody>().position.z, Boundary.zMin, Boundary.zMax)
        );

        //GetComponent<Rigidbody>().rotation = Quaternion.Euler(0.0f, this.transform.rotation.y, GetComponent<Rigidbody>().velocity.x * -Tilt);
    }

    private void ChasePlayer()
    {
        GameObject closestPlayer = FindClosestPlayer();
        if (closestPlayer == null) return;

        float step = Speed * Time.deltaTime; // calculate distance to move

        //when reached goal position
        if (Vector3.Distance(transform.position, ChasePlayerPosition) < 0.1f)
        {
            Shoot();

            //update position for next chase
            float offsetX = Random.Range(-3f, 3f);
            Vector3 goalPosition = new Vector3(
                Mathf.Clamp(closestPlayer.transform.position.x + offsetX, Boundary.xMin, Boundary.xMax),
                0.0f,
                Mathf.Clamp(GetComponent<Rigidbody>().position.z, Boundary.zMin, Boundary.zMax));
            ChasePlayerPosition = goalPosition;
        }
        GetComponent<Rigidbody>().position = Vector3.MoveTowards(transform.position, ChasePlayerPosition, step);
    }

    private GameObject FindClosestPlayer()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        if (players.Length == 0)
        {
            return null;
        }

        GameObject closest;

        // If there is only exactly one anyway skip the rest and return it directly
        if (players.Length == 1)
        {
            closest = players[0];
            return closest;
        }

        // Otherwise: Take the enemies
        closest = players.OrderBy(go => (transform.position - go.transform.position).sqrMagnitude).First();

        return closest;
    }

    IEnumerator SelectRandomWeapon()
    {
        while (AI)
        {
            RandomWeaponChange();
            yield return new WaitForSeconds(Random.Range(1f, 2f));
        }
    }

    private void RandomWeaponChange()
    {
        int choice = Random.Range(0, 2);
        if (choice == 0)
        {
            UpdateWeaponByIndex(CurrentWeaponIndex - 1);
        }
        else
        {
            UpdateWeaponByIndex(CurrentWeaponIndex + 1);
        }
    }

    public void UpdateWeaponByIndex(int index)
    {
        CurrentWeaponIndex = Mathf.RoundToInt(Mathf.Repeat((float)index, (float)(WeaponSystems.Length)));
        //Debug.Log(CurrentWeaponIndex);
        UpdateWeaponVisualization();
    }

    public void UpdateWeaponVisualization()
    {

        if (gameObject.transform.Find("CurrentWeapon") != null)
        {
            GameObject.Destroy(gameObject.transform.Find("CurrentWeapon").gameObject);
        }

        GameObject weapon = Instantiate(WeaponSystems[CurrentWeaponIndex].Bullet, WeaponSystems[CurrentWeaponIndex].ShotSpawnPoints[0].transform.position, WeaponSystems[CurrentWeaponIndex].ShotSpawnPoints[0].transform.rotation);
        weapon.name = "CurrentWeapon";
        weapon.tag = "Obstacle";
        weapon.transform.parent = gameObject.transform;
        weapon.transform.position = WeaponSystems[CurrentWeaponIndex].ShotSpawnPoints[0].transform.position;
        weapon.GetComponent<Rigidbody>().isKinematic = true;
        weapon.GetComponent<Collider>().enabled = false;
        weapon.GetComponent<Mover>().enabled = false;
        weapon.transform.Find("CanvasHPBar").gameObject.SetActive(false);

    }

    private void OnMoveDpad(InputValue value)
    {
        if (AI) return;

        Vector2 input = value.Get<Vector2>();
        input.Normalize();
        Movement = input;

        if(input.y > 0)
        {
            // 5 button (upwards) pressed
        }
        if (input.y < 0)
        {
            // 2 button (downwards) pressed
            //slowing players and slowmotion effect
            if (Time.time > NextSlow)
            {
                SlowPlayers();
                NextSlow = Time.time + SlowRate - Level;
            }

        }

    }

    private void SlowPlayers()
    {
        foreach (GameObject player in GameController.Players)
        {
            PlayerController controller = player.GetComponent<PlayerController>();
            controller.SetSpeedBoostOn(SlowMultiplier);
            StartCoroutine(WaitUntilExpires(controller));
        }
    }

    IEnumerator WaitUntilExpires(PlayerController controller)
    {
        SlowEffect.gameObject.SetActive(true);
        yield return new WaitForSeconds(SlowDuration);
        controller.SetSpeedBoostOff();
        SlowEffect.gameObject.SetActive(false);

    }

    private void OnMenu()
    {

    }

    private void OnNorthPress()
    {
        if (AI) return;
        // + button
        Shoot();
    }

    private void Shoot()
    {
        string tag = transform.gameObject.tag;
        WeaponSystems[CurrentWeaponIndex].Fire(tag);
    }

    private void OnNorthRelease()
    {
        
    }

    private void OnEastPress()
    {
        if (AI) return;
        // 4 button
        UpdateWeaponByIndex(CurrentWeaponIndex - 1);
    }

    private void OnEastRelease()
    {
        
    }

    private void OnSouthPress()
    {
        if (AI) return;
        // 6 button
        UpdateWeaponByIndex(CurrentWeaponIndex + 1 );
    }
    private void OnSouthRelease()
    {
        
    }

    private void OnWestPress()
    {
        if (AI) return;
        if (Time.time > NextWave)
        {
            NextWave = Time.time + WaveRate - Level;
            StartCoroutine(SpawnWave());
        }
    }

    IEnumerator SpawnWave()
    {
        for (int i = 0; i < HazardCount; i++)
        {
            GameObject waveHazard = WaveHazards[Random.Range(0, WaveHazards.Length)];
            Vector3 spawnPosition = new Vector3(Random.Range(-WaveSpawnValues.x, WaveSpawnValues.x), WaveSpawnValues.y, WaveSpawnValues.z);
            Quaternion spawnRotation = Quaternion.identity;
            Instantiate(waveHazard, spawnPosition, spawnRotation);
            yield return new WaitForSeconds(0.5f);
        }
    }
    private void OnWestRelease()
    {
       
    }
}
