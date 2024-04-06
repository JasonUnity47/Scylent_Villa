using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaidIdleState : MaidState
{
    public MaidIdleState(Maid maid, MaidStateMachine stateMachine) : base(maid, stateMachine)
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
        if (maid.maidFOV.isDetected)
        {
            if (timeBtwFrame <= 0)
            {
                timeBtwFrame = startTime;
                stateMachine.ChangeState(maid.ChaseState);
            }

            else
            {
                timeBtwFrame -= Time.deltaTime;
            }
        }

        else
        {
            // ELSE change to PATROL STATE.
            stateMachine.ChangeState(maid.PatrolState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
