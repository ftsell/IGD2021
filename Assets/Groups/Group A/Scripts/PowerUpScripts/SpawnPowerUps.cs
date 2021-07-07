using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPowerUps : MonoBehaviour
{
    public static SpawnPowerUps instance = null;

    public List<GameObject> spawnablePowerUps = new List<GameObject>();
    
    public GameObject pickableEquipment;
    public List<GameObject> pickablePowerUps = new List<GameObject>();

    public GameObject RespawnPointsSource;

    public float spawningStartTime = 0f;
    public float spawningRateSecs = 5f;
    public float equipSpawningRateSecs = 15f;

    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(this.gameObject);
    }

    private void Start()
    {
        InvokeRepeating("spawnPickup", spawningStartTime, spawningRateSecs);
        InvokeRepeating("spawnEquipment", spawningStartTime, equipSpawningRateSecs);
    }

    public void SpawnPowerUp(string powerUpIdentifier, Vector3 position, Quaternion rotation)
    {
        GameObject powerUpPrefab = spawnablePowerUps.Find(prefab => prefab.name == powerUpIdentifier);
        Instantiate(powerUpPrefab, position, rotation);
    }

    private void spawnPickup()
    {
        if(pickablePowerUps.Count >= 1)
        {
            GenerateRings rings = RespawnPointsSource.GetComponent<GenerateRings>();
            Vector3 spawnLocation = rings.getSpawnLocation(randomRing: true);
            spawnLocation.y += 2;
            GameObject pickablePowerUp = pickablePowerUps[Random.Range(0, pickablePowerUps.Count)];
            GameObject spawnedPickablePowerUp = Instantiate(pickablePowerUp, spawnLocation, Quaternion.Euler(0, 0, 0));
            spawnedPickablePowerUp.transform.SetParent(this.gameObject.transform);
        }
    }

    private void spawnEquipment()
    {
        Vector3 spawnLocation = new Vector3(0, 5, 0);
        GameObject spawnedEquipment= Instantiate(pickableEquipment, spawnLocation, Quaternion.Euler(0, 0, 0));
        spawnedEquipment.transform.SetParent(this.gameObject.transform);
    }

    public GameObject SpawnPlayerEquipment(string powerUpIdentifier, MinifigControllerWTH player)
    {
        string path = @"Minifig Character/jointScaleOffset_grp/Joint_grp/detachSpine/spine01/spine02/spine03/spine04/spine05/spine06/shoulder_R/armUp_R/arm_R/wristTwist_R/wrist_R/hand_R/BatSnap";
        //Transform batLock = player.transform.Find(path);
        GameObject powerUpPrefab = spawnablePowerUps.Find(prefab => prefab.name == powerUpIdentifier);
        GameObject bat = Instantiate(powerUpPrefab);
        bat.transform.SetParent(player.transform.Find(path), false);
        return bat;
    }
}
