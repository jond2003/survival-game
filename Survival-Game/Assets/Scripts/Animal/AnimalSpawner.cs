using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalSpawner : MonoBehaviour
{
    [SerializeField] GameObject animalPrefab;

    [SerializeField] float spawnRate = 10.0f;

    void Start()
    {
        StartCoroutine(spawnAnimals());
    }
    private IEnumerator spawnAnimals()
    {
        while (true)
        {
            if (AnimalSpawnerManager.Instance.CanSpawn())
            {
                GameObject animal = Instantiate(animalPrefab, transform.position, Quaternion.identity);
                AnimalSpawnerManager.Instance.AddAnimal(animal);
            }

            yield return new WaitForSeconds(spawnRate);
        }
    }
}
