using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public enum MiniGameType {
    freeForAll,
    singleVsTeam,
    teamVsTeam
}

abstract public class MiniGame : MonoBehaviour
{
    public abstract string getDisplayName();
    public abstract string getSceneName();

    public abstract MiniGameType getMiniGameType();

    //Prefab that shows Game Over and the scores form the players at the end
    //Please use the Prefab from Group C - Interconnections/Prefabs/FinishGames
    public GameObject _finishGamePrefab;

    //Optional to use, set ups the inputs and customization form players considering the whole game
    //SHOULD ONLY BE CALLED WHEN THE WHOLE GAME IS BEING TESTED (Character selection + BoardGame)
    public void InitializePlayers(List<GameObject> players, List<int> playerIds) {

        for (int i = 0; i < players.Count; i++)
        {
            //Character customization
            InputManager.Instance.ApplyPlayerCustomization(players[i], playerIds[i]);

            //Assign saved player control scheme
            var playerInput = players[i].GetComponent<PlayerInput>();
            InputManager.Instance.AssignPlayerInput(playerInput, playerIds[i]);
        }

    }

    //We should reference a template scene which will present instrucitons with a simple image and a Text box to fill with the instructions
    public void LoadInstructions(string instructions, string previewImage) {
        //TODO
    }

    //Saves results in PlayerPrefs
    public void MiniGameFinished(int[] firstPlace, int[] secondPlace, int[] thirdPlace, int[] fourthPlace) {
         StartCoroutine(MiniGameFinishedTransition(firstPlace, secondPlace, thirdPlace, fourthPlace));
    }

    private Color ApplyColor(string color_name){
        if (color_name.Equals("RED")){
            return Color.red;
        }

        if (color_name.Equals("PINK")){
            return Color.magenta;
        }

        if (color_name.Equals("YELLOW")){
            return Color.yellow;
        }

        if (color_name.Equals("BLUE")){
            return Color.blue;
        }

        return Color.black;
    }

