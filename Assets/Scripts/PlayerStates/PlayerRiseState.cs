using UnityEngine;

public class PlayerRiseState : PlayerState
{
    public PlayerRiseState(PlayerController playerController, PlayerStateMachine playerStateMachine, Animator animator, Rigidbody2D rigidbody) : base(playerController, playerStateMachine, animator, rigidbody)
    {
    }
    
    public override void EnterState()
    {
        animator.Play(Anim.Rise);
        Debug.Log(this.ToString());
    }

    public override void FrameUpdate()
    {
        //Debug.Log(rb.velocity.y);
        if(rb.velocity.y < 0.2 && !playerController.isGrounded())
        {
            playerStateMachine.ChangeState(playerController.fallState);
        }
        if(Input.GetKeyDown(KeyCode.Space) && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.1 && playerController.doubleJumpCharge == 1)
        {
            playerStateMachine.ChangeState(playerController.doubleJump);
        }
    }

    public override void PhysicsUpdate()
    {

    }

    public override void ExitState(PlayerState newState)
    {

    }
}
