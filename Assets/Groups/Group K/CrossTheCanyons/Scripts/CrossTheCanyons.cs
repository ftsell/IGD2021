using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossTheCanyons : MiniGame
{
    public InputManagerCTC inputManagerCTC;
    public void GameOver(int leftPlayerFinalLevel, int rightPlayerFinalLevel)
    {
        int[] firstPlace;
        int[] secondPlace;
        if (leftPlayerFinalLevel > rightPlayerFinalLevel)
        {
            firstPlace = new int[] {1,2};
            secondPlace = new int[] {3,4};
        }
        else if (rightPlayerFinalLevel > leftPlayerFinalLevel)
        {
            firstPlace = new int[] {3,4};
            secondPlace = new int[] {1,2};
        }
        else
        {
            firstPlace = new int[] {1,2,3,4};
            secondPlace = new int[] {};
        }
        MiniGameFinished(firstPlace, secondPlace, new int[] {}, new int[] {});
    }

    public override string getDisplayName()
    {
        return "Cross the Canyons";
    }
    public override string getSceneName()
    {
        return "CrossTheCanyons";
    }

    public override MiniGameType getMiniGameType()
    {
        return MiniGameType.teamVsTeam;
    }

    void Start() 
    {
        inputManagerCTC.InitAIstatus();
    }
}
