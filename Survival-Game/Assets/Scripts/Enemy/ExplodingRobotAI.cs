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

    private EnemyHealth enemyHealth;

    private float timeSinceAttacked = 0f;

    private float timeSinceAttackedLimit = 4f;

    [SerializeField] private EnemyData easyEnemyData;
    [SerializeField] private EnemyData hardEnemyData;
    [SerializeField] private EnemyData impossibleEnemyData;
    private EnemyData enemyData;


    private AudioSource audioSource;
    [SerializeField] private AudioClip enemyRunAudioClip;
    [SerializeField] private AudioClip enemyExplosionAudioClip;


    void Awake()
    {
        player = GameObject.FindWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        meshAgent = GetComponent<NavMeshAgent>();
        meshAgent.stoppingDistance = 1.6f; //Stop enemy pushing player

        enemyData = (EnemyData)GameSettingsManager.GetDifficultyData(easyEnemyData, hardEnemyData, impossibleEnemyData);

        timeSinceAttackedLimit = enemyData.attackSpeed;

        enemyHealth = GetComponent<EnemyHealth>();
        audioSource = GetComponent<AudioSource>();
        audioSource.enabled = true;
    }

    void Update()
    {
        meshAgent.destination = player.transform.position; 
    }

    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.tag == "Player" && !enemyHealth.DamageEnemy(0))
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
        audioSource.loop = false;
        playerHealth.TakeDamage(enemyData.attackDamage);
        audioSource.volume = 0.5f; //increase volume for explosion
        audioSource.PlayOneShot(enemyExplosionAudioClip);
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
