using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterIdleState : MasterState
{
    public MasterIdleState(Master master, MasterStateMachine stateMachine, string animName) : base(master, stateMachine, animName)
    {
    }

    // Declaration

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

        // IF detect player THEN change to CHASE STATE.
        if (master.masterFOV.isDetected)
        {
            stateMachine.ChangeState(master.ChaseState);
        }

        else
        {
            // ELSE change to PATROL STATE.
            stateMachine.ChangeState(master.PatrolState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
