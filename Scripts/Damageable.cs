using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    public UnityEvent<int, Vector2> damageableHit;
    Animator animator;
    [SerializeField]
    private int _maxHealth = 100;

    public int MaxHealth
    { get
        {
            return _maxHealth;
        }
        set
        {
            _maxHealth = value;
        }
    }
    [SerializeField]
    private int _health = 100;

    public int Health
    {
        get
        {
            return _health;
        }
        set
        {
            _health = value;
            if (_health <= 0)
            {
                IsAlive = false;
            }
        }
    }
    private bool _isAlive = true;
    [SerializeField]
    private bool isInvul = false;


    private float timeSinceHit = 0f;
    public float invulTimer = 0.1f;

    public bool IsAlive
    {
        get
        {
            return _isAlive;
        }
        set
        {
            _isAlive = value;
            animator.SetBool(AnimationStrings.isAlive, value);
            Debug.Log("IsAlive = " + value);
        }
    }
    public bool LockVelocity
    {
        get
        {
            return animator.GetBool(AnimationStrings.lockVelocity);
        }
        set
        {
            animator.SetBool(AnimationStrings.lockVelocity, value);
        }
    }


    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Update()
    {
        if (isInvul)
        {
            if(timeSinceHit > invulTimer)
            {
                isInvul = false;
                timeSinceHit = 0;
            }
            timeSinceHit += Time.deltaTime;
          
        }
    }
    public bool Hit(int damage, Vector2 knockback)
    {
        if (IsAlive && !isInvul)
        {
            Health -= damage;
            isInvul = true;

            LockVelocity = true;
            animator.SetTrigger(AnimationStrings.hitTrigger);
            damageableHit.Invoke(damage, knockback);
            CharacterEvents.characterDamaged.Invoke(gameObject, damage);

            return true;
           
        }
       return false;
    }
    public void Heal(int healthRestore)
    {
        if(IsAlive)
        {
            int maxHeal = Mathf.Min(MaxHealth - Health, healthRestore);
            Health += maxHeal;

            CharacterEvents.characterHealed(gameObject, maxHeal);
        }
    }
}
