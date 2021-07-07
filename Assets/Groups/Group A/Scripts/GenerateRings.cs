using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GenerateRings : MonoBehaviour
{

    public static GenerateRings instance = null;


    public GameObject wallPrefab;
    public double radius = 8.0;
    public int numberOfRings = 2;
    public float lengthOfBlock = 2;
    public float yLevel = 0;
    public float randomCapProb = 0.1f;
    public float offsetRotation = 5f, offsetRadius = 1f, offsetAngle = 5f, offsetHeight = 0.1f;
    public bool rotate = false;
    public float rotationSpeed = 3f;
    public int ringToRotate = 1;
    public float maxLinkDistance = 5f;

    //Arrays für Ringe
    private List<GameObject> Rings = new List<GameObject>();

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);


        for(int ring = 1; ring <= numberOfRings; ring++)
        {
            GameObject currentRing = new GameObject();
            float circumference = (float)(Mathf.PI * 2 * radius * ring);
            int numberOfBlocks = Mathf.CeilToInt( circumference / lengthOfBlock);
            float currentRadius = (float)(radius * ring);
            List<GameObject> blocksOnCurrentRing = new List<GameObject>();
            Bounds? lastBounds = null;
            for (int block = 1; block <= numberOfBlocks; block++)
            {
                if (Random.Range(0, 100) <= randomCapProb * 100) continue;
                float currentAngle = (360f / numberOfBlocks) * block;
                Vector3 position = PointOnCircle(currentRadius + Random.Range(-offsetRadius, offsetRadius), currentAngle + Random.Range(-offsetRotation, offsetRotation));
                //Debug.Log("currentAngle: " + currentAngle + "  numberOfBlocks: " + numberOfBlocks + "  block: " + block + "  xPos: " + position.x + "  yPos: " + position.y);

                GameObject currentBlock = Instantiate(wallPrefab, position, Quaternion.Euler(0, 360-currentAngle + Random.Range(-offsetAngle, offsetAngle), 0));
                currentBlock.transform.SetParent(currentRing.transform);
                blocksOnCurrentRing.Add(currentBlock);
                
            }
            currentRing.AddComponent<NavMeshSurface>();
            currentRing.layer = 19;
            currentRing.GetComponent<NavMeshSurface>().collectObjects = CollectObjects.Children;
            currentRing.GetComponent<NavMeshSurface>().BuildNavMesh();

            foreach(GameObject block in blocksOnCurrentRing )
            {
                Bounds currentBlockBounds = block.GetComponent<BoxCollider>().bounds;
                currentBlockBounds.Expand(-1.5f);
                if (lastBounds != null)
                {
                    Bounds old = lastBounds ?? new Bounds();
                    if (!currentBlockBounds.Intersects(old))
                    {
                        Vector3 closestPointOld = old.ClosestPoint(block.transform.position);
                        closestPointOld.y += old.extents.y;
                        Vector3 closestPointNew = currentBlockBounds.ClosestPoint(closestPointOld);
                        closestPointNew.y += currentBlockBounds.extents.y;
                        if (maxLinkDistance >= Vector3.Distance(closestPointOld, closestPointNew))
                        {
                            NavMeshHit hit;

                            NavMeshLink newLink = block.AddComponent<NavMeshLink>();
                            NavMesh.SamplePosition(closestPointNew, out hit, maxLinkDistance, NavMesh.AllAreas);
                            newLink.startPoint = block.transform.InverseTransformPoint(hit.position);
                            NavMesh.SamplePosition(closestPointOld, out hit, maxLinkDistance, NavMesh.AllAreas);
                            newLink.endPoint = block.transform.InverseTransformPoint(hit.position);
                            newLink.bidirectional = true;
                            newLink.autoUpdate = true;
                        }
                    }
                }
                lastBounds = currentBlockBounds;

            }

            Rings.Add(currentRing);
        }
    }


    public Vector3 PointOnCircle(float radius, float angleInDegrees)
    {
        // Convert from degrees to radians via multiplication by PI/180        
        float x = (float)(radius * Mathf.Cos(angleInDegrees * Mathf.PI / 180F));
        float y = (float)(radius * Mathf.Sin(angleInDegrees * Mathf.PI / 180F));

        return new Vector3(x, yLevel + Random.Range(-offsetHeight, offsetHeight), y);
    }

    public Vector3 getSpawnLocation(int? sector = null, int noOfSectors = 4, bool randomRing = false)
    {
        int ringNo = randomRing ? Random.Range(0, Rings.Count) : Rings.Count - 1;
        GameObject ring = Rings[ringNo];

        int minIndex = ring.transform.childCount / noOfSectors * (sector ?? 0);
        int maxIndex = sector == null ? ring.transform.childCount : (ring.transform.childCount / noOfSectors) * ((sector ?? 0) + 1) ;
        int blockNo = Random.Range(minIndex, maxIndex);

       Transform block = ring.transform.GetChild(blockNo);
        Vector3 spawnLocation = block.position;
        spawnLocation.y += 4;
        return spawnLocation;
    }



    public void RotateRing(int ringNr, float angle)
    {
        Rings[ringNr - 1].transform.Rotate(0, angle, 0);
    }

    public int i = 0;
    private void Update()
    {
        if(rotate) RotateRing(ringToRotate, rotationSpeed * Time.deltaTime);
    }
}
