using UnityEngine;

public class PlayerRunState : PlayerState
{
    float tempInput;

    public PlayerRunState(PlayerController playerController, PlayerStateMachine playerStateMachine, Animator animator, Rigidbody2D rb) : base(playerController, playerStateMachine, animator, rb)
    {

    }
    public override void EnterState()
    {
        animator.Play(Anim.Run);
        Debug.Log(this.ToString());
    }
    
    public override void PhysicsUpdate()
    {

    }

    public override void FrameUpdate()
    {
        float InputAxis = Input.GetAxisRaw("Horizontal");
        
        if(!playerController.isGrounded())
        {
            playerStateMachine.ChangeState(playerController.fallState);
        }
        else if(InputAxis == 0 && Mathf.Abs(rb.velocity.x) < 1f)
        {
            playerStateMachine.ChangeState(playerController.idleState);
        }

        tempInput = -InputAxis;
    }

    public override void ExitState(PlayerState newState)
    {

    }
}
