using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{

    [SerializeField] private Transform player;
    private NavMeshAgent meshAgent;

    // Start is called before the first frame update
    void Awake()
    {
        meshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        meshAgent.destination = player.position; 
    }
}
