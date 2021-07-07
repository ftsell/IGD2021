using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficTrouble : MiniGame
{
    public List<GameObject> players;

    private int[] firstPlace = null;
    private int[] secondPlace = null;
    private int[] thirdPlace = null;
    private int[] fourthPlace = null;

    private int[] playerHealth = new int[4];

    void Start()
    {
        //Create list of your playersw

        //Create List of corresponding player ids, in this case we only have two players
        //The order of the ids and players should match
        List<int> playerIds = new List<int> { 1, 2, 3, 4 };

        //Call the provided method from the MiniGame class
        InitializePlayers(players, playerIds);
    }

    override public string getDisplayName()
    {
        return "TrafficTrouble";
    }

    override public string getSceneName()
    {
        return "TrafficTrouble";
    }

    override public MiniGameType getMiniGameType()
    {
        return MiniGameType.freeForAll;
    }

    public void SubmitHealth(int playerID, int health)
    {
        playerHealth[playerID - 1] = health;
        if (health == 0)
        {
            if (fourthPlace == null)
            {
                fourthPlace = new int[] { playerID };
                print("fourth place: " + fourthPlace[0]);
            }
            else if (thirdPlace == null)
            {
                thirdPlace = new int[] { playerID };
                print("third place: " + thirdPlace[0]);
            }
            else if (secondPlace == null)
            {
                secondPlace = new int[] { playerID };
                print("second place: " + secondPlace[0]);
                for (int id = 0; id < 4; id++)
                {
                    if (playerHealth[id] != 0)
                    {
                        firstPlace = new int[] { id + 1 };
                        print("Game Over");
                        print("first place: " + firstPlace[0]);
                        Destroy(GameObject.Find("CarSpawner"));
                        MiniGameFinished(firstPlace, secondPlace, thirdPlace, fourthPlace);
                        break;
                    }
                }
            }
        }
    }

    public void TimeIsOver()
    {
        Destroy(GameObject.Find("CarSpawner"));
        int maxHealth = 0;
        for (int i = 0; i < 4; i++)
        {
            if (playerHealth[i] > maxHealth)
            {
                maxHealth = playerHealth[i];
            }
        }

        print(maxHealth);

        List<int> firstPlaceList = new List<int>();
        for (int id = 0; id < 4; id++)
        {
            if (playerHealth[id] == maxHealth)
            {
                firstPlaceList.Add(id + 1);
            }
        }
        firstPlace = firstPlaceList.ToArray();

        maxHealth -= 1;
        if (firstPlace.Length < 2)
        {
            while (secondPlace == null)
            {
                List<int> secondPlaceList = new List<int>();
                for (int id = 0; id < 4; id++)
                {
                    if (playerHealth[id] == maxHealth)
                    {
                        secondPlaceList.Add(id + 1);
                    }
                }
                if (secondPlaceList.Count > 0)
                {
                    secondPlace = secondPlaceList.ToArray();
                }
                maxHealth -= 1;
            }
        }
        else
        {
            secondPlace = new int[] { };
        }

        if (firstPlace.Length + secondPlace.Length < 3)
        {
            while (thirdPlace == null)
            {
                List<int> thirdPlaceList = new List<int>();
                for (int id = 0; id < 4; id++)
                {
                    if (playerHealth[id] == maxHealth)
                    {
                        thirdPlaceList.Add(id + 1);
                    }
                }
                if (thirdPlaceList.Count > 0)
                {
                    thirdPlace = thirdPlaceList.ToArray();
                }
                maxHealth -= 1;
            }
        }
        else
        {
            thirdPlace = new int[] { };
        }

        List<int> fourthPlaceList = new List<int>();
        for (int id = 0; id < 4; id++)
        {
            if (playerHealth[id] <= maxHealth)
            {
                fourthPlaceList.Add(id + 1);
            }
        }
        fourthPlace = fourthPlaceList.ToArray();

        print("TIME IS OVER!!!");
        print("1." + firstPlace.Length);
        print("2." + secondPlace.Length);
        print("3." + thirdPlace.Length);
        print("4." + fourthPlace.Length);
        MiniGameFinished(firstPlace, secondPlace, thirdPlace, fourthPlace);
    }


}
