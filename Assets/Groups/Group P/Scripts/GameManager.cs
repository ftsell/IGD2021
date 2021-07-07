using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


namespace GroupP {
    public class GameManager : MiniGame
    {
        private static GameManager _instance;

        public static GameManager instance { get { return _instance; }}

        public GameObject player1, player2, player3, player4;

        public List<GameObject> songs = new List<GameObject>();

        public bool startPlaying;

        public int songIndex = -1;

        public NoteSystem noteSystem;     //<-- Tutorial: BeatScroller theBS (theBS === noteSystem)

        public GameObject PressStartPrompt;

        private bool stoppedOnEnd = false;

        float beatsPerMinute;

        bool stoppedDancing;

        public float getBpmOfActiveSong() {
            return songs[songIndex].GetComponent<Song>().beatsPerMinute;
        }

        public override string getDisplayName() {
            return "Dance Party";
        }

        public override string getSceneName()  {
            return "RhythmGameScene";
        }

        public override MiniGameType getMiniGameType() {
            return MiniGameType.freeForAll;
        }

        void Awake() {
            if(_instance != null && _instance != this) {
                Destroy(this.gameObject);
            } else {
                _instance = this;    
            }
            if(songIndex < 0) {
                songIndex = UnityEngine.Random.Range(0, songs.Count);
            }
            Pulse.setBPM(songs[songIndex].GetComponent<Song>().beatsPerMinute);
            Pulse.setOffset(songs[songIndex].GetComponent<Song>().offsetInSeconds);
            Pulse.setNumberOfBeats(songs[songIndex].GetComponent<Song>().totalNumberOfBeats);
        }

        // Start is called before the first frame update
        void Start()
        {

            player1.GetComponent<Player>().isAI = PlayerPrefs.GetString("PLAYER1_AI", "False").Equals("True");
            player2.GetComponent<Player>().isAI = PlayerPrefs.GetString("PLAYER2_AI", "False").Equals("True");
            player3.GetComponent<Player>().isAI = PlayerPrefs.GetString("PLAYER3_AI", "False").Equals("True");
            player4.GetComponent<Player>().isAI = PlayerPrefs.GetString("PLAYER4_AI", "False").Equals("True");

            stoppedDancing = false;

            var playerInputs = new List<PlayerInput>();
            if(!player1.GetComponent<Player>().isAI) {
                Debug.Log("In AI-if 1");
                playerInputs.Add(player1.GetComponent<PlayerInput>());
                InputManager.Instance.ApplyPlayerCustomization(player1, 1);
            }
            if(!player2.GetComponent<Player>().isAI) {
                Debug.Log("In AI-if 2");
                playerInputs.Add(player2.GetComponent<PlayerInput>());
                InputManager.Instance.ApplyPlayerCustomization(player2, 2);
            }
            if(!player3.GetComponent<Player>().isAI) {
                Debug.Log("In AI-if 3");
                playerInputs.Add(player3.GetComponent<PlayerInput>());
                InputManager.Instance.ApplyPlayerCustomization(player3, 3);
            }
            if(!player4.GetComponent<Player>().isAI) {
                Debug.Log("In AI-if 4");
                playerInputs.Add(player4.GetComponent<PlayerInput>());
                InputManager.Instance.ApplyPlayerCustomization(player4, 4);
            }
            
            InputManager.Instance.AssignPlayerInput(playerInputs, new List<string> { "1", "2", "3", "4"});
            
            _instance = this;
            // DANCE
            beatsPerMinute = songs[songIndex].GetComponent<Song>().beatsPerMinute;
        }

        private void setStartPlaying() {
            startPlaying = true;
            
        }

        // Update is called once per frame
        void Update()
        {
            if(!startPlaying) {
                if(Input.anyKeyDown) {
                    
                    //Invoke("setStartPlaying", songs[songIndex].GetComponent<Song>().offsetInSeconds);
                    this.startPlaying = true;
                    songs[songIndex].GetComponent<AudioSource>().Play();
                    //DANCE
                    GameEventSystem.current.StartDance();
                    Pulse.play();
                    Destroy(PressStartPrompt, 0f);
                }
            }

            if(!songs[songIndex].GetComponent<AudioSource>().isPlaying && startPlaying && !stoppedOnEnd) {
                stoppedOnEnd = true;
                Pulse.stop();
                if (!stoppedDancing)
                {
                    GameEventSystem.current.StopDance();
                    stoppedDancing = true;
                }
                var scores = new List<(int, int)>();
                scores.Add((1, player1.GetComponent<Score>().score));
                scores.Add((2, player2.GetComponent<Score>().score));
                scores.Add((3, player3.GetComponent<Score>().score));
                scores.Add((4, player4.GetComponent<Score>().score));

                // Highest score first
                scores.Sort(delegate ((int, int) p1, (int, int) p2) 
                {
                    return p2.Item2.CompareTo(p1.Item2);
                });

                List<List<int>> places = new List<List<int>>();
                for(int i=0;i<4;++i) {
                    places.Add(new List<int>());
                }
                
                // First score is always first rank
                places[0].Add(scores[0].Item1);
                
                // distribute remaining scores into the respective arrays by comparing
                // score[i] to score[i-1]
                int lastPlace = 0;
                for(int i=1;i < 4; ++i) {
                    if(scores[i].Item2 < scores[i-1].Item2) {
                        lastPlace++;
                    }
                    places[lastPlace].Add(scores[i].Item1);
                }

                for(int i = 0; i < 4 ; ++i) {
                    Debug.Log("place " + i+1);
                    foreach(var n in places[i]) {
                        Debug.Log(n);
                    }
                }
                
                MiniGameFinished(
                    firstPlace: places[0].ToArray(), 
                    secondPlace: places[1].ToArray(),
                    thirdPlace: places[2].ToArray(),
                    fourthPlace: places[3].ToArray()
                );
            }
        }

        public GameObject getSong() {
            return songs[songIndex];
        }

        //DANCE
        void DanceEvent()
        {
            GameEventSystem.current.Hit();
            
        }

        void BeatEvent()
        {
            GameEventSystem.current.Beat();
        }
    }
}