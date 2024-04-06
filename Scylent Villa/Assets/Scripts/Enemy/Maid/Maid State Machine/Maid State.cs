using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaidState
{
    public Maid maid { get; private set; }
    public MaidStateMachine stateMachine { get; private set; }

    public MaidState(Maid maid, MaidStateMachine stateMachine)
    {
        this.maid = maid;
        this.stateMachine = stateMachine;
    }

    public virtual void Enter()
    {
    }

    public virtual void LogicalUpdate()
    {
    }

    public virtual void PhysicsUpdate()
    {
    }

    public virtual void Exit()
    {
    }
}
