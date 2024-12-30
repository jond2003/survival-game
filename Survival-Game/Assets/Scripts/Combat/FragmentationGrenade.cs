using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragmentationGrenade : MonoBehaviour
{
    private Throwable throwable;

    private float explosionRadius;
    private float damage;
    private float timeToExplode;

    private bool isCooking = false;
    private bool isThrown = false;

    private void Awake()
    {
        throwable = gameObject.GetComponent<Throwable>();
    }

    private void Update()
    {
        if (isCooking)
        {
            timeToExplode -= Time.deltaTime;

            if (timeToExplode <= 0)
            {
                Explode();
            }
        }
    }

    public void Throw(Vector3 throwDirection, float explosionRadius, float damage, float cookTime)
    {
        if (!isThrown)
        {
            this.explosionRadius = explosionRadius;
            this.damage = damage;
            this.timeToExplode = isCooking ? this.timeToExplode : cookTime;

            Cook();

            throwable.Throw(throwDirection);

            isThrown = true;
        }
    }

    public void Cook(float explosionRadius, float damage, float cookTime)
    {
        this.explosionRadius = explosionRadius;
        this.damage = damage;
        this.timeToExplode = cookTime;

        Cook();
    }

    private void Cook()
    {
        isCooking = true;
    }

    private void Explode()
    {
        // Explode
        Debug.Log("Exploded!");
        Destroy(gameObject);
    }
}
