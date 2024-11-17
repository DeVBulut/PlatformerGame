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
        if(rb.velocity.y < VariableList.peakThreshold && rb.velocity.y > -VariableList.peakThreshold && !playerController.isGrounded())
        {
            playerStateMachine.ChangeState(playerController.fallState);
        }
    }

    public override void PhysicsUpdate()
    {

    }

    public override void ExitState(PlayerState newState)
    {

    }
}
