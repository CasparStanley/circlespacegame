using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private EnemySpawnerSpawner spawnerScript;
    private ScoreController scoreContScript;
    private CircleSpawn circleSpawnScript;
    private SpriteRenderer sr;

    [SerializeField] private GameObject enemyPrefab;

    private int enemySpawnWaitTime;

    [SerializeField] private int spawnerHealth = 4;
    [SerializeField] Sprite[] spawnerSprites;
    private int currentSprite;

    [SerializeField] AudioClip[] damageSfxs;
    private AudioSource audSource;

    void Start()
    {
        spawnerScript = GameObject.Find("_Game Controller").GetComponent<EnemySpawnerSpawner>();
        scoreContScript = GameObject.Find("_Game Controller").GetComponent<ScoreController>();
        circleSpawnScript = GetComponent<CircleSpawn>();
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
            audSource.clip = damageSfxs[currentSprite]; // reuse of currentSprite int number 'cause it counts the same as is needed for SFX
            audSource.Play();

            // Visual
            currentSprite++;
            sr.sprite = spawnerSprites[currentSprite];

            spawnerHealth = 0;

            circleSpawnScript.Spawn4Circles();

            Destroy(gameObject, 0.5f);
        }

        else // If it's still got health left
        {
            // Audio
            audSource.clip = damageSfxs[currentSprite]; // reuse of currentSprite int number 'cause it counts the same as is needed for SFX
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
