using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject platform0;
    public GameObject platform1;
    public GameObject platform2;
    public GameObject platform3;
    public GameObject platform4;
    public GameObject bridge;
    public GameObject barrier;
    public GameObject ground;
    public CraneMovement leftCrane;
    public CraneMovement rightCrane;
    private const float minCenterDistance = 1.75f;
    private const float maxCenterDistance = 3.75f;
    private const float maxRotation = 5.0f;
    private const float distanceBridgeToPlatformCenter = 3.0f;
    private Vector3 centerOffset = new Vector3(0, 0, 0.5f);
    private int currentLevel = 1;
    public int LeftPlayerFinalLevel {get; set;}
    public int RightPlayerFinalLevel {get; set;}

    private class Level
    {
        public Vector3 platformCenter;
        private GoalManager leftGoal;
        private GoalManager rightGoal;
        public float bridgeLength;
        public float distanceBetweenPlatforms;
        public GameObject leftBridge;
        public GameObject rightBridge;   
        public Level(Vector3 platformCenter, GoalManager leftGoal, GoalManager rightGoal, float bridgeLength, float distanceBetweenPlatforms)
        {
            this.platformCenter = platformCenter;
            this.leftGoal = leftGoal;
            this.rightGoal = rightGoal;
            this.bridgeLength = bridgeLength;
            this.distanceBetweenPlatforms = distanceBetweenPlatforms;
        }

        public Vector3 GetGoalPos(bool left)
        {
            if (left)
            {
                if (leftGoal)
                    return leftGoal.GetGoalPosition();
                else
                    return new Vector3(2.2f,2.01f,-2);
            }
            else
            {
                if (rightGoal)
                    return rightGoal.GetGoalPosition();
                else
                    return new Vector3(2.2f,2.01f,2);
            }
        }

        public void ActivateGoal(bool left)
        {
            if (left)
                leftGoal.ActivateGoal();
            else
                rightGoal.ActivateGoal();
        }

        public float GetExtremeX(bool upper)
        {
            if (leftGoal)
                return upper? leftGoal.GetUpperExtremeX() : leftGoal.GetLowerExtremeX();
            else
                return upper? 3.5f : 3.5f;
        }

        public void MoveGoalDownwards(bool left)
        {
            if (left)
            {
                if (leftGoal)
                    leftGoal.MoveGoalDownwards();
            }
            else
            {
                if (rightGoal)
                    rightGoal.MoveGoalDownwards();
            }
        }
    }
    
    private List<Level> levels = new List<Level>();
    private List<GameObject> barriers = new List<GameObject>();

    public (Rigidbody leftBridge, Rigidbody rightBridge) NextLevel(bool leftPlayerDead, bool rightPlayerDead)
    {
        //can create levels well beyond the next level (depending on Init()) but still starts the next level
        CreateNextLevel(levels[levels.Count-1].platformCenter, levels[levels.Count-1].GetGoalPos(left: true), levels.Count);
        currentLevel++;
        return StartLevel(currentLevel, leftPlayerDead, rightPlayerDead);
    }

    public (Rigidbody leftBridge, Rigidbody rightBridge) Init()
    {
        for (int i = 0; i < 5; i++)
        {
            barriers.Add(Instantiate(barrier, new Vector3(0,0,0), Quaternion.identity));
        }
        //add first platform to make levels.Count and currentLevel match in upcoming method calls
        levels.Add(new Level(new Vector3(2,0,0), null, null, 0, 0)); 
        for (int i = 0; i < 20; i++)
            CreateNextLevel(levels[i].platformCenter, levels[i].GetGoalPos(left: true), levels.Count);

        return StartLevel(currentLevel, leftPlayerDead: false, rightPlayerDead: false);
    }

    private (Rigidbody leftBridge, Rigidbody rightBridge) StartLevel(int lvl, bool leftPlayerDead, bool rightPlayerDead)
    {
        Vector3 leftRightOffset = new Vector3(0, 0, 2.5f);
        UpdateBarriers(lvl, leftRightOffset);
        
        float bridgeWidth = 0.4f;
        float spawnOffset = platform1.transform.localScale.x/2 - levels[lvl].bridgeLength/2;
        Vector3 newBridgePosition = new Vector3(levels[lvl-1].platformCenter.x, levels[lvl-1].platformCenter.y, 0);
        Vector3 leftBridgeOffset = new Vector3(spawnOffset, distanceBridgeToPlatformCenter, -5/2 + bridgeWidth/2) - leftRightOffset + centerOffset;
        Vector3 rightBridgeOffset = new Vector3(spawnOffset, distanceBridgeToPlatformCenter, +5/2 - bridgeWidth/2) + leftRightOffset - centerOffset;

        if (!leftPlayerDead)
        {
            levels[lvl].ActivateGoal(left: true);
            levels[lvl].leftBridge = Instantiate(bridge, newBridgePosition + leftBridgeOffset, Quaternion.identity);
            levels[lvl].leftBridge.gameObject.transform.localScale = new Vector3(levels[lvl].bridgeLength, bridge.transform.localScale.y, bridgeWidth);
            leftCrane.MoveIntoScene(true, levels[lvl].leftBridge);
        }
        if (!rightPlayerDead)
        {
            levels[lvl].ActivateGoal(left: false);
            levels[lvl].rightBridge = Instantiate(bridge, newBridgePosition + rightBridgeOffset, Quaternion.identity);
            levels[lvl].rightBridge.gameObject.transform.localScale = new Vector3(levels[lvl].bridgeLength, 0.01f, bridgeWidth);
            rightCrane.MoveIntoScene(false, levels[lvl].rightBridge);
        }

        ground.transform.position = new Vector3(ground.transform.position.x - levels[lvl].distanceBetweenPlatforms,
                ground.transform.position.y, ground.transform.position.z);

        //if necessary create placeholders to prevent nullpointer errors
        if (!levels[lvl].leftBridge)
        {
            levels[lvl].leftBridge = new GameObject();
            levels[lvl].leftBridge.AddComponent<Rigidbody>();
        }
        if (!levels[lvl].rightBridge)
        {
            levels[lvl].rightBridge = new GameObject();
            levels[lvl].rightBridge.AddComponent<Rigidbody>();
        }

        return (levels[lvl].leftBridge.GetComponent<Rigidbody>(), levels[lvl].rightBridge.GetComponent<Rigidbody>());
    }

    private void UpdateBarriers(int lvl, Vector3 leftRightOffset)
    {
        float upperX = levels[lvl].GetExtremeX(upper: true) - 0.5f;
        float lowerX = levels[lvl-1].GetExtremeX(upper: false) + 0.5f;
        Vector3 newPlatformCenter = levels[lvl].platformCenter;
        Vector3 upperLeft = new Vector3(upperX, newPlatformCenter.y + distanceBridgeToPlatformCenter, -5);
        Vector3 upperRight = new Vector3(upperX, newPlatformCenter.y + distanceBridgeToPlatformCenter, 5);
        Vector3 lowerLeft = new Vector3(lowerX, newPlatformCenter.y + distanceBridgeToPlatformCenter, -5);
        Vector3 lowerRight = new Vector3(lowerX, newPlatformCenter.y + distanceBridgeToPlatformCenter, 5);
        Vector3 upperCenter = Vector3.Lerp(upperLeft, upperRight, 0.5f);
        Vector3 lowerCenter = Vector3.Lerp(lowerLeft, lowerRight, 0.5f);
        Vector3 leftCenter = Vector3.Lerp(lowerLeft, upperLeft, 0.5f);
        Vector3 rightCenter = Vector3.Lerp(lowerRight, upperRight, 0.5f);
        Vector3 center = Vector3.Lerp(lowerCenter, upperCenter, 0.5f);
        float verticalDistance = (upperLeft - lowerLeft).magnitude;
        float horizontalDistance = (upperLeft - upperRight).magnitude;
        
        //top barrier
        barriers[0].transform.position = upperCenter;
        barriers[0].transform.localScale = new Vector3(0, 2, horizontalDistance);
        //right barrier
        barriers[1].transform.position = rightCenter;
        barriers[1].transform.localScale = new Vector3(verticalDistance, 2, 0);
        //left barrier
        barriers[2].transform.position = leftCenter;
        barriers[2].transform.localScale = new Vector3(verticalDistance, 2, 0);
        //lower barrier
        barriers[3].transform.position = lowerCenter;
        barriers[3].transform.localScale = new Vector3(0, 2, horizontalDistance);
        //central barrier
        barriers[4].transform.position = center;
        barriers[4].transform.localScale = new Vector3(verticalDistance, 2, 0);

        foreach (GameObject barrier in barriers)
        {
            barrier.SetActive(false);
        }
    }

    private void CreateNextLevel(Vector3 oldPlatformCenter, Vector3 oldLeftGoal, int level)
    {
        float nextCenterDistance = Random.Range(minCenterDistance, maxCenterDistance);
        float nextPlatformRotation = Random.Range(-maxRotation, maxRotation);
        Vector3 newPlatformCenter = new Vector3(oldPlatformCenter.x - nextCenterDistance, oldPlatformCenter.y, oldPlatformCenter.z);
        Vector3 leftRightOffset = new Vector3(0, 0, platform1.transform.localScale.z/2);
        int randomness = Random.Range(0,5);
        GameObject leftPlatform;
        switch(randomness)
        {
            case 1:
                leftPlatform = Instantiate(platform1, newPlatformCenter - leftRightOffset, Quaternion.Euler(0, -nextPlatformRotation, 0));
                break;
            case 2:
                leftPlatform = Instantiate(platform2, newPlatformCenter - leftRightOffset, Quaternion.Euler(0, -nextPlatformRotation, 0));
                break;
            case 3:
                leftPlatform = Instantiate(platform3, newPlatformCenter - leftRightOffset, Quaternion.Euler(0, -nextPlatformRotation, 0));
                break;
            case 4:
                leftPlatform = Instantiate(platform4, newPlatformCenter - leftRightOffset, Quaternion.Euler(0, -nextPlatformRotation, 0));
                break;
            default:
                leftPlatform = Instantiate(platform0, newPlatformCenter - leftRightOffset, Quaternion.Euler(0, -nextPlatformRotation, 0));
                break;
        }
        randomness = Random.Range(0,5);
        GameObject rightPlatform;
        switch(randomness)
        {
            case 1:
                rightPlatform = Instantiate(platform1, newPlatformCenter + leftRightOffset, Quaternion.Euler(0, 180.0f + nextPlatformRotation, 0));
                break;
            case 2:
                rightPlatform = Instantiate(platform2, newPlatformCenter + leftRightOffset, Quaternion.Euler(0, 180.0f + nextPlatformRotation, 0));
                break;
            case 3:
                rightPlatform = Instantiate(platform3, newPlatformCenter + leftRightOffset, Quaternion.Euler(0, 180.0f + nextPlatformRotation, 0));
                break;
            case 4:
                rightPlatform = Instantiate(platform4, newPlatformCenter + leftRightOffset, Quaternion.Euler(0, 180.0f + nextPlatformRotation, 0));
                break;
            default:
                rightPlatform = Instantiate(platform0, newPlatformCenter + leftRightOffset, Quaternion.Euler(0, 180.0f + nextPlatformRotation, 0));
                break;
        }
        float newGoalOffset = Random.Range(-0.5f, 1.5f);
        GoalManager leftGoalManager = leftPlatform.GetComponent<GoalManager>();
        GoalManager rightGoalManager = rightPlatform.GetComponent<GoalManager>();
        leftGoalManager.MoveGoal(newGoalOffset);
        rightGoalManager.MoveGoal(newGoalOffset);
        float additionalBridgeLength = 1.0f / (level + 6.0f) * 6.0f - 0.3f;
        float bridgeLength = (oldLeftGoal - leftGoalManager.GetGoalPosition()).magnitude + additionalBridgeLength - 1;

        levels.Add(new Level(newPlatformCenter, leftGoalManager, rightGoalManager, bridgeLength, nextCenterDistance - platform1.transform.localScale.x));
    }

    public (Vector3 leftGoal, Vector3 rightGoal) GetCurrentGoals()
    {
        return (levels[currentLevel].GetGoalPos(left: true), levels[currentLevel].GetGoalPos(left: false));
    }

    public (Vector3 leftGoal, Vector3 rightGoal) GetPreviousGoals()
    {
        return (levels[currentLevel-1].GetGoalPos(left: true), levels[currentLevel-1].GetGoalPos(left: false));
    }

    public (GameObject leftBridge, GameObject rightBridge) GetCurrentBridges()
    {
        return (levels[currentLevel].leftBridge, levels[currentLevel].rightBridge);
    }

    public float DistanceBetweenCurrentPlatforms()
    {
        return Mathf.Abs(levels[currentLevel].platformCenter.x - levels[currentLevel-1].platformCenter.x);
    }

    public int GetCurrentLevel()
    {
        return currentLevel;
    }

    public void ActivateBarriers()
    {
        foreach (GameObject barrier in barriers)
        {
            barrier.SetActive(true);
        }
    }

    public void MoveGoalDownwards(bool left)
    {
        levels[currentLevel].MoveGoalDownwards(left);
    }
}