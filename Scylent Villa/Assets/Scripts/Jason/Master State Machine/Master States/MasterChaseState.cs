using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterChaseState : MasterState
{
    private bool move = false;
    public bool front = false;
    public bool back = false;
    public bool left = false;
    public bool right = false;

    private float offset = 0.2f;

    private Vector2 direction;

    public MasterChaseState(Master master, MasterStateMachine stateMachine, string animName) : base(master, stateMachine, animName)
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

        direction = master.masterMovement.GetTarget();

        AnimationChange();

        master.Anim.SetFloat("Horizontal", direction.x);
        master.Anim.SetFloat("Vertical", direction.y);
        master.Anim.SetFloat("Speed", direction.sqrMagnitude);

        // Check whether player is around the enemy.
        master.masterMovement.TargetInDistance();

        // IF not detect THEN change to IDLE STATE.
        if (!master.masterMovement.isDetected)
        {
            stateMachine.ChangeState(master.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        // IF detect THEN chase player.
        if (master.masterMovement.isDetected)
        {
            master.masterMovement.PathFollow();
        }
    }

    public virtual void AnimationChange()
    {
        if (direction.sqrMagnitude != 0)
        {
            move = true;
            master.Anim.SetBool("MoveBool", move);
        }

        else
        {
            move = false;
            master.Anim.SetBool("MoveBool", move);
        }

        if (direction.y < -offset && !front)
        {
            front = true;
            back = false;
            left = false;
            right = false;

            //Debug.Log(1);

            master.Anim.SetBool("BackBool", back);
            master.Anim.SetBool("RightBool", right);
            master.Anim.SetBool("LeftBool", left);

            master.Anim.SetBool("FrontBool", front);
        }

        if (direction.y > offset && !back)
        {
            back = true;
            front = false;
            left = false;
            right = false;

            //Debug.Log(2);

            master.Anim.SetBool("FrontBool", front);
            master.Anim.SetBool("RightBool", right);
            master.Anim.SetBool("LeftBool", left);

            master.Anim.SetBool("BackBool", back);
        }

        if (direction.x < -offset && !left)
        {
            left = true;
            front = false;
            back = false;
            right = false;

            //Debug.Log(3);

            master.Anim.SetBool("FrontBool", front);
            master.Anim.SetBool("BackBool", back);
            master.Anim.SetBool("RightBool", right);

            master.Anim.SetBool("LeftBool", left);
        }

        if (direction.x > offset && !right)
        {
            right = true;
            left = false;
            front = false;
            back = false;

            //Debug.Log(4);

            master.Anim.SetBool("FrontBool", front);
            master.Anim.SetBool("BackBool", back);
            master.Anim.SetBool("LeftBool", left);

            master.Anim.SetBool("RightBool", right);
        }
    }
}
