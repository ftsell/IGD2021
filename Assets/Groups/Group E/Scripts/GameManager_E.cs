using UnityEngine;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameManager_E : MonoBehaviour
{
    public int totalWinners = 0;
    public List<Transform> carTransformList;
    public List<Transform> carPositionList;
    public Transform Checkpoints;

    public int firstPlace;
    public int secondPlace;
    public int thirdPlace;
    public int fourthPlace;

    public static GameManager_E Instance;

    public void Awake()
    {
        Instance = this;
    }

    public void countRound(Transform player)
    {
        PlayerStats thePlayer = player.GetComponent<PlayerStats>();
        thePlayer.CountRound();

        this.setPlaces(thePlayer);
        //player.GetComponent<CarController>().enabled = false;  
    }

    // Sets Place if there are not yet set
    private void setPlaces(PlayerStats thePlayer)
    {
        if (thePlayer.rounds == 4)
        {
            if (firstPlace == 0)
            {
                firstPlace = thePlayer.playerNumber;
                totalWinners += 1;
                this.startCounter();
            } else if(secondPlace == 0)
            {
                secondPlace = thePlayer.playerNumber;
                totalWinners += 1;
            } else if(thirdPlace == 0)
            {
                thirdPlace = thePlayer.playerNumber;
                totalWinners += 1;
            } else if(fourthPlace == 0)
            {
                fourthPlace = thePlayer.playerNumber;
                totalWinners += 1;
            }
        }
    }

    private void startCounter()
    {
        //Debug.Log("Game finished!");
        TimerCountdown.Instance.startTimer();
    }

    public Transform GetPlayerByPosition(int position)
    {
        // Snapshot of car transform list
        List<Transform> carList = new List<Transform>(carTransformList);

        foreach (Transform car in carList)
        {
            PlayerStats thePlayer = car.GetComponent<PlayerStats>();

            if(thePlayer.GetKartPosition(carList) == position)
            {
                return car;
            }
        }
        
        // Should not be reachable;
        throw new System.Exception("Position Error: The position " + position + " does not exist.");
    }

    public int GetPositionByPlayer(Transform carTransform)
    {
        // Snapshot of car transform list
        List<Transform> carList = new List<Transform>(carTransformList);

        PlayerStats thePlayer = carTransform.GetComponent<PlayerStats>();
        try
        {
            return thePlayer.GetKartPosition(carList);
        }
        catch (System.Exception)
        {
            throw new System.Exception("Position Error: Could not find position of player: " + carTransform.ToString());
        }

    }

    private void Start()
    {
        totalWinners = 0;
        carPositionList = new List<Transform>();
        setKartColors();
    }

    public void setKartColors()
    {
        if (PlayerPrefs.GetString("PLAYER1_NAME") != null)
        {
            setKartColor(carTransformList[0], mapColorValue(PlayerPrefs.GetString("PLAYER1_NAME")));
        } else
        {
            setKartColor(carTransformList[0], Color.yellow);
        }
        if (PlayerPrefs.GetString("PLAYER2_NAME") != null)
        {
            setKartColor(carTransformList[1], mapColorValue(PlayerPrefs.GetString("PLAYER2_NAME")));
        }
        else
        {
            setKartColor(carTransformList[1], Color.white);
        }
        if (PlayerPrefs.GetString("PLAYER3_NAME") != null)
        {
            setKartColor(carTransformList[2], mapColorValue(PlayerPrefs.GetString("PLAYER3_NAME")));
        } 
        else
        {
            setKartColor(carTransformList[2], Color.gray);
        }
        if (PlayerPrefs.GetString("PLAYER4_NAME") != null)
        {
            setKartColor(carTransformList[3], mapColorValue(PlayerPrefs.GetString("PLAYER4_NAME")));
        }
        else
        {
            setKartColor(carTransformList[3], Color.cyan);
        }
    }

    private Color mapColorValue(string color)
    {
        if (color.ToUpper().Equals("RED"))
        {
            return Color.red;
        }
        if (color.ToUpper().Equals("GREEN"))
        {
            return Color.green;
        }
        if (color.ToUpper().Equals("BLUE"))
        {
            return Color.blue;
        }
        if (color.ToUpper().Equals("PINK"))
        {
            return Color.magenta;
        } else
        {
            return Color.white;
        }
    }

    public void setKartColor(Transform kartTransform, Color color)
    {
        Transform kartMesh = kartTransform.Find("Model").Find("kart_color_v2").Find("Main");
        var block = new MaterialPropertyBlock();
        block.SetColor("_BaseColor", color);

        kartMesh.Find("front").GetComponent<MeshRenderer>().SetPropertyBlock(block);
        kartMesh.Find("fillerbrick").GetComponent<MeshRenderer>().SetPropertyBlock(block);
        kartMesh.Find("body l").GetComponent<MeshRenderer>().SetPropertyBlock(block);
        kartMesh.Find("body l.001").GetComponent<MeshRenderer>().SetPropertyBlock(block);
        kartMesh.Find("Cylinder").GetComponent<MeshRenderer>().SetPropertyBlock(block);
        kartMesh.Find("side bumper l").Find("Cube.004").GetComponent<MeshRenderer>().SetPropertyBlock(block);
        kartMesh.Find("side bumper r").Find("Cube.003").GetComponent<MeshRenderer>().SetPropertyBlock(block);
        kartMesh.Find("spoiler").GetComponent<MeshRenderer>().SetPropertyBlock(block);
        kartMesh.Find("test 1x2 doorrail.002").GetComponent<MeshRenderer>().SetPropertyBlock(block);
        kartMesh.Find("test 1x2 doorrail.002").Find("test 1x2 plate.001").GetComponent<MeshRenderer>().SetPropertyBlock(block);
        kartMesh.Find("test 1x2 doorrail.002").Find("test 1x2 plate.004").GetComponent<MeshRenderer>().SetPropertyBlock(block);
        kartMesh.Find("test 1x2 doorrail.003").GetComponent<MeshRenderer>().SetPropertyBlock(block);
        kartMesh.Find("test 1x2 doorrail.003").Find("test 1x2 plate.005").GetComponent<MeshRenderer>().SetPropertyBlock(block);
        kartMesh.Find("test 1x2 doorrail.003").Find("test 1x2 plate.006").GetComponent<MeshRenderer>().SetPropertyBlock(block);
        kartMesh.Find("test 1x2 plate.010").GetComponent<MeshRenderer>().SetPropertyBlock(block);
    }

    public void CreateAI()
    {
        if (PlayerPrefs.GetString("PLAYER1_AI").Equals("True"))
        {
            InitializeAIPlayer(carTransformList[0]);
        }
        if (PlayerPrefs.GetString("PLAYER2_AI").Equals("True"))
        {
            InitializeAIPlayer(carTransformList[1]);
        }
        if (PlayerPrefs.GetString("PLAYER3_AI").Equals("True"))
        {
            InitializeAIPlayer(carTransformList[2]);
        }
        if (PlayerPrefs.GetString("PLAYER4_AI").Equals("True"))
        {
            InitializeAIPlayer(carTransformList[3]);
        }
    }

    public void finishGame()
    {
         KartRacingGame.Instance.finishGame();
    }

    private void Update()
    {
        foreach (Transform car in carTransformList)
        {
            PlayerStats thePlayer = car.GetComponent<PlayerStats>();
            thePlayer.GetKartPosition(carTransformList);

            // some error
            //Debug.Log(thePlayer.GetKartPosition(carTransformList));
            //carPositionList[(thePlayer.GetKartPosition(carTransformList) - 1)] = car;
        }

        //finish game if the players end all rounds
        //if(totalWinners == 4)
        //{
        //    this.finishGame();
        //}
    }

    private void InitializeAIPlayer(Transform car)
    {
        NavMeshAgent agent = car.gameObject.AddComponent(typeof(NavMeshAgent)) as NavMeshAgent;
        agent.speed = 21;
        agent.acceleration = 13;
        agent.stoppingDistance = 10;

        NavAgentScript_E agentScript = car.gameObject.AddComponent(typeof(NavAgentScript_E)) as NavAgentScript_E;
        agentScript.Checkpoints = Checkpoints;
    }
}
