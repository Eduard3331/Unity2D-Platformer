using System.Collections;
using System.Collections.Generic;
//using UnityEditorInternal;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchDirection), typeof(Damageable))]

public class PlayerController : MonoBehaviour
{
    public float runSpeed = 5f;
    public float sprintSpeed = 10f;
    public float timeToSprint = 3f;
    private float timeElapsed = 0f;
    public float jumpImpulse = 10f;
    Vector2 moveInput;
    TouchDirection touchDirection;



    Damageable damageable;
    public float CurrentMoveSpeed
    {
        get{

            if (IsMoving && CanMove && !touchDirection.IsOnWall)
            {
                if (IsSprinting)
                    return sprintSpeed;
              else return runSpeed;
            }
            else return 0;
        }
    }

    private bool _isSprinting = false;
    public bool IsSprinting
    {
        get
        {
            return _isSprinting;

        }
        private set
        {
            _isSprinting = value;
      
            animator.SetBool(AnimationStrings.isSprinting, value);
        }
    }
    private bool _isMoving = false;
    public bool IsMoving
    {
        get
        {
            return _isMoving;

        }
        private set
        {
            _isMoving = value;
            animator.SetBool(AnimationStrings.isMoving, value);
        }
    }
    public bool _isFacingRight = true;
    public bool IsFacingRight
    {
        get
        {
            return _isFacingRight;
        }
        private set
        {
            if(_isFacingRight!=value )
            {
                transform.localScale *= new Vector2(-1, 1);
            }
            _isFacingRight = value;
        }
    }

    public bool CanMove
    {
        get
        {
            return animator.GetBool(AnimationStrings.canMove);
        }

    }
    Rigidbody2D rb;
    Animator animator;
  
    public bool IsAlive
    {
        get
        {
            return animator.GetBool(AnimationStrings.isAlive);
        }
    }



    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); 
        touchDirection= GetComponent<TouchDirection>();
        damageable = GetComponent<Damageable>();
    }

    private void SprintCheck()
    {
        if (IsMoving && rb.velocity.x != 0)
        {

            if (timeElapsed >= timeToSprint)
            {
                IsSprinting = true;
                timeElapsed = 0;
            }
            else if (!IsSprinting) timeElapsed += Time.deltaTime;
        }
        else
        {
            IsSprinting = false;
            timeElapsed = 0;
        }
    }
    private void FixedUpdate()
    {

        if(!damageable.LockVelocity)
        rb.velocity= new Vector2(moveInput.x * CurrentMoveSpeed, rb.velocity.y);
        if (animator.GetBool(AnimationStrings.isGrounded))
        {
            SprintCheck();
            SetFacingDirection(rb.velocity);
        }
        animator.SetFloat(AnimationStrings.yVelocity, rb.velocity.y);

            
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        IsMoving = moveInput != Vector2.zero;
        if (animator.GetBool(AnimationStrings.isGrounded)) SetFacingDirection(moveInput);

    }
    private void SetFacingDirection(Vector2 moveInput)
    {
        if (IsAlive && CanMove)
        {
            if (moveInput.x > 0 && !IsFacingRight)
                IsFacingRight = true;
            else if (moveInput.x < 0 && IsFacingRight)
                IsFacingRight = false;
        }
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.started && touchDirection.IsGrounded && CanMove)
        {
            animator.SetTrigger(AnimationStrings.jumpTrigger);
            if(IsSprinting)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpImpulse+2f);
            }
            else
            rb.velocity = new Vector2(rb.velocity.x, jumpImpulse);
        }
    }
    public void OnAttack(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            animator.SetTrigger(AnimationStrings.attackTrigger);

        }
    }

    public void OnHit(int damage, Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }
    public void OnRangedAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            animator.SetTrigger(AnimationStrings.rangedAttackTrigger);

        }
    }
}
