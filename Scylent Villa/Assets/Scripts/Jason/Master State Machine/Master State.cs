using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterState
{
    public Master master { get; private set; }
    public MasterStateMachine stateMachine { get; private set; }

    private string animName;

    public MasterState(Master master, MasterStateMachine stateMachine, string animName)
    {
        this.master = master;
        this.stateMachine = stateMachine;
        this.animName = animName;
    }

    public virtual void Enter()
    {
        Debug.Log("Enter " + animName);
        master.Anim.SetBool(animName, true);
    }

    public virtual void LogicalUpdate()
    {

    }

    public virtual void PhysicsUpdate()
    {

    }

    public virtual void Exit()
    {
        Debug.Log("Exit " + animName);
        master.Anim?.SetBool(animName, false);
    }
}
