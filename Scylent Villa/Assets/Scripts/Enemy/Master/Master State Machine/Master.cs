using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Master : MonoBehaviour
{
    // Declaration
    // Script Reference
    public AIPath aIPath { get; private set; }

    public EnemyPatrol enemyPatrol { get; private set; }

    public MasterFOV masterFOV { get; private set; }

    // State Machine Reference
    public MasterStateMachine StateMachine { get; private set; }

    public MasterIdleState IdleState { get; private set; }

    public MasterPatrolState PatrolState { get; private set; }

    public MasterChaseState ChaseState { get; private set; }

    // Component
    public Animator Anim { get; private set; }

    // Value
    public bool isMoving = false;

    public bool Front { get; private set; }

    public bool Back { get; private set; }

    public bool Left { get; private set; }

    public bool Right {get; private set;}

    private float activationOffset = 0.5f;

    private float deactivationOffset = 0.2f;

    private void Awake()
    {
        aIPath = GetComponent<AIPath>();
        enemyPatrol = GetComponent<EnemyPatrol>();
        masterFOV = GetComponentInChildren<MasterFOV>();

        StateMachine = new MasterStateMachine();

        IdleState = new MasterIdleState(this, StateMachine);
        PatrolState = new MasterPatrolState(this, StateMachine);
        ChaseState = new MasterChaseState(this, StateMachine);
    }

    private void Start()
    {
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

    // CHECK whether the enemy is moving.
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

        return;
    }

    // PERFORM animation.
    public void AnimationChange()
    {
        // Check if the enemy is moving
        isMoving = aIPath.velocity.magnitude > 0.1f; // Adjust the threshold as needed

        // Look Left
        if (aIPath.velocity.x < -activationOffset && !Left)
        {
            Left = true;
            Front = Back = Right = false;
        }
        else if (aIPath.velocity.x > deactivationOffset && Left)
        {
            Left = false;
        }

        // Look Right
        if (aIPath.velocity.x > activationOffset && !Right)
        {
            Right = true;
            Front = Back = Left = false;
        }
        else if (aIPath.velocity.x < -deactivationOffset && Right)
        {
            Right = false;
        }

        // Look Back
        if (aIPath.velocity.y > activationOffset && !Back)
        {
            Back = true;
            Front = Left = Right = false;
        }
        else if (aIPath.velocity.y < -deactivationOffset && Back)
        {
            Back = false;
        }

        // Look Front
        if (aIPath.velocity.y < -activationOffset && !Front)
        {
            Front = true;
            Back = Left = Right = false;
        }
        else if (aIPath.velocity.y > deactivationOffset && Front)
        {
            Front = false;
        }

        // Update animation parameters
        Anim.SetBool("MoveBool", isMoving);
        Anim.SetFloat("Horizontal", Mathf.Clamp(aIPath.velocity.x, -1, 1));
        Anim.SetFloat("Vertical", Mathf.Clamp(aIPath.velocity.y, -1, 1));

        // Update animation booleans
        Anim.SetBool("FrontBool", Front);
        Anim.SetBool("BackBool", Back);
        Anim.SetBool("LeftBool", Left);
        Anim.SetBool("RightBool", Right);

        return;
    }
}
