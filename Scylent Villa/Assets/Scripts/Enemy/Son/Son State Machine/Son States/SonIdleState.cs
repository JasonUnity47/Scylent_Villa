using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonIdleState : SonState
{
    public SonIdleState(Son son, SonStateMachine stateMachine) : base(son, stateMachine)
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
        if (son.sonFOV.isDetected)
        {
            if (timeBtwFrame <= 0)
            {
                timeBtwFrame = startTime;
                stateMachine.ChangeState(son.ChaseState);
            }

            else
            {
                timeBtwFrame -= Time.deltaTime;
            }
        }

        else
        {
            // ELSE change to PATROL STATE.
            stateMachine.ChangeState(son.PatrolState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}