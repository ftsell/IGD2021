using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CannonStandoff : MiniGame {
	
	private bool running;
	private int scoreSolo, scoreTeam;
	private GameObject activeWall;
	
	public CannonPlayer p1, p2, p3, p4;
	public GameObject[] wallTypes;
	public Material pRed, pGreen, pBlue, pPink;
	
	private void SetPlayerColor(CannonPlayer player, string color) {
		Material mat = pRed;
		
		if (color.Equals("RED")) {
			mat = pRed;
		} else if (color.Equals("GREEN")) {
			mat = pGreen;
		} else if (color.Equals("BLUE")) {
			mat = pBlue;
		} else if (color.Equals("PINK")) {
			mat = pPink;
		}
		
		player.Color = mat;
	}
	
	private void InitPlayerColors() {
		string colorP1 = PlayerPrefs.GetString("PLAYER1_NAME");
		string colorP2 = PlayerPrefs.GetString("PLAYER2_NAME");
		string colorP3 = PlayerPrefs.GetString("PLAYER3_NAME");
		string colorP4 = PlayerPrefs.GetString("PLAYER4_NAME");
		
		SetPlayerColor(p1, colorP1);
		SetPlayerColor(p2, colorP2);
		SetPlayerColor(p3, colorP3);
		SetPlayerColor(p4, colorP4);
	}
	
	private void InitWall() {
		if (wallTypes != null && wallTypes.Length > 0) {
			int i = Random.Range(0, wallTypes.Length);
			GameObject wall = wallTypes[i];
			
			activeWall = Instantiate(wall);
		}
	}
	
	private void SetAi(CannonPlayer player, bool ai) {
		if (ai) {
			PlayerInput input = player.GetComponent<PlayerInput>();
			PlayerInput inputMarker = player.marker.GetComponent<PlayerInput>();
			
			Destroy(input);
			Destroy(inputMarker);
			
			player.SetAiControlled(true);
			player.SetWall(activeWall);
		}
	}
	
	private void InitAi() {
		bool aiP1 = PlayerPrefs.GetString("Player1_AI").Equals("True");
		bool aiP2 = PlayerPrefs.GetString("Player2_AI").Equals("True");
		bool aiP3 = PlayerPrefs.GetString("Player3_AI").Equals("True");
		bool aiP4 = PlayerPrefs.GetString("Player4_AI").Equals("True");
		
		SetAi(p1, aiP1);
		SetAi(p2, aiP2);
		SetAi(p3, aiP3);
		SetAi(p4, aiP4);
	}
	
	private void CheckEndOfGame() {
		if (!running) {
			return;
		}
		
		int remainingBlocks = activeWall.GetComponentsInChildren<WallBlock>().Length;
		int[] firstPlace, secondPlace;
		int[] empty = {};
		
		if (remainingBlocks == 0) {
			if (scoreSolo > scoreTeam) {
				firstPlace = new int[]{1};
				secondPlace = new int[]{2, 3, 4};
			} else if (scoreTeam > scoreSolo) {
				firstPlace = new int[]{2, 3, 4};
				secondPlace = new int[]{1};
			} else {
				firstPlace = new int[]{1, 2, 3, 4};
				secondPlace = empty;
			}
			
			running = false;
			
			MiniGameFinished(firstPlace, secondPlace, empty, empty);
		}
	}
	
	void Start() {
		running = true;
		
		InitWall();
		InitAi();
		InitPlayerColors();
	}
	
	void Update() {
		CheckEndOfGame();
	}
	
	public void ScoreForSolo() {
		scoreSolo++;
	}
	
	public void ScoreForTeam() {
		scoreTeam++;
	}
	
	public override string getDisplayName() {
		return "Cannon standoff";
	}
	
	public override string getSceneName() {
		return "CannonStandoff";
	}

	public override MiniGameType getMiniGameType() {
		return MiniGameType.singleVsTeam;
	}
	
}
