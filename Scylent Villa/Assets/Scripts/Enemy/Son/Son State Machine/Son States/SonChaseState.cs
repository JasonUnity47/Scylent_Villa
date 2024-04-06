using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonChaseState : SonState
{
    // Declaration
    private Transform playerPos;

    public SonChaseState(Son son, SonStateMachine stateMachine) : base(son, stateMachine)
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
        if (son.sonFOV.isDetected)
        {
            son.aIPath.destination = playerPos.position;
        }

        // CHECK whether the enemy is moving.
        son.CheckMovement();

        // PERFORM animation.
        son.AnimationChange();

        // IF not detect THEN change to IDLE STATE.
        if (!son.sonFOV.isDetected)
        {
            stateMachine.ChangeState(son.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}