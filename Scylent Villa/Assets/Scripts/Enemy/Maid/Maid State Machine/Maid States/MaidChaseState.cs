using UnityEngine;

public class MaidChaseState : MaidState
{
    // Declaration
    private Transform playerPos;

    public MaidChaseState(Maid maid, MaidStateMachine stateMachine) : base(maid, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicalUpdate()
    {
        base.LogicalUpdate();

        // If detect player then chase player.
        if (maid.maidFOV.isDetected)
        {
            maid.aIPath.destination = playerPos.position;
        }

        // Check whether the enemy is moving.
        maid.CheckMovement();

        // Perform animation.
        maid.AnimationChange();

        // If not detect then change to IDLE STATE.
        if (!maid.maidFOV.isDetected)
        {
            stateMachine.ChangeState(maid.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}