using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDoubleJumpState : PlayerState
{
    public PlayerDoubleJumpState(PlayerController playerController, PlayerStateMachine playerStateMachine, Animator animator, Rigidbody2D rigidbody) : base(playerController, playerStateMachine, animator, rigidbody)
    {
    }

    public override void EnterState()
    {
        Vector3 vector3 = new Vector3(rb.velocity.x, 0, 0);
        rb.velocity = vector3;
        animator.Play(Anim.DoubleJump);
        playerController.doubleJumpCharge -= 1;
        rb.AddForce(Vector2.up * VariableList.doubleJumpPower);
        Debug.Log(this.ToString());
    }

    public override void FrameUpdate()
    {
        if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.99)
        {
            if(rb.velocity.y < 0 && !playerController.isGrounded())
            {
                playerStateMachine.ChangeState(playerController.fallState);
            }
            else if(rb.velocity.y > 0 && !playerController.isGrounded())
            {
                playerStateMachine.ChangeState(playerController.riseState);
            }
        }
        else if(playerController.isGrounded())
        {
            playerStateMachine.ChangeState(playerController.idleState);
        }
    }

    public override void PhysicsUpdate()
    {
    }

    public override void ExitState(PlayerState newState)
    {
    }
}
