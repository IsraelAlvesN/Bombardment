using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Public
    public float movementSpeed = 500;
    // State machine
    [HideInInspector] public StateMachine stateMachine;
    [HideInInspector] public Idle idleState;
    [HideInInspector] public Walking walkingState;
    // Internal Properties
    [HideInInspector] public Vector2 movementVector;
    [HideInInspector] public Rigidbody thisRigidbody;

    private void Awake()
    {
        thisRigidbody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        //StateMachine
        stateMachine = new StateMachine();
        idleState = new Idle(this); //this is the player controller reference
        walkingState = new Walking(this);
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

    void OnGUI()
    {
        GUI.Label(new Rect(5, 5, 200, 50), stateMachine.currentStateName);
    }
}
