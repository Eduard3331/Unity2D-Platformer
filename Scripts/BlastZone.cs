using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BlastZone : MonoBehaviour
{
    public bool killOnFall;
    Rigidbody2D rb;
    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.transform.position.y <= -15)
        {
            if (killOnFall) { Destroy(gameObject); }
            else
            {
                rb.transform.position = new Vector3(0f, 0f, rb.transform.position.z);
                rb.velocity = new Vector2(rb.velocity.x, 0);
            }
        }
    }
}
