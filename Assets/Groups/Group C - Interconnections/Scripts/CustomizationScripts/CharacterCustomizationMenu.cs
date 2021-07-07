using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class CharacterCustomizationMenu : MonoBehaviour
{

    //Set here player prefab for LobbyPlayer
    //Can be found in Group C - Interconnections/Prefabs/LobbyPlayer
    public GameObject _menuPlayerPrefab;

    private int characterIndex = 1;

    private void Start()
    {
        Button savebutton = GameObject.Find("SaveButton").GetComponent<Button>();
        savebutton.interactable = false;

        //Detect player Inputs
        SpawnKeyboardPlayers();
        SpawnControllerPlayers();
    }

    private void Update()
    {
        Button savebutton = GameObject.Find("SaveButton").GetComponent<Button>();
        if (InputManager.Instance.ShouldEnableSaveButton())
        {
            savebutton.interactable = true;
        } else
        {
            savebutton.interactable = false;
        }
    }



    public void StartGame()
    {
        if (InputManager.Instance.players_colors.Count != 0)
        {
            for (int i = 0; i < InputManager.Instance.players_colors.Count; i++)
            {
                PlayerPrefs.SetString("PLAYER" + InputManager.Instance.ids_players[0].ToString() + "_AI", "True");
                InputManager.Instance.ids_players.RemoveAt(0);
            }
        }
        Debug.Log("PLAYER1_AI" + PlayerPrefs.GetString("PLAYER1_AI"));
        Debug.Log("PLAYER2_AI" + PlayerPrefs.GetString("PLAYER2_AI"));
        Debug.Log("PLAYER3_AI" + PlayerPrefs.GetString("PLAYER3_AI"));
        Debug.Log("PLAYER4_AI" + PlayerPrefs.GetString("PLAYER4_AI"));

        InputManager.Instance.SavePlayersControlSchemes();

        //For testing purposes this should load TestMinigame
        //LoadingManager.Instance.LoadMiniGameTest(MiniGameType.freeForAll);

        //If you want to test your minigame directly from this scene, in order to test the customization and the inputs from player prefs
        //You can uncomment this line and put the name of you scene to load it, will be a direct jump but should work for you to test
        //SceneManager.LoadScene("YOUR_SCENE_NAME");

        //For the real implementation should Load the BoardGame
        LoadingManager.Instance.LoadMainBoardGame();
    }

    public void SaveButton()
    {
        Button savebutton = GameObject.Find("SaveButton").GetComponent<Button>();
        Button randombutton = GameObject.Find("RandomizeButton").GetComponent<Button>();
        Button startgame = GameObject.Find("StartButton").GetComponent<Button>();

        Text color_player = GameObject.Find("MessageColor").GetComponent<Text>();
        Text messagetitle = GameObject.Find("Message1").GetComponent<Text>();
        messagetitle.text = "Player saved! :";

        color_player.color = InputManager.Instance.players_colors[0];
        color_player.text = InputManager.Instance.players_colors_names[0];
        
        PlayerPrefs.SetString("PLAYER"+InputManager.Instance.ids_players[0].ToString()+"_NAME", InputManager.Instance.players_colors_names[0]);
        PlayerPrefs.SetString("PLAYER"+InputManager.Instance.ids_players[0].ToString()+"_AI", "False");
        InputManager.Instance.players_colors.RemoveAt(0);
        InputManager.Instance.players_colors_names.RemoveAt(0);
        InputManager.Instance.ids_players.RemoveAt(0);

        if (InputManager.Instance.players_colors.Count == 0) {
            savebutton.interactable = false;
            randombutton.interactable = false;
        }

        Debug.Log("color saved");
        SavePlayerCustomization();
        characterIndex += 1;
    }

    //Creates a custom player with the current selections
    private void SavePlayerCustomization()
    {
        //Create new blueprint for customization
        CustomCharacter player = new CustomCharacter();

        //Head
        //Hair
        GameObject hair = GameObject.Find("Hair");
        ChangeHair hairScript = hair.GetComponent<ChangeHair>();

        player.hair = hairScript.GetCurrentSelection();

        //Hat
        GameObject hat = GameObject.Find("Hats");
        Accessories hatScript = hat.GetComponent<Accessories>();

        player.hat = hatScript.GetCurrentSelection();

        //Hat-hair color
        GameObject haircolor = GameObject.Find("Hair-/HatColor");
        ChangeHairColour haircolorScript = haircolor.GetComponent<ChangeHairColour>();

        player.hairColor = haircolorScript.GetCurrentSelection();

        //Face
        GameObject faces = GameObject.Find("FaceGesture");
        ChangeFace faceScript = faces.GetComponent<ChangeFace>();

        player.face = faceScript.GetCurrentSelection();

        //Face tone
        GameObject faceTone = GameObject.Find("FaceTone");
        ChangeFaceTone faceToneScript = faceTone.GetComponent<ChangeFaceTone>();

        player.faceTone = faceToneScript.GetCurrentSelection();

        //Upper body
        GameObject upperBody = GameObject.Find("UpperBody");
        ChangeUpperBody upperBodyScript = upperBody.GetComponent<ChangeUpperBody>();

        player.upperBody_front = upperBodyScript.GetCurrentSelection(ChangeUpperBody.UpperBodyOptions.TorsoFront);
        player.upperBody_back = upperBodyScript.GetCurrentSelection(ChangeUpperBody.UpperBodyOptions.TorsoBack);
        player.upperBody_main = upperBodyScript.GetCurrentSelection(ChangeUpperBody.UpperBodyOptions.TorsoMain);

        //Arms
        GameObject arms = GameObject.Find("Arms");
        ChangeArms armsScript = arms.GetComponent<ChangeArms>();

        player.leftArm_front = armsScript.GetCurrentSelection(ChangeArms.ArmOptions.LeftArmFront);
        player.leftArm_main = armsScript.GetCurrentSelection(ChangeArms.ArmOptions.LeftArmMain);
        player.rightArm_front = armsScript.GetCurrentSelection(ChangeArms.ArmOptions.RightArmFront);
        player.rightArm_main = armsScript.GetCurrentSelection(ChangeArms.ArmOptions.RightArmMain);

        //Hands
        GameObject hands = GameObject.Find("Hands");
        ChangeHands handsScript = hands.GetComponent<ChangeHands>();

        player.left_hand = handsScript.GetCurrentSelection(ChangeHands.HandOptions.LeftHand);
        player.right_hand = handsScript.GetCurrentSelection(ChangeHands.HandOptions.RighHand);

        //Hips
        GameObject hip = GameObject.Find("HipSelection");
        ChangeHip hipScript = hip.GetComponent<ChangeHip>();

        player.hip_crotch = hipScript.GetCurrentSelection(ChangeHip.HipOptions.HipCrotch);
        player.hip_front = hipScript.GetCurrentSelection(ChangeHip.HipOptions.HipFront);
        player.hip_main = hipScript.GetCurrentSelection(ChangeHip.HipOptions.HipMain);

        //Legs
        GameObject legs = GameObject.Find("Legs");
        ChangeLegs legsScript = legs.GetComponent<ChangeLegs>();

        player.leftleg_front = legsScript.GetCurrentSelection(ChangeLegs.LegsOptions.LeftLegFront);
        player.leftleg_side = legsScript.GetCurrentSelection(ChangeLegs.LegsOptions.LeftLegSide);
        player.leftleg_main = legsScript.GetCurrentSelection(ChangeLegs.LegsOptions.LeftLegMain);

        player.rightleg_front = legsScript.GetCurrentSelection(ChangeLegs.LegsOptions.RightLegFront);
        player.rightleg_side = legsScript.GetCurrentSelection(ChangeLegs.LegsOptions.RightLegSide);
        player.rightleg_main = legsScript.GetCurrentSelection(ChangeLegs.LegsOptions.RightLegMain);

        //Feet
        GameObject feet = GameObject.Find("Feet");
        ChangeFeet feetScript = feet.GetComponent<ChangeFeet>();

        player.right_foot = feetScript.GetCurrentSelection(ChangeFeet.FeetOptions.RightFoot);
        player.left_foot = feetScript.GetCurrentSelection(ChangeFeet.FeetOptions.LeftFoot);

        player.playerId = characterIndex;

        //Save character into InputManager
        InputManager.Instance.SaveCustomCharacter(player);
    }

    public void RandomizeCharacter()
    {
        int up_custom = Random.Range(0,1);

        switch(up_custom){
            case 0: 
                //hair
                GameObject hair = GameObject.Find("Hair");
                ChangeHair hairScript = hair.GetComponent<ChangeHair>();
                hairScript.Randomize();
                Debug.Log("Hair");
                break;
            case 1:
                //hat
                GameObject hat = GameObject.Find("Hats");
                Accessories hatScript = hat.GetComponent<Accessories>();
                hatScript.Randomize();
                Debug.Log("Hats");
                break;
            
        }

        //Hair-hat color
        GameObject haircolor = GameObject.Find("Hair-/HatColor");
        ChangeHairColour haircolorScript = haircolor.GetComponent<ChangeHairColour>();
        haircolorScript.Randomize();
        Debug.Log("Hair color");

        //face
        GameObject faces = GameObject.Find("FaceGesture");
        ChangeFace faceScript = faces.GetComponent<ChangeFace>();
        faceScript.Randomize();
        Debug.Log("face");

        //face tone
        GameObject faceTone = GameObject.Find("FaceTone");
        ChangeFaceTone faceToneScript = faceTone.GetComponent<ChangeFaceTone>();
        faceToneScript.Randomize();
        Debug.Log("face tone");

        //upper body
        GameObject upperBody = GameObject.Find("UpperBody");
        ChangeUpperBody upperBodyScript = upperBody.GetComponent<ChangeUpperBody>();
        upperBodyScript.Randomize();
        Debug.Log("upper body");

        //arms
        GameObject arms = GameObject.Find("Arms");
        ChangeArms armsScript = arms.GetComponent<ChangeArms>();
        armsScript.Randomize();        
        Debug.Log("arms");

        //hands
        GameObject hands = GameObject.Find("Hands");
        ChangeHands handsScript = hands.GetComponent<ChangeHands>();
        handsScript.Randomize();
        Debug.Log("hands");

        //hip
        GameObject hip = GameObject.Find("HipSelection");
        ChangeHip hipScript = hip.GetComponent<ChangeHip>();
        hipScript.Randomize();
        Debug.Log("hip");

        //legs
        GameObject legs = GameObject.Find("Legs");
        ChangeLegs legsScript = legs.GetComponent<ChangeLegs>();
        legsScript.Randomize();
        Debug.Log("legs");

        //feet
        GameObject feet = GameObject.Find("Feet");
        ChangeFeet feetScript = feet.GetComponent<ChangeFeet>();
        feetScript.Randomize();
        Debug.Log("feet");

    }

    //Contorl Scheme detection
    //Work in progress
    //This will be used when players select their character at the start screen
    //Detect Control Schemes

    private void SpawnKeyboardPlayers()
    {
        var controls = new Controls();

        PlayerControllerLobby player;
        player = PlayerInput.Instantiate(_menuPlayerPrefab).GetComponent<PlayerControllerLobby>();
        player.SetInputDeviceAndControlScheme(controls.KeyboardWASDScheme.name, Keyboard.current);
        player.transform.localScale = new Vector3(0, 0, 0);

        player = PlayerInput.Instantiate(_menuPlayerPrefab).GetComponent<PlayerControllerLobby>();
        player.SetInputDeviceAndControlScheme(controls.KeyboardZGHJScheme.name, Keyboard.current);
        player.transform.localScale = new Vector3(0, 0, 0);

        player = PlayerInput.Instantiate(_menuPlayerPrefab).GetComponent<PlayerControllerLobby>();
        player.SetInputDeviceAndControlScheme(controls.KeyboardPLÖÄScheme.name, Keyboard.current);
        player.transform.localScale = new Vector3(0, 0, 0);

        player = PlayerInput.Instantiate(_menuPlayerPrefab).GetComponent<PlayerControllerLobby>();
        player.SetInputDeviceAndControlScheme(controls.KeyboardNumScheme.name, Keyboard.current);
        player.transform.localScale = new Vector3(0, 0, 0);


    }


    private void SpawnControllerPlayers()
    {
        var controls = new Controls();
        PlayerControllerLobby player;
        ReadOnlyArray<Gamepad> gamepads = Gamepad.all;

        for (int i = 0; i < gamepads.Count; i++)
        {
            player = PlayerInput.Instantiate(_menuPlayerPrefab).GetComponent<PlayerControllerLobby>();
            player.SetInputDeviceAndControlScheme(controls.GamepadScheme.name, Gamepad.all[i]);
            player.transform.localScale = new Vector3(0, 0, 0);

        }
    }
}
