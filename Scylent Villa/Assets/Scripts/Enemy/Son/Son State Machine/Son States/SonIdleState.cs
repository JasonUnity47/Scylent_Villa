using UnityEngine;

public class SonIdleState : SonState
{
    public SonIdleState(Son son, SonStateMachine stateMachine) : base(son, stateMachine)
    {
    }

    // Declaration
    // Timer
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

        // If detect player then change to CHASE STATE.
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
            // Else change to PATROL STATE.
            stateMachine.ChangeState(son.PatrolState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}