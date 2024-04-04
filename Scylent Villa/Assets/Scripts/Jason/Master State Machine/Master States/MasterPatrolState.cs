using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterPatrolState : MasterState
{
    public MasterPatrolState(Master master, MasterStateMachine stateMachine, string animName) : base(master, stateMachine, animName)
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

        master.CheckMovement();

        master.AnimationChange();

        master.enemyPatrol.Patrol();

        //// Check whether player is around the enemy.
        //master.masterMovement.TargetInDistance();

        //// IF detect THEN change to IDLE STATE.
        //if (master.masterMovement.isDetected)
        //{
        //    stateMachine.ChangeState(master.IdleState);
        //}
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
