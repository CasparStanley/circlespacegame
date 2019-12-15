using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddToPlayerEnemies : MonoBehaviour
{
    [SerializeField] private RotateToClosestEnemy rotateToClosestEnemyScript;

    private void Start()
    {
        rotateToClosestEnemyScript = GameObject.Find("EnemyDetector").GetComponent<RotateToClosestEnemy>();
        rotateToClosestEnemyScript.enemies.Add(gameObject);
    }
}
