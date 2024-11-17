using UnityEngine;
public class PlayerState : MonoBehaviour
{
    protected PlayerController playerController;
    protected PlayerStateMachine playerStateMachine;
    protected Animator animator;
    protected Rigidbody2D rb;
    public PlayerState(PlayerController playerController, PlayerStateMachine playerStateMachine, Animator animator, Rigidbody2D rigidbody)
    {
        this.playerController = playerController;
        this.playerStateMachine = playerStateMachine;
        this.animator = animator;
        this.rb = rigidbody;
    }

    public PlayerState(PlayerController playerController, PlayerStateMachine playerStateMachine)
    {
        this.playerController = playerController;
        this.playerStateMachine = playerStateMachine;
    }

    public void Setup(PlayerController playerController, PlayerStateMachine playerStateMachine, Animator animator, Rigidbody2D rigidbody)
    {
        this.playerController = playerController;
        this.playerStateMachine = playerStateMachine;
        this.animator = animator;
        this.rb = rigidbody;
    }
    public virtual void EnterState(){}
    public virtual void FrameUpdate(){}
    public virtual void PhysicsUpdate(){}
    public virtual void ExitState(PlayerState newState){}
}