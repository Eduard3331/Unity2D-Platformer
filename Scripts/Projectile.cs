using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector2 moveSpeed = new Vector2(6f,0);
    public int damage = 10;
    public Vector2 knockback = new Vector2(0, 0);
    public float despawnTimer = 10f;
    private float elapsedTime = 0f;
    Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = new Vector2(moveSpeed.x * transform.localScale.x, moveSpeed.y);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();
        if (damageable != null)
        {
            Vector2 deliveredKnockback = transform.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);
            bool gotHit = damageable.Hit(damage, deliveredKnockback);
            if (gotHit)
            {
                Debug.Log(collision.name + " hit for " + damage);
                Destroy(gameObject);
            } }
    }
    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        if(elapsedTime > despawnTimer)
            Destroy(gameObject);
    }
}
