using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonState
{
    public Son son { get; private set; }
    public SonStateMachine stateMachine { get; private set; }

    private string animName;

    public SonState(Son son, SonStateMachine stateMachine, string animName)
    {
        this.son = son;
        this.stateMachine = stateMachine;
        this.animName = animName;
    }

    public virtual void Enter()
    {
        Debug.Log("Enter " + animName);
        son.Anim.SetBool(animName, true);
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
        son.Anim.SetBool(animName, false);
    }
}
