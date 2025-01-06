using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject normalEnemyPrefab;
    [SerializeField] GameObject ExplodingEnemyPrefab;

    [SerializeField] float spawnRate = 10.0f;
    [SerializeField] float normalEnemyChance = 0.7f;

    void Start()
    {
        StartCoroutine(spawnEnemies());
    }

    private IEnumerator spawnEnemies()
    {
        while (true)
        {
            if (EnemySpawnerManager.Instance.CanSpawn())
            {
                GameObject enemy;

                float randomNumber = Random.Range(0f, 1f); //more likely for normal enemy
                if (randomNumber < normalEnemyChance)
                {
                    enemy = Instantiate(normalEnemyPrefab, transform.position, Quaternion.identity);
                }
                else
                {
                    enemy = Instantiate(ExplodingEnemyPrefab, transform.position, Quaternion.identity);
                }

                EnemySpawnerManager.Instance.AddEnemy(enemy);
            }

            yield return new WaitForSeconds(spawnRate);
        }
    }
}
