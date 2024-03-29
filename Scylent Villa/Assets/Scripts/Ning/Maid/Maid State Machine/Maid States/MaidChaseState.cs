using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaidChaseState : MaidState
{
    public MaidChaseState(Maid maid, MaidStateMachine stateMachine, string animName) : base(maid, stateMachine, animName)
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

        // Check whether player is around the enemy.
        maid.maidMovement.TargetInDistance();

        // IF not detect THEN change to IDLE STATE.
        if (!maid.maidMovement.isDetected)
        {
            stateMachine.ChangeState(maid.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        // IF detect THEN chase player.
        if (maid.maidMovement.isDetected)
        {
            maid.maidMovement.PathFollow();
        }
    }
}
