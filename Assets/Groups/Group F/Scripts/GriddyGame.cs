using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.InputSystem;

public class GriddyGame : MiniGame {

    public GameObject player1;
    public GameObject player2;
    public GameObject player3;
    public GameObject player4;

    public GameObject pEnliS;

    public int length1;
    public int length2;

    public int xoffset;
    public int yoffset;
    public int zoffset;

    public int height;

    public int gridoffset;

    public float decaySpeed = 1f;//0.2f;


    private float death_depth;

    private bool gameEnded = false;
    private List<GameObject> platforms = new List<GameObject>();
    private List<GameObject> dead_players = new List<GameObject>();
    private List<GameObject> players = new List<GameObject>();
    private Dictionary<GameObject, AiState> aiStates = new Dictionary<GameObject, AiState>();

    private float gameStart = 0.0f;

    public override string getDisplayName() {
        return "Griddy Battle";
    }
    public override string getSceneName() {
        return "GriddyBattle";
    }

    public override MiniGameType getMiniGameType() {
        return MiniGameType.freeForAll;
    }

    private GameObject CreatePlatform(int x, int z, int h)
    {
        var platform = Instantiate(pEnliS, position: new Vector3(x * xoffset - gridoffset, -1 * yoffset * h, z * zoffset - gridoffset), rotation: transform.rotation);
        platforms.Add(platform);

        var playerDetection = platform.GetComponent<playerDetection>();
        playerDetection.decaySpeed = decaySpeed;

        return platform;
    }
    private void Start() {
        gameStart = Time.time;
        death_depth = -4 * height - 10;

        Debug.Log($"death_depth = {death_depth}");

        platforms.Clear();
        for (int x = 0; x < length1; x++) {
            for (int z = 0; z < length2; z++) {
                for (int curh = 0; curh < height; curh++) {
                    var platform = CreatePlatform(x, z, curh);
                    platforms.Add(platform);
                }
            }
        }

        //Create list of player inputs from the players in the scene
        players = new List<GameObject> {
            player1,
            player2,
            player3,
            player4
        };

        
        var playerInputs = players.Select(p => p.GetComponent<PlayerInput>()).ToList();

        //This assigns the player input in the order they were given in the array
        InputManager.Instance.AssignPlayerInput(playerInputs);

    }

    private List<GameObject> CheckGameover()
    {
        players
            .Where(p => !dead_players.Contains(p))
            .Where(p => p.transform.position.y < death_depth)
            .ToList()
            .ForEach(dead_players.Add);


        if (dead_players.Count() >= 4)
        {
            GameOver();
        }

        return dead_players.ToList();
    }


    private int GameObject2Int(GameObject obj) => players.IndexOf(obj) + 1;

    private void GameOver()
    {
        if (gameEnded)
        {
            return;
        }
        Debug.Log("GAME END");
        gameEnded = true;

        //Create array of positions with player ids, this also works in case there are multiple players in one position
        int[] first = { GameObject2Int(dead_players[3]) };
        int[] second = { GameObject2Int(dead_players[2]) };
        int[] third = { GameObject2Int(dead_players[1]) };
        int[] fourth = { GameObject2Int(dead_players[0]) };
        Debug.Log($"{first.First()} > {second.First()} > {third.First()} > {fourth.First()}");

        //Note this is still work in progress, but ideally you will use it like this
        MiniGameFinished(
            firstPlace: first,
            secondPlace: second,
            thirdPlace: third,
            fourthPlace: fourth
        );
    }

    void Update()
    {
        var timeDelta = Time.time - gameStart;

        platforms = platforms.Where(p => p != null).ToList();

        CheckGameover();
        if (timeDelta < 5) {
            return;
        }
        foreach (var aiPlayer in GetAiPlayers())
        {
            // determine platforms which have the most health points
            if (aiStates.TryGetValue(aiPlayer, out var lastState)) {
                var goalDistance = Vector2.Distance
                (
                    new Vector2(lastState.GoalPosition.x, lastState.GoalPosition.z),
                    new Vector2(aiPlayer.transform.position.x, aiPlayer.transform.position.z)
                );
                var currDistance = Vector2.Distance
                (
                    new Vector2(lastState.CurrentPosition.x, lastState.CurrentPosition.z),
                    new Vector2(aiPlayer.transform.position.x, aiPlayer.transform.position.z)
                );
                if (currDistance / Time.deltaTime > 2.0 && goalDistance > 0.25 && 2.0 > Math.Abs(lastState.CurrentPosition.y - aiPlayer.transform.position.y))
                    continue;
            }

            float DistanceToPlayer(GameObject p) =>
                Math.Max(
                    Math.Abs(p.transform.position.y - aiPlayer.transform.position.y),
                    Math.Max(
                        Math.Abs(p.transform.position.x - aiPlayer.transform.position.x),
                        Math.Abs(p.transform.position.z - aiPlayer.transform.position.z)
                    )
                );

            var neighboringPlatforms = platforms
                //.Where(p => DistanceToPlayer(p) < 3.25f)
                .Where(p => DistanceToPlayer(p) < 4f)
                .ToList()
                .Shuffle();

            var goalCandidates = neighboringPlatforms
                .OrderBy(p => p.GetComponent<playerDetection>().decay)
                .Take((int)Math.Ceiling(neighboringPlatforms.Count * 0.25))
                .ToList();

            if (!goalCandidates.Any())
                continue;
            var goalPlatform = goalCandidates[new System.Random().Next() % goalCandidates.Count];
            var goalPosition = new Vector3(
                x: goalPlatform.transform.position.x + (float)(new System.Random().NextDouble() * 0.2 - 0.1),
                y: aiPlayer.transform.position.y,
                z: goalPlatform.transform.position.z + (float)(new System.Random().NextDouble() * 0.2 - 0.1)
            );
            
            aiPlayer.GetComponent<MinifigControllerF>().MoveTo(goalPosition);
            aiStates[aiPlayer] = new AiState {
                GoalPosition = goalPosition,
                CurrentPosition = aiPlayer.transform.position
            };
        }
    }

    private List<GameObject> GetAiPlayers() {
        var aiPlayers = new List<GameObject>();
        if (PlayerPrefs.GetString("Player1_AI").Equals("True"))
            aiPlayers.Add(player1);
        //if (PlayerPrefs.GetString("Player2_AI").Equals("True"))
        aiPlayers.Add(player2);
        //if (PlayerPrefs.GetString("Player3_AI").Equals("True"))
        aiPlayers.Add(player3);
        //if (PlayerPrefs.GetString("Player4_AI").Equals("True"))
        aiPlayers.Add(player4);

        return aiPlayers;
    }
}