using UnityEngine.InputSystem;
using System.Collections.Generic;
using UnityEngine;
public class InputManagerCTC : MonoBehaviour
{
    //left team
    public PlayerInput player1;
    public PlayerInput player2;

    //right team
    public PlayerInput player3;
    public PlayerInput player4;

    private bool player1_AI;
    private bool player2_AI;
    private bool player3_AI;
    private bool player4_AI;

    public void InitAIstatus()
    {
        List<PlayerInput> playerInputs = new List<PlayerInput>()
        {
            player1,
            player2,
            player3,
            player4
        };

        //optional return value is not used
        InputManager.Instance.AssignPlayerInput(playerInputs);
        player1_AI = PlayerPrefs.GetString("PLAYER1_AI").Equals("True");
        player2_AI = PlayerPrefs.GetString("PLAYER2_AI").Equals("True");
        player3_AI = PlayerPrefs.GetString("PLAYER3_AI").Equals("True");
        player4_AI = PlayerPrefs.GetString("PLAYER4_AI").Equals("True");
        Debug.Log("AI status: " + player1_AI + " " + player2_AI + " " + player3_AI + " " + player4_AI);
    }

    /**
    Meaningful playerIDs: 1, 2, 3 and 4
    **/
    public bool IsPlayerBot(int playerID)
    {
        switch(playerID)
        {
            case 1:
                //return true;
                return player1_AI;
            case 2:
                //return true;
                return player2_AI;
            case 3:
                //return true;
                return player3_AI;
            case 4:
                //return true;
                return player4_AI;
            default:
                return false;
        }
    }

}
