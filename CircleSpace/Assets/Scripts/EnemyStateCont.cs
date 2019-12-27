using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateCont : MonoBehaviour
{
    private SpriteRenderer enemySprite;
    MenuController menuContScript;

    [SerializeField] private GameObject player;
    [SerializeField] private PlayerMovement playerMovScript;
    [SerializeField] private RotateToClosestEnemy rotateToEnemyScript;
    private CircleSpawn circleSpawnScript;

    [SerializeField] private int enemyState = 1;
    [SerializeField] private float blinkTime;
    [SerializeField] private Color enemyColorGreen;
    [SerializeField] private Color enemyColorYellow;
    [SerializeField] private Color enemyColorRed;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerMovScript = player.GetComponent<PlayerMovement>();
        rotateToEnemyScript = player.GetComponentInChildren<RotateToClosestEnemy>();
        menuContScript = GameObject.Find("_Game Controller").GetComponent<MenuController>();
        circleSpawnScript = GetComponent<CircleSpawn>();

        enemySprite = GetComponentInChildren<SpriteRenderer>();
        enemySprite.color = enemyColorGreen;

        StartCoroutine(ChangeState());
    }

    IEnumerator ChangeState ()
    {
        enemySprite.color = enemyColorYellow;   // Turns Yellow - State 2
        enemyState = 2;
        yield return new WaitForSeconds(blinkTime);
        enemySprite.color = enemyColorRed;      // Turns Red - State 3
        enemyState = 3;
        yield return new WaitForSeconds(blinkTime);
        enemySprite.color = enemyColorGreen;    // Turns Green - State 1
        enemyState = 1;
        yield return new WaitForSeconds(blinkTime);
        StartCoroutine(ChangeState());
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        // THIS ENEMY DIES
        if (col.CompareTag("Player"))
        {
            EnemyHit();
        }
    }

    private void EnemyHit()
    {
        switch(enemyState)
        {
            case 3:
                enemySprite.color = enemyColorRed;

                Debug.Log("<color=red> Player Should Die </color>");
                playerMovScript.PlayerDeath();
                menuContScript.PlayerDeath();
                break;
            case 2:
                enemySprite.color = enemyColorYellow;
                break;
            case 1:
                enemySprite.color = enemyColorGreen;

                Debug.Log("<color=green> Enemy Should Die </color>");

                circleSpawnScript.SpawnCircle();
                rotateToEnemyScript.enemies.Remove(gameObject);

                Destroy(gameObject, 0.01f);
                break;
            default:
                enemySprite.color = enemyColorGreen;
                Debug.Log("<color=purple> Something wenth wrong </color>");
                break;

        }
    }
}
