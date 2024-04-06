using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonPatrolState : SonState
{
    public SonPatrolState(Son son, SonStateMachine stateMachine) : base(son, stateMachine)
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

        // PERFORM partrol movement.
        son.enemyPatrol.Patrol();

        // CHECK whether the enemy is moving.
        son.CheckMovement();

        // PERFORM animation.
        son.AnimationChange();

        // CHECK whether player is within the field of view of the enemy.
        // IF detect THEN change to IDLE STATE.
        if (son.sonFOV.isDetected)
        {
            stateMachine.ChangeState(son.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}