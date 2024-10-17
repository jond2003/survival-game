using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{

    [SerializeField] private GameObject player;
    private NavMeshAgent meshAgent;

    void Awake()
    {
        player = GameObject.FindWithTag("Player");
        meshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        meshAgent.destination = player.transform.position; 
    }
}
