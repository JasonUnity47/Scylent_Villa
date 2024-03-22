using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaidState
{
    public Maid maid { get; private set; }
    public MaidStateMachine stateMachine { get; private set; }

    private string animName;

    public MaidState(Maid maid, MaidStateMachine stateMachine, string animName)
    {
        this.maid = maid;
        this.stateMachine = stateMachine;
        this.animName = animName;
    }

    public virtual void Enter()
    {
        Debug.Log("Enter " + animName);
        maid.Anim.SetBool(animName, true);
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
        maid.Anim.SetBool(animName, false);
    }
}
