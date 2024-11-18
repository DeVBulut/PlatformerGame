using UnityEngine;

public class PlayerDeathState : PlayerState
{
    public PlayerDeathState(PlayerController playerController, PlayerStateMachine playerStateMachine, Animator animator, Rigidbody2D rigidbody) : base(playerController, playerStateMachine, animator, rigidbody)
    {
    }

    public override void EnterState()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        animator.Play(Anim.Die);
        Debug.Log(this.ToString());
    }

    public override void FrameUpdate()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.99 && animator.GetCurrentAnimatorStateInfo(0).IsName("Die"))
        {
            playerStateMachine.ChangeState(playerController.spawnState);
        }
    }

    public override void PhysicsUpdate()
    {

    }

    public override void ExitState(PlayerState newState)
    {

    }
}
