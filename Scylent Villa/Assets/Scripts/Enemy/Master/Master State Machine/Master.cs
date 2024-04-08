using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Master : MonoBehaviour
{
    // Declaration
    // Script Reference
    public EvolutionSystem evolutionSystem { get; private set; }

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

    private bool isStunned = false;

    private bool once = false;

    public GameObject childObject; // Reference to the child GameObject to deactivate

    private void Awake()
    {
        Anim = GetComponent<Animator>();

        evolutionSystem = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<EvolutionSystem>();
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
        Front = true;
        Anim.SetBool("FrontBool", Front);

        StateMachine.InitializeState(IdleState);

        childObject = transform.GetChild(0).gameObject;
    }

    private void Update()
    {
        StateMachine.CurrentState.LogicalUpdate();

        EvolveStage();
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

    public void Stun(float duration)
    {
        if (!isStunned)
        {
            StartCoroutine(StunCoroutine(duration));
        }
    }

    private IEnumerator StunCoroutine(float duration)
    {
        isStunned = true;

        // disable FOV
        DeactivateChildObject();

        // Stop movement
        aIPath.canMove = false;

        // Play stun animation or effects (if any)

        yield return new WaitForSeconds(duration);

        // Re-enable FOV
        ReactivateChildObject();

        // Resume movement
        aIPath.canMove = true;

        isStunned = false;
    }

    // Method to deactivate the child object
    public void DeactivateChildObject()
    {
        childObject.SetActive(false);
    }

    // Method to reactivate the child object
    public void ReactivateChildObject()
    {
        childObject.SetActive(true);
    }

    public void EvolveStage()
    {
        //Stage 2
        if (evolutionSystem.stage2 && !once)
        {
            // Activate only once.
            once = true;

            // Store original value.
            float originalSpeed = aIPath.maxSpeed;

            // Enemy should stop moving if evolution is started.
            aIPath.canSearch = false;
            aIPath.maxSpeed = 0;

            StartCoroutine(WaitEvovle(originalSpeed));
        }
    }

    IEnumerator WaitEvovle(float originalSpeed)
    {
        yield return new WaitForSeconds(5f);

        Anim.SetBool("DeadBool1", false);
        Anim.SetBool("Stage2", true);

        aIPath.canSearch = true;
        aIPath.maxSpeed = originalSpeed;
    }
}
