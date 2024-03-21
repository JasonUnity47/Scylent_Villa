using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Master : MonoBehaviour
{
    // Script Reference
    public MasterMovement masterMovement { get; private set; }

    public MasterStateMachine StateMachine { get; private set; }

    public MasterIdleState IdleState { get; private set; }

    public MasterPatrolState PatrolState { get; private set; }

    public MasterChaseState ChaseState { get; private set; }

    public Rigidbody2D Rb { get; private set; }

    public Animator Anim { get; private set; }

    private void Awake()
    {
        StateMachine = new MasterStateMachine();

        IdleState = new MasterIdleState(this, StateMachine, "IdleBool");
        PatrolState = new MasterPatrolState(this, StateMachine, "PatrolBool");
        ChaseState = new MasterChaseState(this, StateMachine, "ChaseBool");
    }

    private void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();

        masterMovement = GetComponent<MasterMovement>();

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
}
