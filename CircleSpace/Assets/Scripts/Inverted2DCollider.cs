using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inverted2DCollider : MonoBehaviour
{
    EdgeCollider2D edgeCollider;

    public int NumEdges;
    public float Radius;

    public bool circleColliderOn;

    void Start()
    {
        edgeCollider = GetComponent<EdgeCollider2D>();
        Vector2[] points = new Vector2[NumEdges];

        for (int i = 0; i < NumEdges; i++)
        {
            float angle = 2 * Mathf.PI * i / NumEdges;
            float x = Radius * Mathf.Cos(angle);
            float y = Radius * Mathf.Sin(angle);

            points[i] = new Vector2(x, y);
        }
        edgeCollider.points = points;
    }

    public void ColliderOn ()
    {
        if (!circleColliderOn)
        {
            Debug.Log("<color=green>Collider was turned on!</color>");
            edgeCollider.enabled = true;
        }
    }

    public void ColliderOff ()
    {
        Debug.Log("<color=red>Collider was turned off!</color>");
        edgeCollider.enabled = false;
    }
}
