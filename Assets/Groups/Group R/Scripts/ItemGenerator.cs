using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    public GameObject[] ItemPrefabs;

    private List<GameObject> notInstantiated;
    
    public float spawnHeight = 7.0f;
    public float minSpawnZ = -7.0f;
    public float maxSpawnZ = 7.0f;

    public float maxSpawnTime = 20.0f;
    public float minSpawnTime = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        notInstantiated = new List<GameObject>(ItemPrefabs);
        StartCoroutine(itemSpawner());
    }

    void Update(){
        
    }

    private void spawnItem(){
        //Todo check if previously instantiated items are destroyed and may be reinstantiated
        int randomChoice = Random.Range(0,notInstantiated.Count);
        GameObject newItem = Instantiate(notInstantiated[randomChoice]) as GameObject;
        // notInstantiated.Remove(notInstantiated[randomChoice]);
        newItem.transform.position = new Vector3(0.0f,spawnHeight,Random.Range(minSpawnZ,maxSpawnZ));
    }

    IEnumerator itemSpawner(){
        yield return new WaitForSeconds(2); //Initially wait a bit before spawning an item
        while(true){
            yield return new WaitForSeconds(Random.Range(minSpawnTime,maxSpawnTime));
            spawnItem();
        }
    }
}
