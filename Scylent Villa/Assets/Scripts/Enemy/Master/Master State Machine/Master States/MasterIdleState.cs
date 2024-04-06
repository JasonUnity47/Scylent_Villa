using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterIdleState : MasterState
{
    public MasterIdleState(Master master, MasterStateMachine stateMachine) : base(master, stateMachine)
    {
    }

    // Declaration
    private float startTime = 0.5f;
    private float timeBtwFrame;

    public override void Enter()
    {
        base.Enter();
        timeBtwFrame = startTime;
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
            if (timeBtwFrame <= 0)
            {
                timeBtwFrame = startTime;
                stateMachine.ChangeState(master.ChaseState);
            }

            else
            {
                timeBtwFrame -= Time.deltaTime;
            }
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