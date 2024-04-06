using System.Collections;
using System.Collections.Generic;
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

        // IF detect player THEN chase player.
        if (maid.maidFOV.isDetected)
        {
            maid.aIPath.destination = playerPos.position;
        }

        // CHECK whether the enemy is moving.
        maid.CheckMovement();

        // PERFORM animation.
        maid.AnimationChange();

        // IF not detect THEN change to IDLE STATE.
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
