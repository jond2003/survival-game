using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MerchantAI : MonoBehaviour
{
    private NavMeshAgent agent;
    private float timeToChangeDirection = 30f; //change direction when this time is done
    private float timeSinceChangedDirection = 0f;
    private Transform destination;

    private Animator animator;

    private float startingSpeed = 2f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = startingSpeed; //starting speed
        animator = transform.GetChild(0).GetComponent<Animator>();
    }

    void Update()
    {
        if (transform.position == agent.destination)
        {
            animator.SetBool("isWalking", false);
        }

        if (Time.time > timeSinceChangedDirection)
        {
            timeSinceChangedDirection = Time.time + timeToChangeDirection;

            Vector3 newDestination = GetRandomDestination(transform.position);
            animator.SetBool("isWalking", true);
            agent.SetDestination(newDestination);
        }
    }

    public Vector3 GetRandomDestination(Vector3 currentPosition)
    {
        Vector3 newDestination = Random.insideUnitSphere * 50;
        newDestination += currentPosition;
        NavMeshHit navMeshHit;
        NavMesh.SamplePosition(newDestination, out navMeshHit, 50, -1);
        return navMeshHit.position;
    }
}
