using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaidPatrolState : MaidState
{
    public MaidPatrolState(Maid maid, MaidStateMachine stateMachine, string animName) : base(maid, stateMachine, animName)
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

        Vector2 direction = maid.maidMovement.GetMoveSpot();

        maid.Anim.SetFloat("Horizontal", direction.x);
        maid.Anim.SetFloat("Vertical", direction.y);
        maid.Anim.SetFloat("Speed", direction.sqrMagnitude);

        // Check whether player is around the enemy.
        maid.maidMovement.TargetInDistance();

        // IF detect THEN change to IDLE STATE.
        if (maid.maidMovement.isDetected)
        {
            stateMachine.ChangeState(maid.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        // IF doesn't detect THEN patrol.
        if (!maid.maidMovement.isDetected)
        {
            maid.maidMovement.Patrol();
        }
    }
}
