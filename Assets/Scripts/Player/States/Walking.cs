using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walking : State
{
    private PlayerController controller;
    public Walking(PlayerController controller) : base("Walking")
    {
        this.controller = controller;
    }
    public override void Enter()
    {
        base.Enter();

    }

    public override void Exit()
    {
        base.Exit();
        Debug.Log("Saiu walking");
    }

    public override void Update()
    {
        base.Update();

        if (controller.movementVector.IsZero())
        {
            //switch to idle
            controller.stateMachine.ChangeState(controller.idleState);
            return;
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        //Create walk vector
        Vector3 walkVector = new Vector3(controller.movementVector.x, 0, controller.movementVector.y);
        walkVector = controller.GetForward() * walkVector;
        walkVector *= controller.movementSpeed;

        //Apply input to character
        controller.thisRigidbody.AddForce(walkVector, ForceMode.Force);
        
        //to Rotate character
        controller.RotateBodyToFaceInput();
    }


    public override void LateUpdate()
    {
        base.LateUpdate();
    }

    
}
