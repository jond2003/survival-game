using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;

    [SerializeField] float spawnRate = 10.0f;

    void Start()
    {
        StartCoroutine(spawnEnemies());
    }
    private IEnumerator spawnEnemies()
    {
        while (true)
        {
            Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(spawnRate); 
        }
    }
}
