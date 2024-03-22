using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaidIdleState : MaidState
{
    public MaidIdleState(Maid maid, MaidStateMachine stateMachine, string animName) : base(maid, stateMachine, animName)
    {
    }

    // Declaration
    // Timer
    private float startTime = 1f;
    private float timeBtwFrame;

    public override void Enter()
    {
        base.Enter();

        timeBtwFrame = startTime; // Set initial timer.

        maid.Anim.SetFloat("Speed", 0f);

        maid.Rb.velocity = Vector2.zero; // Avoid slipping.
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicalUpdate()
    {
        base.LogicalUpdate();

        Vector2 direction = maid.maidMovement.GetTarget();

        maid.Anim.SetFloat("Horizontal", direction.x);
        maid.Anim.SetFloat("Vertical", direction.y);
        maid.Anim.SetFloat("Speed", direction.sqrMagnitude);

        // Check whether player is around the enemy.
        maid.maidMovement.TargetInDistance();

        // IF detect THEN change to CHASE STATE.
        if (maid.maidMovement.isDetected)
        {
            // Using timer to perform a feeling that enemy is preparing to move.
            if (timeBtwFrame <= 0)
            {
                stateMachine.ChangeState(maid.ChaseState);
                timeBtwFrame = startTime;
            }

            else
            {
                timeBtwFrame -= Time.deltaTime;
            }
        }

        // ELSE change to PATROL STATE.
        else
        {
            // Using timer to perform a feeling that enemy is preparing to move.
            if (timeBtwFrame <= 0)
            {
                stateMachine.ChangeState(maid.PatrolState);
                timeBtwFrame = startTime;
            }

            else
            {
                timeBtwFrame -= Time.deltaTime;
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
