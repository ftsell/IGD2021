using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class InputAssigner : MonoBehaviour
{
    // add player inputs in order
    public List<PlayerInput> players;
    public List<GameObject> characters;
    public List<GameObject> hudCharacters;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 4; i++) {
            InputManager.Instance.AssignPlayerInput(players[i], i+1);
            
            if (!PlayerPrefs.GetString("PLAYER" + (i+1).ToString() + "_AI").Equals("True")) {
                // skip AIs
                InputManager.Instance.ApplyPlayerCustomization(characters[i], i+1);
                InputManager.Instance.ApplyPlayerCustomization(hudCharacters[i], i+1);
            }
            else {
                Debug.Log("AI detected");
            }
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
