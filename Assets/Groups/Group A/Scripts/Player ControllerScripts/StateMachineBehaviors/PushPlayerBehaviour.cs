using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class PushPlayerBehaviour : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    private WhatTheHillGame game;
    private GameObject player;
    private MinifigControllerWTH playerController;
    public float playerDetectionDistance = 5f;
    private NavMeshPath currentPath;
    private GameObject targetPlayer;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        game = GameObject.Find("WTHGameManger").GetComponent<WhatTheHillGame>();
        player = animator.gameObject;
        playerController = animator.GetComponentInParent<MinifigControllerWTH>();
        targetPlayer = playerInRange(player.transform.position);
        if (targetPlayer == null) animator.SetBool("PlayerInSight", false);
    }

    private GameObject playerInRange(Vector3 myPosition)
    {
        foreach (GameObject player in game.Players)
        {
            if (player != this.player)
            {
                Vector3 distance = player.transform.position - myPosition;
                if (distance.magnitude <= playerDetectionDistance) return player;
            }
        }
        return null;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(playerController.state == MinifigControllerWTH.State.Idle)
        {
            currentPath = new NavMeshPath();
            NavMesh.CalculatePath(player.transform.position, targetPlayer.transform.position, NavMesh.AllAreas, currentPath);
            if(currentPath.corners.Length < 2)
            {
                animator.SetBool("PlayerInSight", false);
            } else
            {
                if (Vector3.Distance(player.transform.position, currentPath.corners[1]) > 0.5f)
                {
                    playerController.MoveTo(currentPath.corners[1]);
                }
                else if (currentPath.corners.Length > 2)
                {
                    Vector3 jumpGoal = currentPath.corners[2];
                    playerController.MoveTo(jumpGoal);
                    playerController.AddForce(new Vector3(0, playerController.jumpSpeed, 0));
                }
            }
        }

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
