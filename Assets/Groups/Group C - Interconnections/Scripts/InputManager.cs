using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;


public class InputManager : MonoBehaviour
{

    private Controls controls;

    private const string INPUT_DEVICE_PLAYER = "InputDevideIDPlayer";
    private const string CONTROL_SCHEME_PLAYER = "ControlSchemePlayer";

    //AI PLayers keys
    public const string PLAYER_1_AI = "PLAYER1_AI";
    public const string PLAYER_2_AI = "PLAYER2_AI";
    public const string PLAYER_3_AI = "PLAYER3_AI";
    public const string PLAYER_4_AI = "PLAYER4_AI";


    private List<Tuple<int, int, string>> playerSchemes = new List<Tuple<int, int, string>>();

    public List<Color> players_colors = new List<Color> { Color.red, Color.yellow, Color.magenta, Color.blue };
    public List<string> players_colors_names = new List<string> { "RED", "YELLOW", "PINK", "BLUE" };
    public List<int> ids_players = new List<int> { 1, 2, 3, 4 };

    private List<CustomCharacter> customCharacterList = new List<CustomCharacter>();

    public static InputManager Instance;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);

        }
    }

    private void Start()
    {

    }


    //Assign control scheme that was read from the character selection
    /**
     * Use this method when testing the whole game (the one that includes character selection and board game)
     */
    public List<PlayerInput> AssignPlayerInput(List<PlayerInput> players, List<string> playerIds)
    {

        for (int i = 0; i < players.Count; i++)
        {
            //Read values from PlayerPrefs
            int inputDeviceId = PlayerPrefs.GetInt(INPUT_DEVICE_PLAYER + playerIds[i]);
            string controlScheme = PlayerPrefs.GetString(CONTROL_SCHEME_PLAYER + playerIds[i]);
            print(CONTROL_SCHEME_PLAYER + playerIds[i]);

            players[i].SwitchCurrentControlScheme(controlScheme, InputSystem.GetDeviceById(inputDeviceId));
        }

        return players;

    }

    //Use this method to assign individually a control scheme, and not to a list of players
    //This only works with the Character selection
    public PlayerInput AssignPlayerInput(PlayerInput player, int playerId)
    {
        //Read values from PlayerPrefs
        int inputDeviceId = PlayerPrefs.GetInt(INPUT_DEVICE_PLAYER + playerId.ToString());
        string controlScheme = PlayerPrefs.GetString(CONTROL_SCHEME_PLAYER + playerId.ToString());
        print(CONTROL_SCHEME_PLAYER + playerId);

        player.SwitchCurrentControlScheme(controlScheme, InputSystem.GetDeviceById(inputDeviceId));

        return player;

    }

    //This will assign player control schemes on the order they are received
    // WASD, ZGHJ, PLOA, NUM
    //The return is in case you need to save the modified instance again. However it should still work without using the returned value
    /**
     * Call this one for local testing purposes in your scene, will assign only keyboard inputs
     */
    public List<PlayerInput> AssignPlayerInput(List<PlayerInput> players)
    {

        controls = new Controls();

        List<InputControlScheme> controlSchemes = new List<InputControlScheme> {
            controls.KeyboardWASDScheme,
            controls.KeyboardZGHJScheme,
            controls.KeyboardPLÖÄScheme,
            controls.KeyboardNumScheme
        };

        for (int i = 0; i < players.Count; i++)
        {
            players[i].SwitchCurrentControlScheme(controlSchemes[i].name, Keyboard.current);
        }


        return players;

    }


    private void SavePlayerControlScheme(string playerId, int inputDeviceId, string controlScheme)
    {
        PlayerPrefs.SetInt(INPUT_DEVICE_PLAYER + playerId, inputDeviceId);
        PlayerPrefs.SetString(CONTROL_SCHEME_PLAYER + playerId, controlScheme);
        print(CONTROL_SCHEME_PLAYER + playerId);
    }

    //This should be called at the end of the selection screen
    public void SavePlayersControlSchemes()
    {
        //Iterate over all tuples
        //Tupple corresponds to playerId, inputDeviceId, controlScheme
        foreach (Tuple<int, int, string> player in playerSchemes)
        {
            SavePlayerControlScheme(player.Item1.ToString(), player.Item2, player.Item3);
        }
    }

    public void AddControlScheme(Tuple<int, int, string> playerScheme)
    {
        //Check if the tuple is already in the list
        if (playerSchemes.Count == 0)
        {
            print("Control Scheme Saved: " + playerScheme.Item3);
            playerSchemes.Add(playerScheme);
        }
        else
        {
            bool shouldInsert = true;
            foreach (Tuple<int, int, string> scheme in playerSchemes)
            {
                if (String.Compare(scheme.Item3, playerScheme.Item3) == 0)
                {
                    shouldInsert = false;
                    break;
                }
            }
            if (shouldInsert)
            {
                if (playerSchemes.Count == (4 - ids_players.Count))
                {
                    print("Control Scheme Saved: " + playerScheme.Item3);
                    playerSchemes.Add(playerScheme);
                }

            }
        }
    }

    /*
    //Methods sets the values of which player is AI and which not, should only be called on the Character selection scene
    public void SaveAIPlayers(bool firstPlayer, bool secondPlayer, bool thirdPlayer, bool fourthPlayer )
    {
        PlayerPrefs.SetString(PLAYER_1_AI, firstPlayer.ToString());
        PlayerPrefs.SetString(PLAYER_2_AI, secondPlayer.ToString());
        PlayerPrefs.SetString(PLAYER_3_AI, thirdPlayer.ToString());
        PlayerPrefs.SetString(PLAYER_4_AI, fourthPlayer.ToString());

        //Example of how to retrieve them
        bool player1_AI = PlayerPrefs.GetString(PLAYER_1_AI).Equals("True");
        print("AI player value === " + player1_AI.ToString());
    }
    */


    /*
    private void GetPlayerControlScheme(string playerId)
    {
        int inputDeviceId = PlayerPrefs.GetInt(INPUT_DEVICE_PLAYER + playerId);
        string controlScheme = PlayerPrefs.GetString(CONTROL_SCHEME_PLAYER + playerId);

        //var playerInput = PlayerInput.Instantiate( controlScheme, inputDeviceId);
        
    }
    */

    public void SaveCustomCharacter(CustomCharacter customCharacter)
    {
        customCharacterList.Add(customCharacter);
    }

    public bool ShouldEnableSaveButton()
    {
        return playerSchemes.Count == (customCharacterList.Count + 1);
    }

    //Call this and pass your player minifid as well as the player id
    //Assuming that players id are 1, 2, 3, 4
    public void ApplyPlayerCustomization(GameObject player, int playerId)
    {
        if (playerId < 1 || playerId > 4 || customCharacterList.Count == 0)
        {
            return;
        }

        //Base Minifig and corresponding custom deisgn
        var minifigComponent = player.transform.Find("Minifig Character").transform.Find("Geo_grp");
        CustomCharacter customCharacter = customCharacterList[playerId - 1];

        //Hair / Hat
        var hairComponent = player.transform.Find(
            "Minifig Character/jointScaleOffset_grp/Joint_grp/detachSpine/spine01/spine02/spine03/spine04/spine05/spine06/spine07/neck/head/hat_loc/m85974(Clone)/Shell"
            );
        var hair = hairComponent.GetComponent<MeshFilter>();
        hair.mesh = customCharacter.hair;

        //Hair / Hat Color
        var hairColor = hairComponent.GetComponent<MeshRenderer>();
        hairColor.material = customCharacter.hairColor;



        //Face tone
        var playerComponents = minifigComponent.transform.Find("Head");
        var faceTone = minifigComponent.transform.Find("Head").GetComponent<SkinnedMeshRenderer>();
        faceTone.material = customCharacter.faceTone;

        //Face
        var face = minifigComponent.transform.Find("Face").GetComponent<SkinnedMeshRenderer>();
        face.material = customCharacter.face;

        //Upper body
        playerComponents = minifigComponent.transform.Find("Torso");

        var torsoBack = playerComponents.transform.Find("Torso_Back").GetComponent<SkinnedMeshRenderer>();
        torsoBack.material = customCharacter.upperBody_back;

        var torsoFront = playerComponents.transform.Find("Torso_Front").GetComponent<SkinnedMeshRenderer>();
        torsoFront.material = customCharacter.upperBody_front;

        var torsoMain = playerComponents.transform.Find("Torso_main").GetComponent<SkinnedMeshRenderer>();
        torsoMain.material = customCharacter.upperBody_main;

        //Arms
        playerComponents = minifigComponent.transform.Find("Arm_Left");

        var armLeftFront = playerComponents.transform.Find("Arm_L_Front").GetComponent<SkinnedMeshRenderer>();
        armLeftFront.material = customCharacter.leftArm_front;

        var armLeftMain = playerComponents.transform.Find("Arm_L_Main").GetComponent<SkinnedMeshRenderer>();
        armLeftMain.material = customCharacter.leftArm_main;

        playerComponents = minifigComponent.transform.Find("Arm_Right");

        var armRightFront = playerComponents.transform.Find("Arm_R_Front").GetComponent<SkinnedMeshRenderer>();
        armRightFront.material = customCharacter.leftArm_front;

        var armRightMain = playerComponents.transform.Find("Arm_R_Main").GetComponent<SkinnedMeshRenderer>();
        armRightMain.material = customCharacter.leftArm_main;

        //Hands
        var handLeft = minifigComponent.transform.Find("Hand_Left").GetComponent<SkinnedMeshRenderer>();
        handLeft.material = customCharacter.left_hand;

        var handRight = minifigComponent.transform.Find("Hand_Right").GetComponent<SkinnedMeshRenderer>();
        handRight.material = customCharacter.right_hand;

        //Hips
        playerComponents = minifigComponent.transform.Find("Hip");

        var hipCrotch = playerComponents.transform.Find("Hip_Crotch").GetComponent<SkinnedMeshRenderer>();
        hipCrotch.material = customCharacter.hip_crotch;

        var hipFront = playerComponents.transform.Find("Hip_Front").GetComponent<SkinnedMeshRenderer>();
        hipFront.material = customCharacter.hip_front;

        var hipMain = playerComponents.transform.Find("Hip_Main").GetComponent<SkinnedMeshRenderer>();
        hipMain.material = customCharacter.hip_main;

        //Legs
        //Left
        playerComponents = minifigComponent.transform.Find("Leg_Left");

        var legLeftFront = playerComponents.transform.Find("Leg_L_Front").GetComponent<SkinnedMeshRenderer>();
        legLeftFront.material = customCharacter.leftleg_front;

        var legLeftSide = playerComponents.transform.Find("Leg_L_Side").GetComponent<SkinnedMeshRenderer>();
        legLeftSide.material = customCharacter.leftleg_side;

        var legLeftMain = playerComponents.transform.Find("Leg_L_Main").GetComponent<SkinnedMeshRenderer>();
        legLeftMain.material = customCharacter.leftleg_main;

        //Left Foot
        var legLeftFoot = playerComponents.transform.Find("Leg_L_Foot").GetComponent<SkinnedMeshRenderer>();
        legLeftFoot.material = customCharacter.left_foot;


        //Right
        playerComponents = minifigComponent.transform.Find("Leg_Right");

        var rightLeftFront = playerComponents.transform.Find("Leg_R_Front").GetComponent<SkinnedMeshRenderer>();
        rightLeftFront.material = customCharacter.rightleg_front;

        var rightLeftSide = playerComponents.transform.Find("Leg_R_Side").GetComponent<SkinnedMeshRenderer>();
        rightLeftSide.material = customCharacter.rightleg_side;

        var rightLeftMain = playerComponents.transform.Find("Leg_R_Main").GetComponent<SkinnedMeshRenderer>();
        rightLeftMain.material = customCharacter.rightleg_main;

        //Right Foot
        var rightLeftFoot = playerComponents.transform.Find("Leg_R_Foot").GetComponent<SkinnedMeshRenderer>();
        rightLeftFoot.material = customCharacter.right_foot;

    }

}

