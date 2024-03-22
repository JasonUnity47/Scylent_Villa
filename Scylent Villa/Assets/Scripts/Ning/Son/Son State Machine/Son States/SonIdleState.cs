using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonIdleState : SonState
{
    public SonIdleState(Son son, SonStateMachine stateMachine, string animName) : base(son, stateMachine, animName)
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

        son.Anim.SetFloat("Speed", 0f);

        son.Rb.velocity = Vector2.zero; // Avoid slipping.
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicalUpdate()
    {
        base.LogicalUpdate();

        Vector2 direction = son.sonMovement.GetTarget();

        son.Anim.SetFloat("Horizontal", direction.x);
        son.Anim.SetFloat("Vertical", direction.y);
        son.Anim.SetFloat("Speed", direction.sqrMagnitude);

        // Check whether player is around the enemy.
        son.sonMovement.TargetInDistance();

        // IF detect THEN change to CHASE STATE.
        if (son.sonMovement.isDetected)
        {
            // Using timer to perform a feeling that enemy is preparing to move.
            if (timeBtwFrame <= 0)
            {
                stateMachine.ChangeState(son.ChaseState);
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
                stateMachine.ChangeState(son.PatrolState);
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
