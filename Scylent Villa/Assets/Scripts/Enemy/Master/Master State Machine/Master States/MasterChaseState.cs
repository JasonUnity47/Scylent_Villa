using UnityEngine;

public class MasterChaseState : MasterState
{
    // Declaration
    private Transform playerPos;

    public MasterChaseState(Master master, MasterStateMachine stateMachine) : base(master, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        // Get reference.
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicalUpdate()
    {
        base.LogicalUpdate();

        // If detect player then chase player.
        if (master.masterFOV.isDetected)
        {
            master.aIPath.destination = playerPos.position;
        }

        // Check whether the enemy is moving.
        master.CheckMovement();

        // Perform animation.
        master.AnimationChange();

        // If not detect then change to IDLE STATE.
        if (!master.masterFOV.isDetected)
        {
            stateMachine.ChangeState(master.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}