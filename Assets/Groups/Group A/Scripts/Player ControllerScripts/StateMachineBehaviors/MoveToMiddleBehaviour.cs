using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class MoveToMiddleBehaviour : StateMachineBehaviour
{
    private MinifigControllerWTH playerController;
    private GameObject player;
    private Animator animator;
    private NavMeshPath currentPath;
    public float maxDistanceFromMiddle = 1f;
    public float playerDetectionDistance = 5f;
    private WhatTheHillGame game;
    private GameObject pickUpContainer;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerController = animator.GetComponentInParent<MinifigControllerWTH>();
        player = animator.gameObject;
        this.animator = animator;
        
        game = GameObject.Find("WTHGameManger").GetComponent<WhatTheHillGame>();
        pickUpContainer = GameObject.Find("PowerUpSpawner");

        Vector3 targetPosition = new Vector3(0, 0, 0);
        currentPath = new NavMeshPath(); 
        NavMesh.CalculatePath(player.transform.position, targetPosition, NavMesh.AllAreas, currentPath);
        for (int i = 0; i < currentPath.corners.Length - 1; i++)
            Debug.DrawLine(currentPath.corners[i], currentPath.corners[i + 1], Color.red);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("IsInMiddle", isPlayerInMiddle(player.transform.position));
        animator.SetBool("PlayerInSight", playerInRange(player.transform.position));
        animator.SetBool("PowerUpInSight", powerUpInRange(player.transform.position));
        if (playerController.state == MinifigControllerWTH.State.Idle)
        {
            currentPath = new NavMeshPath();
            Vector3 targetPosition = new Vector3(0, 0, 0);
            if (playerController.hasPowerUp())
            {
                playerController.SpawnPowerUp();
                playerController.MoveTo(targetPosition, maxMoveTime: 1.5f);
            } else {

                NavMesh.CalculatePath(player.transform.position, targetPosition, NavMesh.AllAreas, currentPath);
                if (currentPath.corners.Length >= 2)
                {
                    if(Vector3.Distance(player.transform.position, currentPath.corners[1]) > 0.5f)
                    {
                        playerController.MoveTo(currentPath.corners[1]);
                    } else if(currentPath.corners.Length > 2)
                    {
                        Vector3 jumpGoal = currentPath.corners[2];
                        playerController.MoveTo(jumpGoal);
                        playerController.AddForce(new Vector3(0, playerController.jumpSpeed, 0));
                    }
                }
            }
        }
    }

    private bool isPlayerInMiddle (Vector3 position)
    {
        return Mathf.Abs(position.x) <= maxDistanceFromMiddle && Mathf.Abs(position.z) <= maxDistanceFromMiddle;
    }

    private bool playerInRange (Vector3 myPosition)
    {
        bool result = false;
        foreach (GameObject player in game.Players)
        {
            if(player != this.player)
            {
                Vector3 distance = player.transform.position - myPosition;
                if (distance.magnitude <= playerDetectionDistance) return true;
            }
        }
        return result;
    }
    private bool powerUpInRange (Vector3 myPosition)
    {
        bool result = false;
        foreach(Transform pickup in pickUpContainer.transform)
        {
            Vector3 distance = pickup.position - myPosition;
            if (distance.magnitude <= playerDetectionDistance) return true;
        }
        return result;
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
