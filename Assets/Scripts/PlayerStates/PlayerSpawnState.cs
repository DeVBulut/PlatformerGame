using UnityEngine;

public class PlayerSpawnState : PlayerState
{
    public PlayerSpawnState(PlayerController playerController, PlayerStateMachine playerStateMachine, Animator animator, Rigidbody2D rigidbody) : base(playerController, playerStateMachine, animator, rigidbody)
    {
    }

    public override void EnterState()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        animator.Play(Anim.Spawn);
        Debug.Log(this.ToString());
    }

    public override void FrameUpdate()
    {
        if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.99)
        {
            playerStateMachine.ChangeState(playerController.fallState);
        }
    }

    public override void PhysicsUpdate()
    {

    }

    public override void ExitState(PlayerState newState)
    {
        rb.constraints = RigidbodyConstraints2D.None;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
}
