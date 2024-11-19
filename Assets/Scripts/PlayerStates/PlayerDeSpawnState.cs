using UnityEngine;

public class PlayerDeSpawnState : PlayerState
{
    public PlayerDeSpawnState(PlayerController playerController, PlayerStateMachine playerStateMachine, Animator animator, Rigidbody2D rigidbody) : base(playerController, playerStateMachine, animator, rigidbody)
    {
    }

    public override void EnterState()
    {
        playerController.canMove = false; 
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        animator.Play(Anim.Die);
        Debug.Log(this.ToString());
    }

    public override void FrameUpdate()
    {
        
    }

    public override void PhysicsUpdate()
    {

    }

    public override void ExitState(PlayerState newState)
    {
        playerController.canMove = true; 
    }
}
