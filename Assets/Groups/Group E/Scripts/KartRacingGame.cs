using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class KartRacingGame : MiniGame
{

    public List<GameObject> players;

    public static KartRacingGame Instance;

    public bool gameFinished = false;

    public void Awake()
    {
        Instance = this;
    }

    public override string getDisplayName(){
        return "Kart Racing";
    }
    public override string getSceneName(){
        return "KartRacingMinigame";
    }

    public override MiniGameType getMiniGameType()
    {
        return MiniGameType.freeForAll;
    }

    private void Start()
    {
        // for local testing
        /**List<PlayerInput> playerInputs = new List<PlayerInput>();
        foreach(GameObject player in players)
        {
            playerInputs.Add(player.GetComponent<PlayerInput>());
        }
        InputManager.Instance.AssignPlayerInput(playerInputs);
    */


        //Create List of corresponding player ids, in this case we only have two players
        //The order of the ids and players should match

        List<int> playerIds = new List<int>();
        List<GameObject> nonAiPlayers = new List<GameObject>();

        if (PlayerPrefs.GetString("PLAYER1_AI").Equals("False"))
        {
            playerIds.Add(1);
            nonAiPlayers.Add(players[0]);
        }

        if (PlayerPrefs.GetString("PLAYER2_AI").Equals("False"))
        {
            playerIds.Add(2);
            nonAiPlayers.Add(players[1]);
        }

        if (PlayerPrefs.GetString("PLAYER3_AI").Equals("False"))
        {
            playerIds.Add(3);
            nonAiPlayers.Add(players[2]);
        }

        if (PlayerPrefs.GetString("PLAYER4_AI").Equals("False"))
        {
            playerIds.Add(4);
            nonAiPlayers.Add(players[3]);
        }


        //Call the provided method from the MiniGame class
        InitializePlayers(nonAiPlayers, playerIds);
    }

    private void Update()
    {
    }

    // Sets places and finishes game
    public void finishGame()
    {
        int[] first = { GameManager_E.Instance.firstPlace };
        int[] second = { GameManager_E.Instance.secondPlace };
        int[] third = { GameManager_E.Instance.thirdPlace };
        int[] fourth = { GameManager_E.Instance.fourthPlace };

        this.disableSplitScreen();

        MiniGameFinished(firstPlace: first, secondPlace: second, thirdPlace: third, fourthPlace: fourth);
    }

    private void disableSplitScreen()
    {
        GameObject.Find("Camera WASD").SetActive(false);
        GameObject.Find("Camera ZGHJ").SetActive(false);
        GameObject.Find("Camera PLÖA").SetActive(false);
        GameObject.Find("Camera Num").SetActive(false);
        GameObject.Find("Main Camera").GetComponent<Camera>().enabled = true;
    }
}