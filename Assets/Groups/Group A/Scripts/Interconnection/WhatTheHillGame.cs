using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WhatTheHillGame : MiniGame
{
    public List<GameObject> Players;
    public bool myGameEndingCondition = false;


    public override string getDisplayName()
    {
        return "What The Hill";
    }

    public override MiniGameType getMiniGameType()
    {
        return MiniGameType.freeForAll;
    }

    public override string getSceneName()
    {
        return "WhatTheHill";
    }

    private void Start()
    {
        //Create list of player inputs from the players in the scene
        var playerInputs = new List<PlayerInput>();
        int i = 1;
        foreach (GameObject player in Players)
        {
            bool isAi = PlayerPrefs.GetString("PLAYER" + i + "_AI").Equals("True");
            Players[i - 1].GetComponent<MinifigControllerWTH>().activateAI(isAi);
            if (!isAi)
            {
                InputManager.Instance.ApplyPlayerCustomization(player, i);
            }

            playerInputs.Add(player.GetComponent<PlayerInput>());

            i++;
        }
        //Players[0].GetComponent<MinifigControllerWTH>().activateAI(true);
        //Players[1].GetComponent<MinifigControllerWTH>().activateAI(true);
        //Players[2].GetComponent<MinifigControllerWTH>().activateAI(true);
        //Players[3].GetComponent<MinifigControllerWTH>().activateAI(true);

        //This assigns the player input in the order they were given in the array
        InputManager.Instance.AssignPlayerInput(playerInputs);



    }

    void Update()
    {
        //If your game has already ended you can call the MiniGameFinished method
        if (myGameEndingCondition == true)
        {
            List<Tuple<int, int>> placements = new List<Tuple<int, int>>();

            //Create array of positions with player ids, this also works in case there are multiple players in one position
            for (int i = 0; i < Players.Count; i++)
            {
                MinifigControllerWTH controller = Players[i].GetComponent<MinifigControllerWTH>();
                placements.Add(new Tuple<int, int>(i, controller.playerPoints));
            }
            placements.Sort((tuple1, tuple2) =>
            {
                if (tuple1.Item2 > tuple2.Item2)
                {
                    return 1;
                }
                else if (tuple1.Item2 == tuple2.Item2)
                {
                    return 0;
                }
                else
                {
                    return -1;
                }
            });
            List<List<int>> results = new List<List<int>>();
            int lastValue = 0;
            placements.ForEach(tuple => {
                if (results.Count != 0 && lastValue == tuple.Item2 )
                {
                    results[results.Count - 1].Add(tuple.Item1);
                } else
                {
                    results.Add(new List<int>() { tuple.Item1 });
                    lastValue = tuple.Item2;
                }

            });
            while (results.Count < 4)
            {
                results.Add(new List<int>());
            }

            Debug.Log($"Erster {results[0].ToArray()}, Zweiter {results[1].ToArray()}, Dritter {results[2].ToArray()}, Vierter {results[3].ToArray()}");
            //Note this is still work in progress, but ideally you will use it like this
            MiniGameFinished(firstPlace: results[0].ToArray(), secondPlace: results[1].ToArray(), thirdPlace: results[2].ToArray(), fourthPlace: results[3].ToArray());
        }
        myGameEndingCondition = false;
    }
}
