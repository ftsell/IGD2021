using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

 public class GameManagerJ : MiniGame
{
    // Start is called before the first frame update
    public int deathCount1 = 0;
    public int deathCount2 = 0;
    public Text team1DeathCount;
    public Text team2DeathCount;
    public GameObject arm;
    private float counter = 0;

    public GameObject myPlayer;
    public GameObject secondPlayer;
    public GameObject thirdPlayer;
    public GameObject fourthPlayer;
    public bool gameFinished = false;

    public int team1LavaDeath = 0;
    public int team2LavaDeath = 0;
    bool isRunning = true;

    void Start()
    {
        
        var playerInputs = new List<PlayerInput> { myPlayer.GetComponent<PlayerInput>(), secondPlayer.GetComponent<PlayerInput>(), thirdPlayer.GetComponent<PlayerInput>(), fourthPlayer.GetComponent<PlayerInput>() };
   
        InputManager.Instance.AssignPlayerInput(playerInputs);

        StartCoroutine(MotorChange(arm, counter));
    }

    IEnumerator MotorChange(GameObject arm, float counter)
    {
        while (true)
        {
            arm.GetComponent<HingeJoint>().motor = new JointMotor() { targetVelocity = 50 + counter, force = 100000 };
            counter += 5;
            yield return new WaitForSeconds(3);
        }
    }

    public void UpdateDeath(bool isTeam1)
    {
        if (isTeam1) 
        {
            deathCount1++;
            team1DeathCount.text = "Hits: " + deathCount1;
        }
        else
        {
            deathCount2++;
            team2DeathCount.text = "Hits: " + deathCount2;
        }       
    }

    // Update is called once per frame
    void Update()
    {
        
        if (gameFinished == true)
        {
            if (!isRunning) return;
            isRunning = false;
            Time.timeScale = 0;
            Debug.Log("Game finished");
            //Create array of positions with player ids, this also works in case there are multiple players in one position

            //Note this is still work in progress, but ideally you will use it like this
            MiniGameFinished(GetWinningMembers().ToArray(), GetLoosingMembers().ToArray(), new int[] { 0}, new int[] {1});
        }
    }

    /// <summary>
    /// Returns all winners
    /// </summary>
    /// <returns></returns>
    private List<int> GetWinningMembers()
    {
        List<int> ranking = new List<int>();

        if (team1LavaDeath == 2 && team2LavaDeath == 2)
        {
            ranking.Add(0);
            ranking.Add(1);
            ranking.Add(2);
            ranking.Add(3);
        }
        else if (team1LavaDeath == 2)
        {
            ranking.Add(0);
            ranking.Add(1);
        }
        else if (team2LavaDeath == 2)
        {
            ranking.Add(2);
            ranking.Add(3);
        }
        else if (deathCount1 < deathCount2)
        {
            ranking.Add(0);
            ranking.Add(1);
        }
        else
        {
            ranking.Add(2);
            ranking.Add(3);
        }

        return ranking;
    }
    private List<int> GetLoosingMembers()
    {
        List<int> ranking = new List<int>();

        if (team1LavaDeath == 2 && team2LavaDeath == 2)
        {
            return ranking;
        }
        else
       if (team1LavaDeath == 2)
        {
            ranking.Add(2);
            ranking.Add(3);
        }
        else if (team2LavaDeath == 2)
        {
            ranking.Add(0);
            ranking.Add(1);
        }
        else if (deathCount1 < deathCount2)
        {
            ranking.Add(2);
            ranking.Add(3);
        }
        else
        {
            ranking.Add(0);
            ranking.Add(1);
        }

        return ranking;
    }

    public override string getDisplayName()
    {
        return "Jump or Die";
    }

    public override string getSceneName()
    {
        return "Minigame_Group_J";
    }

    public override MiniGameType getMiniGameType()
    {
        return MiniGameType.teamVsTeam;
    }


}
