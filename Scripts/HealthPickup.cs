using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healthRestore = 25;
    public Vector3 spinRotationSpeed = new Vector3(0, 180, 0);
    public AudioClip healthSound;
    // Start is called before the first frame update

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();

        if(damageable && damageable.MaxHealth!=damageable.Health)
        {
            damageable.Heal(healthRestore);
            AudioSource.PlayClipAtPoint(healthSound, gameObject.transform.position);
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        transform.eulerAngles += spinRotationSpeed * Time.deltaTime;
    }
}
