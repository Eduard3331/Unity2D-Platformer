using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject projectilePrefab;
    public void FireProjectile()
    {
        GameObject projectile = Instantiate (projectilePrefab, spawnPoint.position, projectilePrefab.transform.rotation);
        Vector3 originalScale = projectile.transform.localScale;
        projectile.transform.localScale = new Vector3(originalScale.x * transform.localScale.x > 0 ? 1 : -1,
            originalScale.y, originalScale.z);
    
    }
}
