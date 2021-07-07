using System.Collections;
using UnityEngine;

public class GameManagerK : MonoBehaviour
{
    public CrossTheCanyons crossTheCanyons;
    public MinifigControllerModified minifigure1;
    public MinifigControllerModified minifigure2;
    public LevelManager levelManager;
    public CountdownTimer timer;
    public GameOverChecker gameOverChecker;
    public BridgeMovement leftBridgeMovement;
    public BridgeMovement rightBridgeMovement;
    public CraneMovement leftCrane;
    public CraneMovement rightCrane;
    public MoveCamera mainCamera;

    //private fields
    private Rigidbody leftBridge;
    private Rigidbody rightBridge;
    private bool leftPlayerMoveCompleted = false;
    private bool rightPlayerMoveCompleted = false;

    public IEnumerator ReleaseBridgeSegments()
    {
        leftBridgeMovement.DisconnectFromCrane();
        rightBridgeMovement.DisconnectFromCrane();
        leftBridge.useGravity = true;
        rightBridge.useGravity = true;
        leftBridgeMovement.enabled = false;
        rightBridgeMovement.enabled = false;

        yield return new WaitForSeconds(1.0f);

        (Vector3 leftGoal, Vector3 rightGoal) = levelManager.GetCurrentGoals();
        if (!gameOverChecker.IsLeftPlayerDead())
        {
            leftCrane.MoveOutOfScene(true);
            minifigure1.MoveTo(leftGoal, moveDelay: 2.0f, speedMultiplier: 0.2f,
                onComplete: () => {levelManager.MoveGoalDownwards(left: true);
                    minifigure1.PlaySpecialAnimation(MinifigControllerModified.SpecialAnimation.Dance, 
                    onSpecialComplete: x => {StartNextLevel(leftPlayer: true);});});
        }
        if (!gameOverChecker.IsRightPlayerDead())
        {
            rightCrane.MoveOutOfScene(false);
            minifigure2.MoveTo(rightGoal, moveDelay: 2.0f, speedMultiplier: 0.2f,
                onComplete: () => {levelManager.MoveGoalDownwards(left: false);
                    minifigure2.PlaySpecialAnimation(MinifigControllerModified.SpecialAnimation.Dance, 
                    onSpecialComplete: x => {StartNextLevel(leftPlayer: false);});});
        }
    }

    private void StartNextLevel(bool leftPlayer)
    {
        if (leftPlayer && !leftPlayerMoveCompleted)
            leftPlayerMoveCompleted = true;
        else if (!leftPlayer && !rightPlayerMoveCompleted)
            rightPlayerMoveCompleted = true;
        
        if ((leftPlayerMoveCompleted && rightPlayerMoveCompleted) || (leftPlayerMoveCompleted && gameOverChecker.IsRightPlayerDead())
            || (gameOverChecker.IsLeftPlayerDead() && rightPlayerMoveCompleted))
        {
            leftPlayerMoveCompleted = false;
            rightPlayerMoveCompleted = false;
            mainCamera.MoveForward(levelManager.DistanceBetweenCurrentPlatforms());
            (leftBridge, rightBridge) = levelManager.NextLevel(gameOverChecker.IsLeftPlayerDead(), gameOverChecker.IsRightPlayerDead());
            leftBridgeMovement.SetBridgeBody(leftBridge, gameOverChecker.IsLeftPlayerDead());
            rightBridgeMovement.SetBridgeBody(rightBridge, gameOverChecker.IsRightPlayerDead());
        }
    }

    public void BridgeInPosition()
    {
        if (!gameOverChecker.IsLeftPlayerDead())
        {
            leftBridgeMovement.enabled = true;
        }
        if (!gameOverChecker.IsRightPlayerDead())
        {
            rightBridgeMovement.enabled = true;
        }
        levelManager.ActivateBarriers();
        timer.StartTimer();
    }

    private void Start()
    {
        (leftBridge, rightBridge) = levelManager.Init();
        leftBridgeMovement.SetBridgeBody(leftBridge, false);
        rightBridgeMovement.SetBridgeBody(rightBridge, false);
    }

    public void GameOver(int leftPlayerFinalLevel, int rightPlayerFinalLevel)
    {
        Debug.Log("Game Over for both teams!");
        crossTheCanyons.GameOver(leftPlayerFinalLevel, rightPlayerFinalLevel);
    }

}
