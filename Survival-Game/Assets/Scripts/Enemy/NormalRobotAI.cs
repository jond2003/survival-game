using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NormalRobotAI : MonoBehaviour
{

    [SerializeField] private GameObject player;
    [SerializeField] private PlayerHealth playerHealth;
    private NavMeshAgent meshAgent;

    [SerializeField] private float timeSinceAttackedLimit = 2f;

    private float timeSinceAttacked = 0f;

    [SerializeField] Animator animator;


    void Awake()
    {
        player = GameObject.FindWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        meshAgent = GetComponent<NavMeshAgent>();
        meshAgent.stoppingDistance = 2f; //Stop enemy pushing player
        animator = GetComponent<Animator>();
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
                AttackPlayer();
            }
        }
 
    }

    private void AttackPlayer()
    {
        StartCoroutine(AttackAnimation());  
    }

    private IEnumerator AttackAnimation()
    {
        animator.SetBool("isAttacking", true);
        yield return new WaitForSeconds(1f);
        playerHealth.TakeDamage(20);
        animator.SetBool("isAttacking", false);
    }

}
