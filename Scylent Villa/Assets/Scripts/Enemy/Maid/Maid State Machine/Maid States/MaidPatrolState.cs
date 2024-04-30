using UnityEngine;

public class MaidPatrolState : MaidState
{
    public MaidPatrolState(Maid maid, MaidStateMachine stateMachine) : base(maid, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicalUpdate()
    {
        base.LogicalUpdate();

        // Perform partrol movement.
        maid.enemyPatrol.Patrol();

        // Check whether the enemy is moving.
        maid.CheckMovement();

        // Perform animation.
        maid.AnimationChange();

        // Check whether player is within the field of view of the enemy.
        // If detect then change to IDLE STATE.
        if (maid.maidFOV.isDetected)
        {
            stateMachine.ChangeState(maid.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}