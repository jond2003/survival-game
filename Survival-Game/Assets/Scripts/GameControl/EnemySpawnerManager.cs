using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerManager : MonoBehaviour
{
    [SerializeField] private EnemySpawnerData easySpawnerData;
    [SerializeField] private EnemySpawnerData hardSpawnerData;
    [SerializeField] private EnemySpawnerData impossibleSpawnerData;
    private EnemySpawnerData spawnerData;

    private List<GameObject> enemies = new List<GameObject>();
    public static EnemySpawnerManager Instance { get; private set; }

    private void Awake()
    {
        // Singleton
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;

        spawnerData = (EnemySpawnerData)GameSettingsManager.GetDifficultyData(easySpawnerData, hardSpawnerData, impossibleSpawnerData);
    }

    private void FixedUpdate()
    {
        // Delete dead enemies
        List<int> removeIndices = new List<int>();
        int i = 0;
        foreach (GameObject enemy in enemies)
        {
            if (enemy == null)
            {
                removeIndices.Add(i);
            }
            i++;
        }

        foreach (int index in removeIndices)
        {
            enemies.RemoveAt(index);
        }
    }

    public bool CanSpawn()
    {
        return enemies.Count < spawnerData.maxEnemies;
    }

    // Add enemy when spawned
    public void AddEnemy(GameObject enemy)
    {
        if (enemies.Count < spawnerData.maxEnemies)
        {
            enemies.Add(enemy);
        }
    }
}
