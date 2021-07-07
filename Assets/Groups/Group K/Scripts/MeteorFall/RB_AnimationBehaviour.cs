using UnityEngine;

namespace Unity.LEGO.Minifig
{

    public class RB_AnimationBehaviour : StateMachineBehaviour
    {
        int playSpecialHash = Animator.StringToHash("Play Special");

        RBCharacterController minifigController;

        override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
        {
            animator.SetBool(playSpecialHash, false);
            if (!minifigController)
            {
                minifigController = animator.transform.parent.GetComponent<RBCharacterController>();
            }

        }

        public override void OnStateMachineExit(Animator animator, int stateMachinePathHash)
        {
            minifigController.SpecialAnimationFinished();
        }
    }

}