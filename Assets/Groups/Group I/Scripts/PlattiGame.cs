using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class PlattiGame : MiniGame
{
    public List<GameObject> players;
    public List<int> finishedPlayers;
    public List<int> remainingPlayers;

    float currentTime = 0f;
    float startingTime = 180f;
    bool gameOver = false;

    public override string getDisplayName()
    {
        return "RunBrickRun";
    }

    public override MiniGameType getMiniGameType()
    {
        return MiniGameType.freeForAll;
    }

    public override string getSceneName()
    {
        return "RunBrickRun";
    }

    void Start()
    {
        // Check if player is AI
        for (int i = 0; i < 4; i++)
        {
            string playerString = "PLAYER" + (i + 1) + "_AI";
            if (PlayerPrefs.GetString(playerString).Equals("True")) {
                players[i].GetComponent<playercollision>().enableAi();
            }
        }

        currentTime = startingTime;

        remainingPlayers = new List<int> { 1, 2, 3, 4 };

        List<int> playerIds = new List<int> { 1, 2, 3, 4 };
        // Comment in for global testing
        InitializePlayers(players, playerIds);
    }

    void Update()
    {
        for (int i = 1; i <= 4; i++)
        {
            if (((playercollision)players[i - 1].GetComponent("playercollision")).hasFinished())
            {
                if (remainingPlayers.Contains(i))
                {
                    finishedPlayers.Add(i);
                    remainingPlayers.Remove(i);
                }
            }
        }

        currentTime -= 1 * Time.deltaTime;

        if (!gameOver && (finishedPlayers.Count == 4 || currentTime <= 0f))
        {
            finishGame();
        }
    }

    private void finishGame()
    {
        gameOver = true;

        List<int[]> winners = new List<int[]>();

        // add players that reached the finish to the winners list
        for (int i = 0; i < finishedPlayers.Count; i++)
        {
            winners.Add(new int[] { finishedPlayers[i] });
        }

        // collect z values for players that did not finish
        Dictionary<int, float> dict = new Dictionary<int, float>();

        for (int i = 0; i < remainingPlayers.Count; i++)
        {
            int playerId = remainingPlayers[i];
            float zValue = ((playercollision)players[playerId - 1].GetComponent("playercollision")).getZPos();

            dict.Add(playerId, zValue);
        }

        // add players that did not finish to the winners list ordered by their z position
        foreach (KeyValuePair<int, float> rPlayer in dict.OrderBy(key => key.Value))
        {
            winners.Add(new int[] { rPlayer.Key } );
        }

        Debug.Log(winners[0][0]);
        Debug.Log(winners[1][0]);
        Debug.Log(winners[2][0]);
        Debug.Log(winners[3][0]);

        int[] firstPlace = winners[0];
        int[] secondPlace = winners[1];
        int[] thirdPlace = winners[2];
        int[] fourthPlace = winners[3];

        MiniGameFinished(firstPlace, secondPlace, thirdPlace, fourthPlace);
    }
}
