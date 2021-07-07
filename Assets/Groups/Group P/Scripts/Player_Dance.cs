using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GroupP
{
    public class Player_Dance : MonoBehaviour
    {
        Animator animator;
        MinifigFaceAnimationController faceAnim;
        public GameObject minifig;

        public Texture2D badFace;
        public Texture2D specialFace;

        int hashstart = Animator.StringToHash("Start");
        int hashstop = Animator.StringToHash("Stop");

        bool stumbling;

        private void Start()
        {
            animator = minifig.GetComponent<Animator>();
            GameEventSystem.current.onStartDance += StartDance;
            GameEventSystem.current.onStopDance += StopDance;

            faceAnim = minifig.GetComponent<MinifigFaceAnimationController>();
            
            faceAnim.AddAnimation(MinifigFaceAnimationController.FaceAnimation.Frustrated, new Texture2D[] { badFace});
            faceAnim.AddAnimation(MinifigFaceAnimationController.FaceAnimation.Cool, new Texture2D[] { specialFace });

            stumbling = false;
        }

        void StartDance()
        {
            animator.SetTrigger(hashstart);
        }
        private void StopDance()
        {
            animator.SetTrigger(hashstop);
        }

        private void OnDestroy()
        {
            GameEventSystem.current.onStartDance -= StartDance;
        }

        public void BadKey()
        {
            //HardStumble();
            SoftStumble();
            BadFaceAnimation();
        }

        private void SoftStumble()
        {
            animator.Play("Stumble", 1, 0.1f);
        }

        private void HardStumble()
        {
            if (!stumbling)
            {
                stumbling = true;
                animator.Play("Stumble", 2, 0.1f);
                Invoke("ResetLayerWeight", animator.GetCurrentAnimatorStateInfo(2).length * 0.9f);
                animator.SetLayerWeight(2, 1f);
                BadFaceAnimation();
            }
        }

        public void SpecialKey()
        {
            //Invoke("ResetLayerWeight", 1f);
            //animator.SetLayerWeight(4, 1f);
            SpecialFaceAnimation();

        }

        private void ResetLayerWeight()
        {
            animator.SetLayerWeight(2, 0f);
            animator.SetLayerWeight(4, 0f);
            stumbling = false;
        }

        private void BadFaceAnimation()
        {
            faceAnim.PlayAnimation(MinifigFaceAnimationController.FaceAnimation.Frustrated, 0.5f);
        }

        private void SpecialFaceAnimation()
        {
            faceAnim.PlayAnimation(MinifigFaceAnimationController.FaceAnimation.Cool, 0.5f);
        }

    }
}