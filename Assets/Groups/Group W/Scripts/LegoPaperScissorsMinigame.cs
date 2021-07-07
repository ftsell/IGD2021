using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class LegoPaperScissorsMinigame : MiniGame
{
    List<PlayerInput> playerInputs;
    List<PlayerProperties> players;
    public static PhaseHandler.Phase phase;

    public override string getDisplayName() => "LegoPaperScissors";

    public override MiniGameType getMiniGameType() => MiniGameType.teamVsTeam;

    public override string getSceneName() => "LegoPaperScissors";

    public void GetWinners()
    {
        if (phase == PhaseHandler.Phase.End)
        {
            print("end phase!");
            PhaseHandler.Team leadingTeam = PhaseHandler.leadingTeam;

            int[] firstPlaceIndices = players.Select((v, i) => new { v, i })
                    .Where(x => x.v.team == leadingTeam)
                    .Select(x => x.i).ToArray();


            int[] secondPlaceIndices = players.Select((v, i) => new { v, i })
                    .Where(x => x.v.team != leadingTeam)
                    .Select(x => x.i).ToArray();

            // since this is a teamVsTeam game, there are no 3rd/4th places
            MiniGameFinished(firstPlace: firstPlaceIndices, secondPlace: secondPlaceIndices, thirdPlace: new int[] { }, fourthPlace: new int[] { });
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // adds input controls and customizations for non-ai players
        // List<int> playerIds = new List<int>();
        List<int> playerIds = new List<int>() { 1, 2, 3, 4 };

        List<GameObject> playerGameObjects = new List<GameObject>();
        var interconnectionsAi = gameObject.GetComponent<InterconnectionsAi>();
        int i = 1;

        foreach (Transform child in transform)
        {
            var playerName = child.Find("LegoPaperScissors").GetComponent<PlayerProperties>().playerName;
            // print($"player name is {playerName}");

            var IsAiPlayer = interconnectionsAi.IsAiPlayer(playerName);
            // print($"isAiPlayer is: {IsAiPlayer}");
            if (!IsAiPlayer)
            {
                playerGameObjects.Add(child.gameObject);
                playerIds.Add(i);
                // print($"added playerGameObject: {child.gameObject}");
            }

            i++;
        }

        try
        {
            InitializePlayers(playerGameObjects, playerIds);
        }

        catch(Exception e)
        {
            print("not entering via boardgame or customization scene, initializing locally instead (default input and lego schemes)");
        }
      
    }

    void Update()
    {
        phase = PhaseHandler.phase;
        players = PhaseHandler.players;
        GetWinners();
    }
}
