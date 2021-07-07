using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestingGame : MiniGame
{

    public GameObject myPlayer;
    public GameObject secondPlayer;

    public GameObject loadingScreen;

    public override string getDisplayName(){
        return "My Awesome Game!";
    }
    public override string getSceneName(){
        return "MiniGameTesting";
    }

    public override MiniGameType getMiniGameType()
    {
        return MiniGameType.freeForAll;
    }

    void Start()
    {
        /**
         * Examples for different initializations
         */
        //InitializeForLocalTesting();
        InitializeForGlobalTesting();
        InitializeForGlobalTesting_Example_2();

        PlayerPrefs.SetString("PLAYER1_NAME", "Brenda");
        PlayerPrefs.SetString("PLAYER2_NAME", "Jovanna");
        PlayerPrefs.SetString("PLAYER3_NAME", "Myriem");
        PlayerPrefs.SetString("PLAYER4_NAME", "Jose");


        //Different configurations for the final scores and Game Over
        MiniGameFinished(new int []{1,2}, new int []{3,4}, new int []{},new int []{});
        //MiniGameFinished(new int []{3,2,4}, new int []{1}, new int []{},new int []{});
      
        PlayerPrefs.SetString("PLAYER1_NAME", "RED");
        PlayerPrefs.SetString("PLAYER2_NAME", "PINK");
        PlayerPrefs.SetString("PLAYER3_NAME", "YELLOW");
        PlayerPrefs.SetString("PLAYER4_NAME", "BLUE");

        //base.MiniGameFinished(new int []{1,2}, new int []{3,4}, new int []{},new int []{});
        //MiniGameFinished(new int []{3,2,4}, new int []{1}, new int []{},new int []{});
        //base.MiniGameFinished(new int []{2}, new int []{4}, new int []{1},new int []{3});


        Debug.Log(PlayerPrefs.GetInt("PLAYER1_PLACE"));
        Debug.Log(PlayerPrefs.GetInt("PLAYER2_PLACE"));
        Debug.Log(PlayerPrefs.GetInt("PLAYER3_PLACE"));
        Debug.Log(PlayerPrefs.GetInt("PLAYER4_PLACE"));

    }

    //Initialize Player inputs and customization with the provided MiniGame method
    //This should onl be called when the Character selection was used before
    public void InitializeForGlobalTesting()
    {
        //Create list of your players
        List<GameObject> players = new List<GameObject> { myPlayer, secondPlayer };

        //Create List of corresponding player ids, in this case we only have two players
        //The order of the ids and players should match
        List<int> playerIds = new List<int> { 1, 2 };

        //Call the provided method from the MiniGame class
        InitializePlayers(players, playerIds);
    }

    //Instead of using the inherent method, you can also do it yourself like shown here
    public void InitializeForGlobalTesting_Example_2()
    {
        List<string> playerIdsStrings = new List<string> { "1", "2" };
        List<PlayerInput> playerInputs = new List<PlayerInput> { myPlayer.GetComponent<PlayerInput>(), secondPlayer.GetComponent<PlayerInput>() };

        //Inputs
        //You can pass a list of players inputs as well as the list of corresponding ids as strings
        InputManager.Instance.AssignPlayerInput(playerInputs, playerIdsStrings);

        //Customization
        //You pass the player reference, as well as its corresponding id
        InputManager.Instance.ApplyPlayerCustomization(myPlayer, 1);
        InputManager.Instance.ApplyPlayerCustomization(secondPlayer, 2);

    }

    //Example method of how to initialize you character for local testing
    //When you are running only your scene there is nothing saved in PlayerPrefs for input or customization
    //At this point customization is not possible
    public void InitializeForLocalTesting()
    {
        //Create list of player inputs from the players in the scene
        var playerInputs = new List<PlayerInput> { myPlayer.GetComponent<PlayerInput>(), secondPlayer.GetComponent<PlayerInput>() };

        //Use this one when you are testing only your Scene
        InputManager.Instance.AssignPlayerInput(playerInputs);

        //If you decide to run the Customization to test it on your scene, please reference the script CharacterCustomizationMenu, the public void StartGame() menu
        //And then you can use the customization
        //InputManager.Instance.ApplyPlayerCustomization(player_reference, playerId);
    }

}