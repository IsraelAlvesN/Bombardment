using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : State
{
    private PlayerController controller;
    private bool hasJumped;
    private float cooldown;

    public Jump(PlayerController controller) : base("Jump")
    {
        this.controller = controller;
    }

    public override void Enter()
    {
        base.Enter();

        //reset variable
        hasJumped = false;
        cooldown = 0.5f;
}

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        cooldown -= Time.deltaTime;

        //switch to idle
        if (hasJumped && controller.isGrounded)
        {
            controller.stateMachine.ChangeState(controller.idleState);
        }
    }

    private void ApplyImpulse()
    {
        Vector3 forceVector = Vector3.up * controller.jumpPower;
        controller.thisRigidbody.AddForce(forceVector, ForceMode.Impulse);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if (!hasJumped)
        {
            hasJumped = true;
            ApplyImpulse();
        }
    }


    public override void LateUpdate()
    {
        base.LateUpdate();
    }

    
}
