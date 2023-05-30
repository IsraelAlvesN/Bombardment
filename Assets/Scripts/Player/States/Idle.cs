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

    }

    public override void Exit()
    {
        base.Exit();
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

        if (controller.hasJumpInput)
        {
            //jumping
            controller.stateMachine.ChangeState(controller.jumpingState);
            return;
        }
        if (!controller.movementVector.IsZero())
        {
            //walking
            controller.stateMachine.ChangeState(controller.walkingState);
            return;
        }
    }

    
}
