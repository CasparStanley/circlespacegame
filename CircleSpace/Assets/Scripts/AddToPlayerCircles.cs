using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddToPlayerCircles : MonoBehaviour
{
    PlayerMovement playerMoveScript;
    private ScoreController scoreContScript;

    [SerializeField] private int pointsForDestroy = 1;

    private void Start()
    {
        playerMoveScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        scoreContScript = GameObject.FindGameObjectWithTag("GameCont").GetComponent<ScoreController>();

        playerMoveScript.circles.Add(gameObject);
        scoreContScript.ScoreUpdate(pointsForDestroy);
    }
}
