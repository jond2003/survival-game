using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ExplodingEnemyAI : MonoBehaviour
{

    [SerializeField] private GameObject player;
    [SerializeField] private PlayerHealth playerHealth;
    private NavMeshAgent meshAgent;

    [SerializeField] private GameObject explosion;


    private float timeSinceAttacked = 0f;

    [SerializeField] private float timeSinceAttackedLimit = 4f;


    void Awake()
    {
        player = GameObject.FindWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        meshAgent = GetComponent<NavMeshAgent>();
        meshAgent.stoppingDistance = 1.6f; //Stop enemy pushing player
    }

    void Update()
    {
        meshAgent.destination = player.transform.position; 
    }

    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            if (Time.time > timeSinceAttacked)
            {
                timeSinceAttacked = Time.time + timeSinceAttackedLimit;
                Debug.Log("Exploding at player");
                AttackPlayer();
            }

            

        }
    }

    private void AttackPlayer()
    {
        playerHealth.TakeDamage(50);
        StartCoroutine(TriggerExplosion());

 
    }

    private IEnumerator TriggerExplosion()
    {
        GameObject generatedExplosion = Instantiate(explosion, transform.position, transform.rotation);

        Renderer[] renderersInRobot = GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderersInRobot)
        {
            renderer.enabled = false; 
        }

        yield return new WaitForSeconds(1f);
        Destroy(generatedExplosion);
        Destroy(this.gameObject);


    }

}
