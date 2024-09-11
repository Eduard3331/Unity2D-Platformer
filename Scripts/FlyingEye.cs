using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FlyingEye : MonoBehaviour
{
    public float flightSpeed = 4f;
    public float waypointReachedDistance= 0.1f ;
    public DetectionZone biteDetectionZone;
    public List<Transform> waypoints;
    

    Animator animator;
    Rigidbody2D rb;
    Damageable damageable;
    Collider2D col;
    Transform nextWaypoint;
    TouchDirection direction;
    int waypointNum = 0;
    private bool _hasTarget = false;
 

    public bool HasTarget
    {
        get { return _hasTarget; }
        private set
        {
            _hasTarget = value;
            animator.SetBool(AnimationStrings.hasTarget, value);

        }
    
   }
    public bool CanMove
    {
        get
        {
            return animator.GetBool(AnimationStrings.canMove);
        }

    }
    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        damageable = GetComponent<Damageable>();
        col = GetComponent<Collider2D>();
        direction= GetComponent<TouchDirection>();
    }
    // Start is called before the first frame update
    void Start()
    {
        nextWaypoint = waypoints[waypointNum];
        Debug.Log("Eye collision: " + rb.attachedColliderCount);
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat(AnimationStrings.yVelocity, rb.velocity.y);
        HasTarget = biteDetectionZone.detectedColliders.Count > 0;
    }

    private void FixedUpdate()
    {
        if(damageable.IsAlive)
        {
            if(CanMove)
            {
                Flight();
            }
            else
            {
                rb.velocity = Vector2.zero;
            }
        }
        else
        {
            col.offset = new Vector2(0,-1.1f); //manipulating the eye's collider so that it visually matches with its new falling and death aninations
            rb.gravityScale = 1f;
            rb.velocity = new Vector2(0, rb.velocity.y);
            
        }
    }

    private void Flight()
    {
        Vector2 directionToWaypoint = (nextWaypoint.position - transform.position).normalized;
        float distance = Vector2.Distance(nextWaypoint.position, transform.position);
        if (rb.velocity.x * directionToWaypoint.x < 0)
            transform.localScale *= new Vector2(-1,1);
        rb.velocity = directionToWaypoint * flightSpeed;
        if(distance<= waypointReachedDistance)
        {
            waypointNum++;
            if(waypointNum>=waypoints.Count)
            {
                waypointNum = 0;
            }
            nextWaypoint = waypoints[waypointNum];

        }
    }
}
