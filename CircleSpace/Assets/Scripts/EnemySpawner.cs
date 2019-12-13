using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    EnemySpawnerSpawner spawnerScript;
    ScoreController scoreContScript;
    SpriteRenderer sr;

    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject plus20PointsPrefab;

    private int enemySpawnWaitTime;

    [SerializeField] private int spawnerHealth = 4;
    [SerializeField] private int pointsForDestroy = 2;
    [SerializeField] Sprite[] spawnerSprites;
    private int currentSprite;

    [SerializeField] AudioClip[] damageSfxs;
    private AudioSource audSource;

    void Start()
    {
        spawnerScript = GameObject.Find("_Game Controller").GetComponent<EnemySpawnerSpawner>();
        scoreContScript = GameObject.Find("_Game Controller").GetComponent<ScoreController>();
        audSource = GetComponent<AudioSource>();

        enemySpawnWaitTime = Random.Range(8, 14);

        StartCoroutine(SpawnEnemy());
        sr = GetComponentInChildren<SpriteRenderer>();

        currentSprite = 0;
    }

    void ReduceSpawnerHealth()
    {
        if (spawnerHealth <= 1) // If it dies
        {
            // Remove this Enemy Spawner from the list of spawners and add its location to the list of possible spawner locations
            spawnerScript.currentAmountSpawners--;
            spawnerScript.SpawnSpawner();
            spawnerScript.spawnerLocations.Add(transform.position);

            // Audio
            audSource.clip = damageSfxs[currentSprite]; // reuse of currentSprite 'cause it counts the same
            audSource.Play();

            // Visual
            currentSprite++;
            sr.sprite = spawnerSprites[currentSprite];

            spawnerHealth = 0;
            scoreContScript.ScoreUpdate(pointsForDestroy);

            GameObject plus20 = Instantiate(plus20PointsPrefab, transform.position, transform.rotation);
            plus20.transform.SetParent(transform);

            Destroy(gameObject, 0.5f);
        }

        else // If it's still got health left
        {
            // Audio
            audSource.clip = damageSfxs[currentSprite]; // reuse of currentSprite 'cause it counts the same
            audSource.Play();

            // Visual
            currentSprite++;
            spawnerHealth--;
            sr.sprite = spawnerSprites[currentSprite];
        }
    }

    IEnumerator SpawnEnemy ()
    {
        Instantiate(enemyPrefab, transform.position, transform.rotation);
        yield return new WaitForSeconds(enemySpawnWaitTime);
        enemySpawnWaitTime = Random.Range(8, 14);
        StartCoroutine(SpawnEnemy());
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            ReduceSpawnerHealth();
        }
    }
}
