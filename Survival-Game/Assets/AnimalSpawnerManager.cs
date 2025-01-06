using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalSpawnerManager : MonoBehaviour
{
    private List<GameObject> animals = new List<GameObject>();
    public static AnimalSpawnerManager Instance { get; private set; }
    private int maxAnimals = 16;

    private void Awake()
    {
        // Singleton
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
    }

    private void FixedUpdate()
    {
        // Delete dead animals
        List<int> removeIndices = new List<int>();
        int i = 0;
        foreach (GameObject enemy in animals)
        {
            if (enemy == null)
            {
                removeIndices.Add(i);
            }
            i++;
        }

        foreach (int index in removeIndices)
        {
            animals.RemoveAt(index);
        }
    }

    public bool CanSpawn()
    {
        return animals.Count < maxAnimals;
    }

    // Add animal when spawned
    public void AddAnimal(GameObject enemy)
    {
        if (animals.Count < maxAnimals)
        {
            animals.Add(enemy);
        }
    }
}
