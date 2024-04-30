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

        // Perform partrol movement.
        master.enemyPatrol.Patrol();

        // Check whether the enemy is moving.
        master.CheckMovement();

        // Perform animation.
        master.AnimationChange();

        // Check whether player is within the field of view of the enemy.
        // If detect then change to IDLE STATE.
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