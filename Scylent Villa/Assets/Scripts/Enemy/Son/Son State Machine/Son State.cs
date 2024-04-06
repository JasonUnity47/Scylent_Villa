using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonState
{
    public Son son { get; private set; }
    public SonStateMachine stateMachine { get; private set; }

    public SonState(Son son, SonStateMachine stateMachine)
    {
        this.son = son;
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
