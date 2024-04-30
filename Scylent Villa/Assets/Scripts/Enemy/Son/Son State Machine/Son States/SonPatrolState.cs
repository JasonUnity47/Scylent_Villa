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

        // Perform partrol movement.
        son.enemyPatrol.Patrol();

        // Check whether the enemy is moving.
        son.CheckMovement();

        // Perform animation.
        son.AnimationChange();

        // Check whether player is within the field of view of the enemy.
        // If detect then change to IDLE STATE.
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