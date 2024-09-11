using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D),typeof(TouchDirection), typeof (Damageable))]
public class Knight : MonoBehaviour
{
    public float walkSpeed = 3f;
    TouchDirection direction;
    Rigidbody2D rb;
    public DetectionZone attackZone;
    public DetectionZone cliffDetection;
    public enum WalkableDirection { Right, Left };
    Animator animator;
    Damageable damageable;

    private WalkableDirection _walkDirection;
    private Vector2 walkDirectionVector = Vector2.right;

    public WalkableDirection WalkDirection
    {
        get { return _walkDirection; }
        set { 
            
            if(_walkDirection !=value)
            {
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);

                if(value == WalkableDirection.Left )
                {
                    walkDirectionVector = Vector2.left;
                }    
                else if(value == WalkableDirection.Right ) { walkDirectionVector = Vector2.right; }
            }
            _walkDirection = value; }
    }
    public bool _hasTarget = false;
    public bool HasTarget { get { return _hasTarget; }
        private set
        {
            _hasTarget = value;
            animator.SetBool(AnimationStrings.hasTarget, value);

        } }

    public bool CanMove
    {
        get
        {
            return animator.GetBool(AnimationStrings.canMove);
        }

    }

    public float AttackCooldown
    {
        get
        {
            return animator.GetFloat(AnimationStrings.attackCooldown);
        }
        private set
        {
            animator.SetFloat(AnimationStrings.attackCooldown, Mathf.Max(value,0));
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        direction = GetComponent<TouchDirection>();
        animator = GetComponent<Animator>();    
        damageable = GetComponent<Damageable>();
    }


    // Update is called once per frame
    void Update()
    {
        HasTarget = attackZone.detectedColliders.Count > 0;
        if(AttackCooldown>0)
        AttackCooldown -= Time.deltaTime;
    }
    private void FixedUpdate()
    {
      if ((direction.IsOnWall || cliffDetection.detectedColliders.Count == 0) && direction.IsGrounded)
            {
            FlipDirection();
        }
        if(!damageable.LockVelocity)
        rb.velocity = new Vector2(walkSpeed * walkDirectionVector.x * (CanMove?1:0), rb.velocity.y);
    }
    private void FlipDirection()
    {
        if (direction.flipOK)
        {
            if (WalkDirection == WalkableDirection.Left)
                WalkDirection = WalkableDirection.Right;
            else if (WalkDirection == WalkableDirection.Right)
                WalkDirection = WalkableDirection.Left;
            else
                Debug.LogError("Current walkable direction is not set to right or left");
        }
    }

    public void OnHit(int damage, Vector2 knockback)
    {
        if (knockback.x * rb.velocity.x > 0)
            FlipDirection();
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }
}
