using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject normalEnemyPrefab;
    [SerializeField] GameObject ExplodingEnemyPrefab;

    [SerializeField] float spawnRate = 10.0f;

    void Start()
    {
        StartCoroutine(spawnEnemies());
    }
    private IEnumerator spawnEnemies()
    {
        while (true)
        {
            float randomNumber = Random.Range(0, 10.0f); //more likely for normal enemy
            if (randomNumber > 3.0f)
            {
                Instantiate(normalEnemyPrefab, transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(ExplodingEnemyPrefab, transform.position, Quaternion.identity);
            }
           
            yield return new WaitForSeconds(spawnRate); 
        }
    }
}
