using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InterconnectionsAi : MonoBehaviour
{
    public bool IsAiPlayer(string playerName)
    {
        // goes through all players in the order which is also used to get the input,
        // such that we know which player is the first, second and so on
        // then searches for the correct string variable set by the interconnections group

        // players should not be necessary to be searched for every call of this function
        // but somehow this was throwing errors if outsource to Start() and there is no time left to debug, so this will do it
        List<PlayerProperties> players = new List<PlayerProperties>();

        foreach (Transform child in transform)
        {
            players.Add(child.Find("LegoPaperScissors").GetComponent<PlayerProperties>());
        }

            for (int index = 1; index <= 4; index++)
            {
                var player = players[index - 1];

                if (playerName == player.playerName)
                {
                    bool isAi = PlayerPrefs.GetString($"PLAYER{index}_AI").Equals("True");
                    // print($"player at index {index} ({playerName}) isAi? {isAi}");
                    return isAi;
                }
            }

            print($"could not find matching player for name {playerName} while calling IsAiPlayer");
            return false;
       
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
}
