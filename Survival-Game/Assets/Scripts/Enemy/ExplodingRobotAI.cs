using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ExplodingEnemyAI : MonoBehaviour
{

    [SerializeField] private GameObject player;
    [SerializeField] private PlayerHealth playerHealth;
    private NavMeshAgent meshAgent;

    [SerializeField] private float timeSinceAttackedLimit = 0.5f;

    private float timeSinceAttacked = 0f;


    void Awake()
    {
        player = GameObject.FindWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        meshAgent = GetComponent<NavMeshAgent>();
        meshAgent.stoppingDistance = 1.1f; //Stop enemy pushing player
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
        Destroy(this.gameObject);
    }

}
