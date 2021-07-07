using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class MoveToPowerUpBehaviour : StateMachineBehaviour
{
    private GameObject player;
    private MinifigControllerWTH playerController;
    public float playerDetectionDistance = 5f;
    private NavMeshPath currentPath;
    private GameObject pickUpContainer;
    private Transform target;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = animator.gameObject;
        playerController = animator.GetComponentInParent<MinifigControllerWTH>();
        pickUpContainer = GameObject.Find("PowerUpSpawner");
        target = powerUpInRange(player.transform.position);
        
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (target == null)
        {
            animator.SetBool("PowerUpInSight", false);
        }
        else if (playerController.state == MinifigControllerWTH.State.Idle)
        {
            currentPath = new NavMeshPath();
            NavMesh.CalculatePath(player.transform.position, target.position, NavMesh.AllAreas, currentPath);
            if (currentPath.corners.Length < 2)
            {
                animator.SetBool("PowerUpInSight", false);
            }
            else
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

    private Transform powerUpInRange(Vector3 myPosition)
    {
        foreach (Transform pickup in pickUpContainer.transform)
        {
            Vector3 distance = pickup.position - myPosition;
            if (distance.magnitude <= playerDetectionDistance) return pickup;
        }
        return null;
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
