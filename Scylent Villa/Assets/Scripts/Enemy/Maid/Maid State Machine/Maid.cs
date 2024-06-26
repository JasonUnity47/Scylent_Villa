using Pathfinding;
using System.Collections;
using UnityEngine;

public class Maid : MonoBehaviour
{
    // Declaration
    // Script Reference
    public EvolutionSystem evolutionSystem { get; private set; }

    public AIPath aIPath { get; private set; }

    public EnemyPatrol enemyPatrol { get; private set; }

    public MaidFOV maidFOV { get; private set; }

    // State Machine Reference
    public MaidStateMachine StateMachine { get; private set; }

    public MaidIdleState IdleState { get; private set; }

    public MaidPatrolState PatrolState { get; private set; }

    public MaidChaseState ChaseState { get; private set; }

    public Animator Anim { get; private set; }

    // Movement
    [Header("Movement")]
    public bool isMoving = false;

    public bool Front { get; private set; }

    public bool Back { get; private set; }

    public bool Left { get; private set; }

    public bool Right { get; private set; }

    private float activationOffset = 0.5f;

    private float deactivationOffset = 0.2f;

    private bool isStunned = false;

    private bool once = false;

    private bool once2 = false;

    // Object Reference
    [Header("Object Reference")]
    public GameObject childObject; // Reference to the child GameObject to deactivate

    public GameObject objectToInstantiate; // The object you want to instantiate

    public string childTransformName; // Name of the child transform to instantiate the object at

    public GameObject selfLight; // Light that show the enemy itself.

    private void Awake()
    {
        // Get reference.s
        Anim = GetComponent<Animator>();

        evolutionSystem = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<EvolutionSystem>();
        aIPath = GetComponent<AIPath>();
        enemyPatrol = GetComponent<EnemyPatrol>();
        maidFOV = GetComponentInChildren<MaidFOV>();

        StateMachine = new MaidStateMachine();

        IdleState = new MaidIdleState(this, StateMachine);
        PatrolState = new MaidPatrolState(this, StateMachine);
        ChaseState = new MaidChaseState(this, StateMachine);
    }

    private void Start()
    {
        StateMachine.InitializeState(IdleState);

        childObject = transform.GetChild(0).gameObject;
    }

    private void Update()
    {
        StateMachine.CurrentState.LogicalUpdate();

        EvolveStage();

        CheckFOV();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }

    // Check whether the enemy is moving.
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

    // Perform animation.
    public void AnimationChange()
    {
        // Check if the enemy is moving.
        isMoving = aIPath.velocity.magnitude > 0.1f; // Adjust the threshold as needed.

        // Look Left.
        if (aIPath.velocity.x < -activationOffset && !Left)
        {
            Left = true;
            Front = Back = Right = false;
        }
        else if (aIPath.velocity.x > deactivationOffset && Left)
        {
            Left = false;
        }

        // Look Right.
        if (aIPath.velocity.x > activationOffset && !Right)
        {
            Right = true;
            Front = Back = Left = false;
        }
        else if (aIPath.velocity.x < -deactivationOffset && Right)
        {
            Right = false;
        }

        // Look Back.
        if (aIPath.velocity.y > activationOffset && !Back)
        {
            Back = true;
            Front = Left = Right = false;
        }
        else if (aIPath.velocity.y < -deactivationOffset && Back)
        {
            Back = false;
        }

        // Look Front.
        if (aIPath.velocity.y < -activationOffset && !Front)
        {
            Front = true;
            Back = Left = Right = false;
        }
        else if (aIPath.velocity.y > deactivationOffset && Front)
        {
            Front = false;
        }

        // Update animation parameters.
        Anim.SetBool("MoveBool", isMoving);
        Anim.SetFloat("Horizontal", Mathf.Clamp(aIPath.velocity.x, -1, 1));
        Anim.SetFloat("Vertical", Mathf.Clamp(aIPath.velocity.y, -1, 1));

        // Update animation booleans.
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

        // Disable FOV.
        DeactivateChildObject();

        // Stop movement.
        aIPath.canMove = false;

        GameObject instantiatedObject = null;
        Transform childTransform = transform.Find(childTransformName);
        instantiatedObject = Instantiate(objectToInstantiate, childTransform.position, Quaternion.identity);
        instantiatedObject.transform.parent = childTransform;

        yield return new WaitForSeconds(duration);

        Destroy(instantiatedObject);

        // Re-enable FOV.
        ReactivateChildObject();

        // Resume movement.
        aIPath.canMove = true;

        isStunned = false;
    }

    // Check whether the fov is active.
    // If the fov is unactive then no detection mark appears.
    void CheckFOV()
    {
        if (!maidFOV.gameObject.activeSelf || GameObject.FindGameObjectWithTag("Player") == null)
        {
            maidFOV.isDetected = false;
            maidFOV.ShowDetection();
        }

        return;
    }

    // Method to deactivate the child object
    public void DeactivateChildObject()
    {
        childObject.SetActive(false);
        selfLight.SetActive(false);
    }

    // Method to reactivate the child object
    public void ReactivateChildObject()
    {
        childObject.SetActive(true);
        selfLight.SetActive(true);
    }

    public void EvolveStage()
    {
        //Stage 2
        if (evolutionSystem.stage2 && !once)
        {
            // Activate only once.
            once = true;

            FindObjectOfType<AudioManager>().Stop("Heartbeat"); // Stop the heartbeat sound.

            // Store original value.
            float originalSpeed = aIPath.maxSpeed;

            // Enemy should stop moving if evolution is started.
            aIPath.canSearch = false;
            aIPath.maxSpeed = 0;

            // Disable FOV and self light.
            DeactivateChildObject();

            Anim.SetBool("DeadBool1", true);

            StartCoroutine(WaitEvovle(originalSpeed));
        }

        //Stage 3
        if (evolutionSystem.stage3 && !once2)
        {
            // Activate only once.
            once2 = true;

            FindObjectOfType<AudioManager>().Stop("Heartbeat"); // Stop the heartbeat sound.

            // Store original value.
            float originalSpeed = aIPath.maxSpeed;

            // Enemy should stop moving if evolution is started.
            aIPath.canSearch = false;
            aIPath.maxSpeed = 0;

            // Disable FOV and self light.
            DeactivateChildObject();

            Anim.SetBool("DeadBool2", true);

            StartCoroutine(WaitEvovle2(originalSpeed));
        }
    }

    IEnumerator WaitEvovle(float originalSpeed)
    {
        yield return new WaitForSeconds(3f);

        Anim.SetBool("DeadBool1", false);

        yield return new WaitForSeconds(0.8f);

        Anim.SetBool("Stage2", true);

        // Re-enable FOV and self light.
        ReactivateChildObject();

        aIPath.canSearch = true;
        aIPath.maxSpeed = originalSpeed;
    }

    IEnumerator WaitEvovle2(float originalSpeed)
    {
        yield return new WaitForSeconds(3f);

        Anim.SetBool("DeadBool2", false);

        yield return new WaitForSeconds(0.8f);

        Anim.SetBool("Stage3", true);

        // Re-enable FOV and self light.
        ReactivateChildObject();

        aIPath.canSearch = true;
        aIPath.maxSpeed = originalSpeed;
    }
}