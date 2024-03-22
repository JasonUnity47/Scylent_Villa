using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterIdleState : MasterState
{
    public MasterIdleState(Master master, MasterStateMachine stateMachine, string animName) : base(master, stateMachine, animName)
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

        master.Anim.SetFloat("Speed", 0f);

        master.Rb.velocity = Vector2.zero; // Avoid slipping.
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicalUpdate()
    {
        base.LogicalUpdate();

        Vector2 direction = master.masterMovement.GetTarget();

        master.Anim.SetFloat("Horizontal", direction.x);
        master.Anim.SetFloat("Vertical", direction.y);
        master.Anim.SetFloat("Speed", direction.sqrMagnitude);

        // Check whether player is around the enemy.
        master.masterMovement.TargetInDistance();

        // IF detect THEN change to CHASE STATE.
        if (master.masterMovement.isDetected)
        {
            // Using timer to perform a feeling that enemy is preparing to move.
            if (timeBtwFrame <= 0)
            {
                stateMachine.ChangeState(master.ChaseState);
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
                stateMachine.ChangeState(master.PatrolState);
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