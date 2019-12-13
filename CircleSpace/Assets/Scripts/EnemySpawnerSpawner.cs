using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerSpawner : MonoBehaviour
{
    [SerializeField] GameObject enemySpawnerPrefab;

    [SerializeField] private int maxAmountSpawners;
    public int currentAmountSpawners;
   
    public List<Vector2> spawnerLocations = new List<Vector2>();

    void Start()
    {
        StartCoroutine(SpawnSpawnersStart());
    }

    IEnumerator SpawnSpawnersStart ()
    {
        for (int i = 0; i < maxAmountSpawners; i++)
        {
            int spawnPoint = Random.Range(0, spawnerLocations.Count); // Choose a random spawn point from the List

            // Actually spawn it!
            Vector2 spawnLoc = spawnerLocations[spawnPoint];
            spawnerLocations.Remove(spawnLoc); // Remove the location to avoid duplicate spawners
            GameObject newSpawner = Instantiate(enemySpawnerPrefab, spawnLoc, transform.rotation);
            currentAmountSpawners++;

            yield return new WaitForSeconds(Random.Range(0.5f, 2.5f));
        }
    }

    public void SpawnSpawner ()
    {
        int spawnPoint = Random.Range(0, spawnerLocations.Count); // Choose a random spawn point from the List

        // Actually spawn it!
        Vector2 spawnLoc = spawnerLocations[spawnPoint];
        spawnerLocations.Remove(spawnLoc); // Remove the location to avoid duplicate spawners
        GameObject newSpawner = Instantiate(enemySpawnerPrefab, spawnLoc, transform.rotation);
        currentAmountSpawners++;
    }
}
