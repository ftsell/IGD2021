using UnityEngine;

public class GoalManager : MonoBehaviour
{
    public GameObject goal;

    public Transform leftCylinder;
    public Transform rightCylinder;
    private Vector3 targetPosition;
    private bool moveGoal = false;

    void Update() 
    {
        if (moveGoal)
        {
            goal.transform.position = Vector3.Lerp(goal.transform.position, targetPosition, Time.deltaTime);
        }
    }

    public void MoveGoalDownwards()
    {
        moveGoal = true;
        targetPosition = goal.transform.position + new Vector3(0, -2, 0);
    }

    public void MoveGoal(float movement)
    {
        goal.transform.localPosition = goal.transform.localPosition + new Vector3(0, 0, movement);
    }

    public void ActivateGoal()
    {
        goal.SetActive(true);
    }

    public Vector3 GetGoalPosition()
    {
        return goal.transform.position;
    }

    public void SetGoalPosition(Vector3 pos) 
    {
        goal.transform.position = pos;
    }

    public float GetUpperExtremeX()
    {
        return leftCylinder.position.x < rightCylinder.position.x? leftCylinder.position.x : rightCylinder.position.x;
    }
    public float GetLowerExtremeX()
    {
        return leftCylinder.position.x >= rightCylinder.position.x? leftCylinder.position.x : rightCylinder.position.x;
    }
}
