using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Son : MonoBehaviour
{
    // Script Reference
    public SonMovement sonMovement { get; private set; }

    public SonStateMachine StateMachine { get; private set; }

    public SonIdleState IdleState { get; private set; }

    public SonPatrolState PatrolState { get; private set; }

    public SonChaseState ChaseState { get; private set; }

    public Rigidbody2D Rb { get; private set; }

    public Animator Anim { get; private set; }

    private void Awake()
    {
        StateMachine = new SonStateMachine();

        IdleState = new SonIdleState(this, StateMachine, "IdleBool");
        PatrolState = new SonPatrolState(this, StateMachine, "PatrolBool");
        ChaseState = new SonChaseState(this, StateMachine, "ChaseBool");
    }

    private void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();

        sonMovement = GetComponent<SonMovement>();

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
