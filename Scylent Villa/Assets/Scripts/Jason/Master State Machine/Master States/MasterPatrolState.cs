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

        Vector2 direction = master.masterMovement.GetMoveSpot();

        master.Anim.SetFloat("Horizontal", direction.x);
        master.Anim.SetFloat("Vertical", direction.y);
        master.Anim.SetFloat("Speed", direction.sqrMagnitude);

        // Check whether player is around the enemy.
        master.masterMovement.TargetInDistance();

        // IF detect THEN change to IDLE STATE.
        if (master.masterMovement.isDetected)
        {
            stateMachine.ChangeState(master.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        // IF doesn't detect THEN patrol.
        if (!master.masterMovement.isDetected)
        {
            master.masterMovement.Patrol();
        }
    }
}
