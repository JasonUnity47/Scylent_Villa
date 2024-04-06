using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterChaseState : MasterState
{
    // Declaration
    private Transform playerPos;

    public MasterChaseState(Master master, MasterStateMachine stateMachine) : base(master, stateMachine)
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
        if (master.masterFOV.isDetected)
        {
            master.aIPath.destination = playerPos.position;
        }

        // CHECK whether the enemy is moving.
        master.CheckMovement();

        // PERFORM animation.
        master.AnimationChange();

        // IF not detect THEN change to IDLE STATE.
        if (!master.masterFOV.isDetected)
        {
            stateMachine.ChangeState(master.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
