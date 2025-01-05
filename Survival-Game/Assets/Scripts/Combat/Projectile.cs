using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Collider col;
    private Rigidbody rb;

    private bool propel = false;
    private LayerMask layersToHit;
    private GunData gunData;

    private void Awake()
    {
        col = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (propel)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, -transform.forward, out hit, 0.1f, layersToHit))
            {
                Collider[] colliders = Physics.OverlapSphere(transform.position, gunData.projectileExplosionRadius);

                bool playerTakenDamage = false;
                foreach (Collider collider in colliders)
                {
                    Target enemy = collider.GetComponent<Target>();
                    PlayerHealth player = collider.GetComponent<PlayerHealth>();

                    if (enemy != null) enemy.TakeDamage(gunData.damage);
                    if (player != null && !collider.isTrigger && !playerTakenDamage)
                    {
                        player.TakeDamage(gunData.damage);
                        playerTakenDamage = true;
                    }
                }
                Debug.Log("Exploded");
                Destroy(gameObject);
            }
        }
    }

    public void Shoot(GunData gunData, LayerMask layersToHit)
    {
        this.layersToHit = layersToHit;
        this.gunData = gunData;
        transform.SetParent(null);
        rb.AddForce(-transform.forward * gunData.projectileSpeed, ForceMode.Impulse);
        propel = true;
    }
}
