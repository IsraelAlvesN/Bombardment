using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : State
{
    private PlayerController controller;
    public Idle(PlayerController controller) : base("Idle")
    {
        this.controller = controller;
    }

    public override void Enter()
    {
        base.Enter();

        //Anything else
        Debug.Log("Idle");
    }

    public override void Exit()
    {
        base.Exit();
        Debug.Log("Saiu Idle");
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }


    public override void LateUpdate()
    {
        base.LateUpdate();
    }

    public override void Update()
    {
        base.Update();

        if (!controller.movementVector.IsZero())
        {
            //walking
            controller.stateMachine.ChangeState(controller.walkingState);
            return;
        }
    }

    
}