    public IEnumerator MiniGameFinishedTransition(int[] firstPlace, int[] secondPlace, int[] thirdPlace, int[] fourthPlace){

        var finishGameBanner = Instantiate(_finishGamePrefab);

        GameObject gameoverbanner = GameObject.Find("GameOverBanner");
        GameObject gamescores = GameObject.Find("GameScores");

        gamescores.SetActive(false);
        yield return new WaitForSeconds(3f);
        gameoverbanner.SetActive(false);
        gamescores.SetActive(true);
        

        //Labels
        Text first_place = GameObject.Find("first_place").GetComponent<Text>();
        Text second_place = GameObject.Find("second_place").GetComponent<Text>();
        Text third_place = GameObject.Find("third_place").GetComponent<Text>();
        Text fourth_place = GameObject.Find("fourth_place").GetComponent<Text>();

        //Names
        Text name1 = GameObject.Find("name1").GetComponent<Text>();
        Text name2 = GameObject.Find("name2").GetComponent<Text>();
        Text name3 = GameObject.Find("name3").GetComponent<Text>();
        Text name4 = GameObject.Find("name4").GetComponent<Text>();

        int ranking_player1 = 0;
        int ranking_player2 = 0;
        int ranking_player3 = 0;
        int ranking_player4 = 0;

        //extracting positions
        for(int i = 0; i<firstPlace.Length; i++){
            switch(firstPlace[i]){
                case 1: ranking_player1 = 1; break;
                case 2: ranking_player2 = 1; break;
                case 3: ranking_player3 = 1; break;
                case 4: ranking_player4 = 1; break;
            }
        }

        for(int i = 0; i<secondPlace.Length; i++){
            switch(secondPlace[i]){
                case 1: ranking_player1 = 2; break;
                case 2: ranking_player2 = 2; break;
                case 3: ranking_player3 = 2; break;
                case 4: ranking_player4 = 2; break;
            }
        }

        for(int i = 0; i<thirdPlace.Length; i++){
            switch(thirdPlace[i]){
                case 1: ranking_player1 = 3; break;
                case 2: ranking_player2 = 3; break;
                case 3: ranking_player3 = 3; break;
                case 4: ranking_player4 = 3; break;
            }
        }

        for(int i = 0; i<fourthPlace.Length; i++){
            switch(fourthPlace[i]){
                case 1: ranking_player1 = 4; break;
                case 2: ranking_player2 = 4; break;
                case 3: ranking_player3 = 4; break;
                case 4: ranking_player4 = 4; break;
            }
        }

        //updating texts
        if (firstPlace.Length > 1){
            if(firstPlace.Length == 2 ){
                if (secondPlace.Length == 2) {
                    second_place.text = "1st";
                    third_place.text = "2nd";
                    fourth_place.text = "2nd";

                    name1.text = PlayerPrefs.GetString("PLAYER"+firstPlace[0].ToString()+"_NAME");
                    name1.color = ApplyColor(PlayerPrefs.GetString("PLAYER"+firstPlace[0].ToString()+"_NAME"));

                    name2.text = PlayerPrefs.GetString("PLAYER"+firstPlace[1].ToString()+"_NAME");
                    name2.color = ApplyColor(PlayerPrefs.GetString("PLAYER"+firstPlace[1].ToString()+"_NAME"));

                    name3.text = PlayerPrefs.GetString("PLAYER"+secondPlace[0].ToString()+"_NAME");
                    name3.color = ApplyColor(PlayerPrefs.GetString("PLAYER"+secondPlace[0].ToString()+"_NAME"));

                    name4.text = PlayerPrefs.GetString("PLAYER"+secondPlace[1].ToString()+"_NAME");
                    name4.color = ApplyColor(PlayerPrefs.GetString("PLAYER"+secondPlace[1].ToString()+"_NAME"));
                } else{
                    second_place.text = "1st";
                    third_place.text = "2nd";
                    fourth_place.text = "3rd";

                    name1.text = PlayerPrefs.GetString("PLAYER"+firstPlace[0].ToString()+"_NAME");
                    name1.color = ApplyColor(PlayerPrefs.GetString("PLAYER"+firstPlace[0].ToString()+"_NAME"));

                    name2.text = PlayerPrefs.GetString("PLAYER"+firstPlace[1].ToString()+"_NAME");
                    name2.color = ApplyColor(PlayerPrefs.GetString("PLAYER"+firstPlace[1].ToString()+"_NAME"));

                    name3.text = PlayerPrefs.GetString("PLAYER"+secondPlace[0].ToString()+"_NAME");
                    name3.color = ApplyColor(PlayerPrefs.GetString("PLAYER"+secondPlace[0].ToString()+"_NAME"));

                    name4.text = PlayerPrefs.GetString("PLAYER"+thirdPlace[0].ToString()+"_NAME");
                    name4.color = ApplyColor(PlayerPrefs.GetString("PLAYER"+thirdPlace[0].ToString()+"_NAME"));
                }
                
            }

            if(firstPlace.Length == 4 ) {
                    second_place.text = "1st";
                    third_place.text = "1st";
                    fourth_place.text = "1st";

                    name1.text = PlayerPrefs.GetString("PLAYER"+firstPlace[0].ToString()+"_NAME");
                    name1.color = ApplyColor(PlayerPrefs.GetString("PLAYER"+firstPlace[0].ToString()+"_NAME"));
                    name2.text = PlayerPrefs.GetString("PLAYER"+firstPlace[1].ToString()+"_NAME");
                    name2.color = ApplyColor(PlayerPrefs.GetString("PLAYER"+firstPlace[1].ToString()+"_NAME"));
                    name3.text = PlayerPrefs.GetString("PLAYER"+firstPlace[2].ToString()+"_NAME");
                    name3.color = ApplyColor(PlayerPrefs.GetString("PLAYER"+firstPlace[2].ToString()+"_NAME"));
                    name4.text = PlayerPrefs.GetString("PLAYER"+firstPlace[3].ToString()+"_NAME");
                    name4.color = ApplyColor(PlayerPrefs.GetString("PLAYER"+firstPlace[3].ToString()+"_NAME"));

            } 

            if (firstPlace.Length == 3) {
                    second_place.text = "1st";
                    third_place.text = "1st";
                    fourth_place.text = "2nd";

                    name1.text = PlayerPrefs.GetString("PLAYER"+firstPlace[0].ToString()+"_NAME");
                    name1.color = ApplyColor(PlayerPrefs.GetString("PLAYER"+firstPlace[0].ToString()+"_NAME"));
                    name2.text = PlayerPrefs.GetString("PLAYER"+firstPlace[1].ToString()+"_NAME");
                    name2.color = ApplyColor(PlayerPrefs.GetString("PLAYER"+firstPlace[1].ToString()+"_NAME"));
                    name3.text = PlayerPrefs.GetString("PLAYER"+firstPlace[2].ToString()+"_NAME");
                    name3.color = ApplyColor(PlayerPrefs.GetString("PLAYER"+firstPlace[2].ToString()+"_NAME"));
                    name4.text = PlayerPrefs.GetString("PLAYER"+secondPlace[0].ToString()+"_NAME");
                    name4.color = ApplyColor(PlayerPrefs.GetString("PLAYER"+secondPlace[0].ToString()+"_NAME"));
            }
        } 

        if(secondPlace.Length > 1) {
            if (firstPlace.Length == 1 && secondPlace.Length ==2 ) {
                third_place.text = "2nd";
                fourth_place.text = "3rd";

                name1.text = PlayerPrefs.GetString("PLAYER"+firstPlace[0].ToString()+"_NAME");
                name1.color = ApplyColor(PlayerPrefs.GetString("PLAYER"+firstPlace[0].ToString()+"_NAME"));

                name2.text = PlayerPrefs.GetString("PLAYER"+secondPlace[0].ToString()+"_NAME");
                name2.color = ApplyColor(PlayerPrefs.GetString("PLAYER"+secondPlace[0].ToString()+"_NAME"));

                name3.text = PlayerPrefs.GetString("PLAYER"+secondPlace[1].ToString()+"_NAME");
                name3.color = ApplyColor(PlayerPrefs.GetString("PLAYER"+secondPlace[1].ToString()+"_NAME"));

                name4.text = PlayerPrefs.GetString("PLAYER"+thirdPlace[0].ToString()+"_NAME");
                name4.color = ApplyColor(PlayerPrefs.GetString("PLAYER"+thirdPlace[0].ToString()+"_NAME"));
            }

            if (secondPlace.Length == 3){
                third_place.text = "2nd";
                fourth_place.text = "2nd";

                name1.text = PlayerPrefs.GetString("PLAYER"+firstPlace[0].ToString()+"_NAME");
                name1.color = ApplyColor(PlayerPrefs.GetString("PLAYER"+firstPlace[0].ToString()+"_NAME"));

                name2.text = PlayerPrefs.GetString("PLAYER"+secondPlace[0].ToString()+"_NAME");
                name2.color = ApplyColor(PlayerPrefs.GetString("PLAYER"+secondPlace[0].ToString()+"_NAME"));

                name3.text = PlayerPrefs.GetString("PLAYER"+secondPlace[1].ToString()+"_NAME");
                name3.color = ApplyColor(PlayerPrefs.GetString("PLAYER"+secondPlace[1].ToString()+"_NAME"));

                name4.text = PlayerPrefs.GetString("PLAYER"+secondPlace[2].ToString()+"_NAME");
                name4.color = ApplyColor(PlayerPrefs.GetString("PLAYER"+secondPlace[2].ToString()+"_NAME"));
            }      
        }

        if(thirdPlace.Length > 1) {;
            fourth_place.text = "3rd";

                name1.text = PlayerPrefs.GetString("PLAYER"+firstPlace[0].ToString()+"_NAME");
                name1.color = ApplyColor(PlayerPrefs.GetString("PLAYER"+firstPlace[0].ToString()+"_NAME"));

                name2.text = PlayerPrefs.GetString("PLAYER"+secondPlace[0].ToString()+"_NAME");
                name2.color = ApplyColor(PlayerPrefs.GetString("PLAYER"+secondPlace[0].ToString()+"_NAME"));

                name3.text = PlayerPrefs.GetString("PLAYER"+thirdPlace[0].ToString()+"_NAME");
                name3.color = ApplyColor(PlayerPrefs.GetString("PLAYER"+thirdPlace[0].ToString()+"_NAME"));

                name4.text = PlayerPrefs.GetString("PLAYER"+thirdPlace[1].ToString()+"_NAME");
                name4.color = ApplyColor(PlayerPrefs.GetString("PLAYER"+thirdPlace[1].ToString()+"_NAME"));
        }
        
        if(thirdPlace.Length == 1 && secondPlace.Length == 1 && firstPlace.Length == 1 && fourthPlace.Length == 1)
        {
                name1.text = PlayerPrefs.GetString("PLAYER"+firstPlace[0].ToString()+"_NAME");
                name1.color = ApplyColor(PlayerPrefs.GetString("PLAYER"+firstPlace[0].ToString()+"_NAME"));
                name2.text = PlayerPrefs.GetString("PLAYER"+secondPlace[0].ToString()+"_NAME");
                name2.color = ApplyColor(PlayerPrefs.GetString("PLAYER"+secondPlace[0].ToString()+"_NAME"));
                name3.text = PlayerPrefs.GetString("PLAYER"+thirdPlace[0].ToString()+"_NAME");
                name3.color = ApplyColor(PlayerPrefs.GetString("PLAYER"+thirdPlace[0].ToString()+"_NAME"));
                name4.text = PlayerPrefs.GetString("PLAYER"+fourthPlace[0].ToString()+"_NAME");
                name4.color = ApplyColor(PlayerPrefs.GetString("PLAYER"+fourthPlace[0].ToString()+"_NAME"));
        }

        

        //Storing ranking
        PlayerPrefs.SetInt("PLAYER1_PLACE", ranking_player1);
        PlayerPrefs.SetInt("PLAYER2_PLACE", ranking_player2);
        PlayerPrefs.SetInt("PLAYER3_PLACE", ranking_player3);
        PlayerPrefs.SetInt("PLAYER4_PLACE", ranking_player4);

        yield return new WaitForSeconds(5f);

        //Back to the MainBoard Game
        LoadingManager.Instance.LoadMainBoardGame();
    }
    
}
