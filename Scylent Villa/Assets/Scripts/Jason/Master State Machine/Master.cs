using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        return (Vector2)aIPath.destination - (Vector2)transform.position;
    }

    public void AnimationChange()
    {
        Vector2 direction = GetDirection();

        Anim.SetFloat("Horizontal", Mathf.Clamp(direction.x, -1f, 1f));
        Anim.SetFloat("Vertical", Mathf.Clamp(direction.y, -1f, 1f));

        Anim.SetBool("MoveBool", isMoving);
    }
}
