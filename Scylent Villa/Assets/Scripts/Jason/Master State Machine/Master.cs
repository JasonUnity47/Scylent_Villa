using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Master : MonoBehaviour
{
    // Script Reference
    public AIPath aIPath  { get; private set; }

    public EnemyPatrol enemyPatrol { get; private set; }

    public MasterStateMachine StateMachine { get; private set; }

    public MasterIdleState IdleState { get; private set; }

    public MasterPatrolState PatrolState { get; private set; }

    public MasterChaseState ChaseState { get; private set; }

    public Rigidbody2D Rb { get; private set; }

    public Animator Anim { get; private set; }

    // Value
    public bool isMoving = false;

    private bool move = false;
    public bool front = false;
    public bool back = false;
    public bool left = false;
    public bool right = false;

    private float offset = 0.2f;

    private void Awake()
    {
        aIPath = GetComponent<AIPath>();
        enemyPatrol = GetComponent<EnemyPatrol>();

        StateMachine = new MasterStateMachine();

        IdleState = new MasterIdleState(this, StateMachine, "IdleBool");
        PatrolState = new MasterPatrolState(this, StateMachine, "PatrolBool");
        ChaseState = new MasterChaseState(this, StateMachine, "ChaseBool");
    }

    private void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();


        StateMachine.InitializeState(IdleState);
    }

    private void Update()
    {
        StateMachine.CurrentState.LogicalUpdate();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }

    public void CheckMovement()
    {
        if (aIPath.velocity.magnitude != 0)
        {
            isMoving = true;
        }

        else
        {
            isMoving = false;
        }
    }

    private Vector2 GetDirection()
    {
        return  (Vector2)transform.position - (Vector2)aIPath.destination;
    }

    public void AnimationChange()
    {
        if (aIPath.velocity.y < -offset && !front)
        {
            front = true;
            back = false;
            left = false;
            right = false;

            //Debug.Log(1);

            Anim.SetBool("BackBool", back);
            Anim.SetBool("RightBool", right);
            Anim.SetBool("LeftBool", left);
            Anim.SetBool("FrontBool", front);
        }

        if (aIPath.velocity.y > offset && !back)
        {
            back = true;
            front = false;
            left = false;
            right = false;

            //Debug.Log(2);

            Anim.SetBool("FrontBool", front);
            Anim.SetBool("RightBool", right);
            Anim.SetBool("LeftBool", left);
            Anim.SetBool("BackBool", back);
        }

        if (aIPath.velocity.x < -offset && !left)
        {
            left = true;
            front = false;
            back = false;
            right = false;

            //Debug.Log(3);

            Anim.SetBool("FrontBool", front);
            Anim.SetBool("BackBool", back);
            Anim.SetBool("RightBool", right);
            Anim.SetBool("LeftBool", left);
        }

        if (aIPath.velocity.x > offset && !right)
        {
            right = true;
            left = false;
            front = false;
            back = false;

            //Debug.Log(4);

            Anim.SetBool("FrontBool", front);
            Anim.SetBool("BackBool", back);
            Anim.SetBool("LeftBool", left);
            Anim.SetBool("RightBool", right);
        }

        Anim.SetBool("MoveBool", isMoving);
        Anim.SetFloat("Horizontal", Mathf.Clamp(aIPath.velocity.x, -1, 1));
        Anim.SetFloat("Vertical", Mathf.Clamp(aIPath.velocity.y, -1, 1));

      
    }
}
