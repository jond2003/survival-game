using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragmentationGrenade : MonoBehaviour, IGrenade
{
    private Throwable throwable;

    private float explosionRadius;
    private float damage;
    private float timeToExplode;

    private bool isCooking = false;
    private bool isThrown = false;
    private bool hasExploded = false;

    private AudioSource audioSource;

    [SerializeField] private GameObject explosion;

    private void Awake()
    {
        throwable = gameObject.GetComponent<Throwable>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (isCooking)
        {
            timeToExplode -= Time.deltaTime;

            if (timeToExplode <= 0 && hasExploded == false)
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

    public void Explode()
    {
        hasExploded = true;
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        bool playerTakenDamage = false;
        foreach (Collider collider in colliders)
        {
            Target enemy = collider.GetComponent<Target>();
            PlayerHealth player = collider.GetComponent<PlayerHealth>();

            if (enemy != null) enemy.TakeDamage(damage);
            if (player != null && !collider.isTrigger && !playerTakenDamage)
            {
                player.TakeDamage(damage);
                playerTakenDamage = true;
            }
        }

        Renderer[] renderersInRobot = GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderersInRobot)
        {
            renderer.enabled = false;
        }

        StartCoroutine(ExplosionEffects());
    }

    private IEnumerator ExplosionEffects()
    {
        audioSource.Play();
        GameObject generatedExplosion = Instantiate(explosion, transform.position, transform.rotation);
        

        yield return new WaitForSeconds(1f);
        Destroy(generatedExplosion);
        Destroy(this.gameObject);


    }
}
