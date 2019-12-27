using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleSpawn : MonoBehaviour
{
    [SerializeField] private GameObject GameBoardCirclePrefab;
    [SerializeField] private GameObject plusPointsPrefab10;
    [SerializeField] private GameObject plusPointsPrefab20;
    private GameObject backgroundParent;

    [SerializeField] private Transform[] fourCirclesPositions;

    // Start is called before the first frame update
    void Start()
    {
        backgroundParent = GameObject.Find("Background");
    }

    public void SpawnCircle ()
    {
        GameObject newCircle = Instantiate(GameBoardCirclePrefab, transform.position, transform.rotation); // Make new circle where this enemy died
        newCircle.transform.SetParent(backgroundParent.transform);

        GameObject plus10 = Instantiate(plusPointsPrefab10, transform.position, transform.rotation);
        plus10.transform.SetParent(transform);
    }

    public void Spawn4Circles ()
    {
        for (int i = 0; i < 4; i++)
        {
            GameObject newCircle = Instantiate(GameBoardCirclePrefab, fourCirclesPositions[i]); // Make new circle where this enemy died
            newCircle.transform.SetParent(backgroundParent.transform);
        }

        GameObject plus20 = Instantiate(plusPointsPrefab20, transform.position, transform.rotation);
        plus20.transform.SetParent(transform);
    }
}
