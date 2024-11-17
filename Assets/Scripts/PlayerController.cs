using System;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.UI;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    //private bool alive = true;
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float airSpeed = 4f;
    private float InputAxis;
    public bool canMove;
    public LayerMask groundLayer;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D groundCheck;

    #region State Machine Variables 
    public PlayerStateMachine stateMachine;
    public PlayerIdleState idleState;
    public PlayerRunState runState;
    public PlayerRiseState riseState;
    public PlayerFallState fallState;
    public PlayerHitState hitState; 

    #endregion

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        groundCheck = transform.GetChild(0).GetComponent<BoxCollider2D>();

        idleState.Setup(this, stateMachine, animator, rb);
        runState.Setup(this, stateMachine, animator, rb);
        riseState.Setup(this, stateMachine, animator, rb);
        fallState.Setup(this, stateMachine, animator, rb);
        hitState.Setup(this, stateMachine, animator, rb);
    }

    void Start()
    {
        canMove = true;
        stateMachine.Initialize(idleState);
    }

    void Update()
    {
        stateMachine.CurrentPlayerState.FrameUpdate();
        Flip();
    }

    void FixedUpdate()
    {
        stateMachine.CurrentPlayerState.PhysicsUpdate();
        Move();
    }
    
    //Movement Code
    public void Move() 
    {
        if (canMove) {

            //Input Axis
            InputAxis = Input.GetAxisRaw("Horizontal");

            //Velocities for Movement
            float variableSpeed = isGrounded() == true ? runSpeed : airSpeed;
            float variableSmoothing = VariableList.movementSmoothing + (InputAxis != 0 ? VariableList.movementSmoothing : 0f);


            Vector2 targetVelocity = new Vector2(InputAxis * variableSpeed, rb.velocity.y);

            //zero variable
            Vector3 zeroVector = Vector2.zero;
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref zeroVector, isGrounded() ? variableSmoothing : variableSmoothing + 0.1f);
        }
    }

    public void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && canJump()){ 

            //add force in positive upwards direction
            rb.AddForce(Vector2.up * VariableList.jumpPower);
            stateMachine.ChangeState(riseState);
        }
    }


    public void Flip()
    {
        if (InputAxis < 0f)
        {
            if (spriteRenderer.flipX == false)
            {
                spriteRenderer.flipX = true;
            }
        }
        else if (InputAxis > 0f)
        {
            if (spriteRenderer.flipX == true)
            {
                spriteRenderer.flipX = false;
            }
        }
        
    }

    //Boolean Functions
    public bool isGrounded()
    {
        return (Physics2D.OverlapAreaAll(groundCheck.bounds.min, groundCheck.bounds.max, groundLayer).Length > 0) ? true : false;
    }

    public bool canJump()
    {
        return isGrounded() && canMove;
    }

    public bool isRunning()
    {
        if(stateMachine.CurrentPlayerState == runState && Mathf.Abs(rb.velocity.x) > 2) 
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
