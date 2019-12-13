using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    private Vector2 moveTo;

    public GameObject player;

    void Start()
    {
        if (GameObject.FindGameObjectWithTag("Player") == null) // ENEMIES NEED TO COMMUNICATE SO THAT WHEN ONE DESTROYS A PLAY PLAYER THEY ALL KNOW
            return;

        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Player") == null) // ENEMIES NEED TO COMMUNICATE SO THAT WHEN ONE DESTROYS A PLAY PLAYER THEY ALL KNOW
            return;

        else
        {

            moveTo = new Vector2(player.transform.position.x, player.transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, moveTo, moveSpeed * Time.deltaTime);

        }
    }
}
