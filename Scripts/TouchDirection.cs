using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchDirection : MonoBehaviour
{
    public ContactFilter2D contactFilter;
    public float groundDistance = 0f;
    public float wallDistance = 0.2f;
    public float ceilingDistance = 0.05f;
    public float groundTimer = 0.1f;
    private float timeElapsed = 0f;
    public bool flipOK = false;
    CapsuleCollider2D capsuleCollider;
    RaycastHit2D[] groundHits = new RaycastHit2D[5];
    RaycastHit2D[] wallHits = new RaycastHit2D[5];
    RaycastHit2D[] ceilingHits = new RaycastHit2D[5];
    Animator animator;

    [SerializeField]
    private bool _isGrounded = false;
    public bool IsGrounded
    {
        get
        {
            return _isGrounded;
        }
        private set
        {
            _isGrounded = value;
            if (_isGrounded==false)
            {
                flipOK = false;
                timeElapsed = 0;
            }
            else
            {

                if (timeElapsed >= groundTimer)
                    flipOK = true;
                else timeElapsed += Time.deltaTime;
            }
            animator.SetBool(AnimationStrings.isGrounded, value);
        }
    }
    [SerializeField]
    private bool _isOnWall = false;
    public bool IsOnWall
    {
        get
        {
            return _isOnWall;
        }
        private set
        {
            _isOnWall = value;
            animator.SetBool(AnimationStrings.isOnWall, value);
        }
    }
    [SerializeField]
    private bool _isOnCeiling = false;
    private Vector2 wallCheckDirection => gameObject.transform.localScale.x >0 ? Vector2.right : Vector2.left;

    public bool IsOnCeiling
    {
        get
        {
            return _isOnCeiling;
        }
        private set
        {
            _isOnCeiling = value;
            animator.SetBool(AnimationStrings.isOnCeiling, value);
        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       IsGrounded= capsuleCollider.Cast(Vector2.down, contactFilter, groundHits, groundDistance)>0;
        IsOnWall = capsuleCollider.Cast(wallCheckDirection, contactFilter, wallHits, wallDistance) > 0;
        IsOnCeiling = capsuleCollider.Cast(Vector2.up, contactFilter, ceilingHits, ceilingDistance) > 0;
    }


}
