using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonChaseState : SonState
{
    public SonChaseState(Son son, SonStateMachine stateMachine, string animName) : base(son, stateMachine, animName)
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
        son.sonMovement.TargetInDistance();

        // IF not detect THEN change to IDLE STATE.
        if (!son.sonMovement.isDetected)
        {
            stateMachine.ChangeState(son.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        // IF detect THEN chase player.
        if (son.sonMovement.isDetected)
        {
            son.sonMovement.PathFollow();
        }
    }
}
