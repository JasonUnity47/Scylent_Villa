using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maid : MonoBehaviour
{
    // Script Reference
    public MaidMovement maidMovement { get; private set; }

    public MaidStateMachine StateMachine { get; private set; }

    public MaidIdleState IdleState { get; private set; }

    public MaidPatrolState PatrolState { get; private set; }

    public MaidChaseState ChaseState { get; private set; }

    public Rigidbody2D Rb { get; private set; }

    public Animator Anim { get; private set; }

    private void Awake()
    {
        StateMachine = new MaidStateMachine();

        IdleState = new MaidIdleState(this, StateMachine, "IdleBool");
        PatrolState = new MaidPatrolState(this, StateMachine, "PatrolBool");
        ChaseState = new MaidChaseState(this, StateMachine, "ChaseBool");
    }

    private void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();

        maidMovement = GetComponent<MaidMovement>();

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
