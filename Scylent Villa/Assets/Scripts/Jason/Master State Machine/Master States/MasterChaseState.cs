using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterChaseState : MasterState
{
    public MasterChaseState(Master master, MasterStateMachine stateMachine, string animName) : base(master, stateMachine, animName)
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
        master.masterMovement.TargetInDistance();

        // IF not detect THEN change to IDLE STATE.
        if (!master.masterMovement.isDetected)
        {
            stateMachine.ChangeState(master.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        // IF detect THEN chase player.
        if (master.masterMovement.isDetected)
        {
            master.masterMovement.PathFollow();
        }
    }
}
