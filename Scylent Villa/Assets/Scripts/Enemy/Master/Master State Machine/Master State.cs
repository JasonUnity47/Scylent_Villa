using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterState
{
    public Master master { get; private set; }
    public MasterStateMachine stateMachine { get; private set; }

    public MasterState(Master master, MasterStateMachine stateMachine)
    {
        this.master = master;
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
