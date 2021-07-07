using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.InputSystem;

// TODO sort ranking for finished method
public class MiniGameManager : MonoBehaviour
{
    [SerializeField] private GameObject _player1;
    [SerializeField] private GameObject _player2;
    [SerializeField] private GameObject _player3;
    [SerializeField] private GameObject _player4;
    public GameObject _arena;

    [Header("AI Parameters")]
    public float AI_idleTime = 1.0f;
    public float AI_PlayerDetectRadius = 5.0f;
    public float AI_StunnedPreferationMultiplier = 1.3f;
    public float AI_HuntDelay = 1.0f;
    public float AI_KickDistance = 1.0f;
    public float AI_WaveDetectionRadius = 1.0f;
    public float AI_WaveDodgePercentage = 1.0f;
    public float AI_JumpPenalty = 0.5f;
    public float AI_StunDuration = 1.0f;
    public float AI_SinkingTolerance = 0.5f;
    public float AI_MeteorFleeDistance = 2.0f;
    public float AI_MeteorTolerance = 3.0f;

    private MiniGame _minigame;
    private bool gameEnded;
    private int currPlace;
    private List<int>[] placings;
    private bool[] lastActive;

    private void Awake()
    {
        gameEnded = false;
        currPlace = 0;
        placings = new List<int>[] { new List<int>(), new List<int>(), new List<int>(), new List<int>() };
        lastActive = new bool[4] { true, true, true, true };

        _minigame = transform.GetComponent<MiniGame_Meteorfall>();

        List<PlayerInput> playerInputs = new List<PlayerInput>(4)
        {
            _player1.GetComponent<PlayerInput>(),
            _player2.GetComponent<PlayerInput>(),
            _player3.GetComponent<PlayerInput>(),
            _player4.GetComponent<PlayerInput>()
        };
        List<string> ids = new List<string>(4)
        {
            "1",
            "2",
            "3",
            "4"
        };

        bool player1_AI = PlayerPrefs.GetString("PLAYER1_AI").Equals("True");
        bool player2_AI = PlayerPrefs.GetString("PLAYER2_AI").Equals("True");
        bool player3_AI = PlayerPrefs.GetString("PLAYER3_AI").Equals("True");
        bool player4_AI = PlayerPrefs.GetString("PLAYER4_AI").Equals("True");
        bool[] aiList = new bool[] { player1_AI, player2_AI, player3_AI, player4_AI };
        AssignAI(player1_AI, player2_AI, player3_AI, player4_AI);

        //InputManager.Instance.AssignPlayerInput(playerInputs); // Stops execution of this monobehaviour in absence of playerprefs
        //InputManager.Instance.AssignPlayerInput(playerInputs, ids); // Right Version when Playerprefs work correctly
        for(int ind = 0; ind < playerInputs.Count; ind++)
        {
            if(!aiList[ind])
            {
                InputManager.Instance.AssignPlayerInput(playerInputs[ind], ind+1);
            }
        }
        
        for(int ind = 0; ind < aiList.Length; ind++)
        {
            if(!aiList[ind])
            {
                InputManager.Instance.ApplyPlayerCustomization(playerInputs[ind].gameObject, ind+1);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        bool aB1 = _player1.activeSelf;
        bool aB2 = _player2.activeSelf;
        bool aB3 = _player3.activeSelf;
        bool aB4 = _player4.activeSelf;

        bool[] currActive = new bool[4] { aB1, aB2, aB3, aB4 };

        bool placed = false;
        int numAlive = 0;
        if(!gameEnded)
        {
            for (int ind = 0; ind < currActive.Length; ind++)
            {
                if (currActive[ind])
                {
                    numAlive++;
                }
                if (!currActive[ind] && lastActive[ind])
                {
                    placings[currPlace].Add(ind + 1);
                    placed = true;
                }
            }
            if (placed)
            {
                currPlace++;
            }
        }
        
        if(!gameEnded && numAlive <= 1)
        {
            EndGame();
            gameEnded = true;
        }
        
        lastActive = new bool[4] { aB1, aB2, aB3, aB4 };
    }

    private void EndGame()
    {
        int[][] places = new int[4][];
        for(int ind = 0; ind < placings.Length; ind++)
        {
            places[ind] = placings[placings.Length - 1 - ind].ToArray();
        }
        StartCoroutine(DeclareWinner(places[0], places[1], places[2], places[3]));
    }

    public void AssignAI(bool p1, bool p2, bool p3, bool p4)
    {
        if(p1)
        {
            _player1.AddComponent<MeteorFallAI>();
        }
        if (p2)
        {
            _player2.AddComponent<MeteorFallAI>();
        }
        if (p3)
        {
            _player3.AddComponent<MeteorFallAI>();
        }
        if (p4)
        {
            _player4.AddComponent<MeteorFallAI>();
        }
    }

    IEnumerator DeclareWinner(int[] p1, int[] p2, int[] p3, int[] p4)
    {
        yield return new WaitForSeconds(0.5f);
        _minigame.MiniGameFinished(p1, p2, p3, p4);
    }
}
