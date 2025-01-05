using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimalAI : MonoBehaviour
{

    private NavMeshAgent agent;
    private float timeToChangeDirection = 15f; //change direction when this time is done
    private float timeSinceChangedDirection = 0f;
    private Transform destination;

    private float startingSpeed = 2f;

    [SerializeField] Animator animator;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = startingSpeed; //starting speed

    }

    void Update()
    {
    
        if (Time.time  > timeSinceChangedDirection)
        {
            timeSinceChangedDirection = Time.time + timeToChangeDirection;
            
            Vector3 newDestination = GetRandomDestination(transform.position);
            agent.SetDestination(newDestination);
        }

        if (agent.velocity.sqrMagnitude > 0f) //means it's moving
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }

    public  Vector3 GetRandomDestination(Vector3 currentPosition)
    {
        Vector3 newDestination = Random.insideUnitSphere * 40;
        newDestination += currentPosition;
        NavMeshHit navMeshHit;
        NavMesh.SamplePosition(newDestination, out navMeshHit, 40, -1);
        return navMeshHit.position;
    }

}
