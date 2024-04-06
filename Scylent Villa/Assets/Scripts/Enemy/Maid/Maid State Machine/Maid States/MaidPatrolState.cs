using System.Collections;
using System.Collections.Generic;
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

        // PERFORM partrol movement.
        maid.enemyPatrol.Patrol();

        // CHECK whether the enemy is moving.
        maid.CheckMovement();

        // PERFORM animation.
        maid.AnimationChange();

        // CHECK whether player is within the field of view of the enemy.
        // IF detect THEN change to IDLE STATE.
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
