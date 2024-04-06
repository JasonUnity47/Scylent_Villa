using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterPatrolState : MasterState
{
    public MasterPatrolState(Master master, MasterStateMachine stateMachine) : base(master, stateMachine)
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
        master.enemyPatrol.Patrol();

        // CHECK whether the enemy is moving.
        master.CheckMovement();

        // PERFORM animation.
        master.AnimationChange();

        // CHECK whether player is within the field of view of the enemy.
        // IF detect THEN change to IDLE STATE.
        if (master.masterFOV.isDetected)
        {
            stateMachine.ChangeState(master.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
