
using System.Collections.Generic;

public static class GameList
{
    public static List<string> GAMES = new List<string> {"Free for All","One vs Three","Two vs Two"};

    //Board Group: change the name of the scene if required
    public const string MAIN_BOARD_SCENE = "Boardgame";

    public static List<MiniGame> FREE_FOR_ALL_LIST = new List<MiniGame>{
        new GriddyGame(),
        new MiniGame_Meteorfall(),
        new SmashGameR(),
        new RhythmGame(),        
        new KartRacingGame(),
        new TrafficTrouble(),
        new PlattiGame(),
        new Groups.Group_S.PartKartMiniGame(),
        new WhatTheHillGame()
    };

    public static List<MiniGame> SINGLE_VS_TEAM_LIST = new List<MiniGame>{
        new GameController_G(),
        new CannonStandoff()
    };

    public static List<MiniGame> TEAM_VS_TEAM_LIST = new List<MiniGame>{
        new CrossTheCanyons(),
        new LegoPaperScissorsMinigame(),
        new GameManagerJ()
    };

    //For Board Game Testing Purposes
    public static List<MiniGame> TESTING_LIST = new List<MiniGame>{
        new TestingGame(),
        new TestingGame(),
        new TestingGame()

    };

}



