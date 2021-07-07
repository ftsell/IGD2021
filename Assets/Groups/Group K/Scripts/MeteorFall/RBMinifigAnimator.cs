using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RBMinifigAnimator : MonoBehaviour
{
    public RBCharacterController Controller;

    // Animation event.
    public void StepFoot()
    {
        Controller.StepFoot();
    }

    // Animation event.
    public void LiftFoot()
    {
        Controller.LiftFoot();
    }
}
