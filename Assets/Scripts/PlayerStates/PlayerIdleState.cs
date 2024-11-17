using UnityEngine;

public class PlayerIdleState : PlayerState
{

    public PlayerIdleState(PlayerController playerController, PlayerStateMachine playerStateMachine, Animator animator, Rigidbody2D rb) : base(playerController, playerStateMachine, animator, rb)
    {

    }

    public override void PhysicsUpdate()
    {

    }

    public override void FrameUpdate()
    {
        float InputAxis = Input.GetAxisRaw("Horizontal");
        if(InputAxis != 0 && Mathf.Abs(rb.velocity.x) < 6f)
        {
            playerStateMachine.ChangeState(playerController.runState);
        }
        else if(!playerController.isGrounded())
        {
            playerStateMachine.ChangeState(playerController.fallState);
        }
    }

    public override void EnterState()
    {
        animator.Play(Anim.Idle);
        Debug.Log(this.ToString());
    }

    public override void ExitState(PlayerState newState)
    {

    }
}
