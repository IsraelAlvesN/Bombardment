using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Public
    public float movementSpeed = 500f;
    public float jumpPower = 10f;
    public float jumpMovementFactor = 1f;
    // State machine
    [HideInInspector] public StateMachine stateMachine;
    [HideInInspector] public Idle idleState;
    [HideInInspector] public Walking walkingState;
    [HideInInspector] public Jump jumpingState;
    // Internal Properties
    [HideInInspector] public Vector2 movementVector;
    [HideInInspector] public bool hasJumpInput;
    [HideInInspector] public bool isGrounded;
    [HideInInspector] public Rigidbody thisRigidbody;
    [HideInInspector] public Collider thisCollider;
    //animator
    [HideInInspector] public Animator thisAnimator;

    private void Awake()
    {
        thisRigidbody = GetComponent<Rigidbody>();
        thisAnimator = GetComponent<Animator>();
        thisCollider = GetComponent<Collider>();
    }

    void Start()
    {
        //StateMachine
        stateMachine = new StateMachine();
        idleState = new Idle(this); //this is the player controller reference
        walkingState = new Walking(this);
        jumpingState = new Jump(this);
        stateMachine.ChangeState(idleState);
    }

    void Update()
    {
        //create input vector
        bool isUp = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
        bool isDown = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);
        bool isRight = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);
        bool isLeft = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
        float inputX = isRight ? 1 : isLeft ? -1 : 0;
        float inputY = isUp ? 1 : isDown ? -1 : 0;
        movementVector = new Vector2(inputX, inputY);
        hasJumpInput = Input.GetKey(KeyCode.Space);

        //velocity between 0 and 1
        float velocity = thisRigidbody.velocity.magnitude;
        float velocityRate = velocity / movementSpeed;

        //detect ground
        DetectGround();

        thisAnimator.SetFloat("fVelocity", velocityRate);

        stateMachine.Update();
    }

    private void LateUpdate()
    {
        //StateMachine
        stateMachine.LateUpdate();
    }
    private void FixedUpdate()
    {
        //StateMachine
        stateMachine.FixedUpdate();
    }

    public Quaternion GetForward()
    {
        Camera camera = Camera.main;
        float eulerY = camera.transform.eulerAngles.y;
        return Quaternion.Euler(0, eulerY, 0);
    }

    public void RotateBodyToFaceInput()
    {
        if(movementVector.IsZero()) return;
        //Calculate Rotation
        Camera camera = Camera.main;
        float eulerY = camera.transform.eulerAngles.y;
        Vector3 inputVector = new Vector3(movementVector.x, 0, movementVector.y);
        Quaternion q1 = Quaternion.LookRotation(inputVector, Vector3.up);
        Quaternion q2 = Quaternion.Euler(0, eulerY, 0);
        Quaternion toRotation = q1 * q2;
        Quaternion newRotation =  Quaternion.LerpUnclamped(transform.rotation, toRotation, 0.15f);

        //apply rotation
        thisRigidbody.MoveRotation(newRotation);
    }

    private void DetectGround()
    {
        //reset flag
        isGrounded = false;

        Vector3 origin = transform.position;
        Vector3 direction = Vector3.down;
        Bounds bounds = thisCollider.bounds;
        float radius = bounds.size.x * 0.33f;
        float maxDistance = bounds.size.y * 0.25f;
        if(Physics.SphereCast(origin, radius, direction, out var hitInfo, maxDistance))
        {
            GameObject hitObject = hitInfo.transform.gameObject;
            if (hitObject.CompareTag("Platform"))
            {
                isGrounded = true;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 origin = transform.position;
        Vector3 direction = Vector3.down;
        Bounds bounds = thisCollider.bounds;
        float radius = bounds.size.x * 0.33f;
        float maxDistance = bounds.size.y * 0.25f;

        //Draw ray
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(new Ray(origin, direction * maxDistance));

        //draw origin
        Gizmos.color = Color.gray;
        Gizmos.DrawSphere(origin, 0.1f);

        //Draw sphere
        Vector3 spherePosition = direction * maxDistance + origin;
        Gizmos.color = isGrounded ? Color.green : Color.cyan;
        Gizmos.DrawSphere(spherePosition, radius);
    }
}
