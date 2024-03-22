using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonPatrolState : SonState
{
    public SonPatrolState(Son son, SonStateMachine stateMachine, string animName) : base(son, stateMachine, animName)
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

        Vector2 direction = son.sonMovement.GetMoveSpot();

        son.Anim.SetFloat("Horizontal", direction.x);
        son.Anim.SetFloat("Vertical", direction.y);
        son.Anim.SetFloat("Speed", direction.sqrMagnitude);

        // Check whether player is around the enemy.
        son.sonMovement.TargetInDistance();

        // IF detect THEN change to IDLE STATE.
        if (son.sonMovement.isDetected)
        {
            stateMachine.ChangeState(son.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        // IF doesn't detect THEN patrol.
        if (!son.sonMovement.isDetected)
        {
            son.sonMovement.Patrol();
        }
    }
}
