using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGame_Meteorfall : MiniGame
{
    public override string getDisplayName()
    {
        return "Meteorfall";
    }

    public override MiniGameType getMiniGameType()
    {
        return MiniGameType.freeForAll;
    }

    public override string getSceneName()
    {
        return "KMeteorfall";
    }
}
