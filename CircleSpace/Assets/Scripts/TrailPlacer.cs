using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailPlacer : MonoBehaviour
{
    [SerializeField] private GameObject trailObj;
    [SerializeField] private float delay = 0.2f;

    private GameObject trailParent;
    private bool placeTrails = true;
    private const string TRAILSPARENTNAME = "TrailsParent";

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PlaceTrailsLoop());
    }

    private IEnumerator PlaceTrailsLoop ()
    {
        if (placeTrails)
        {
            yield return new WaitForSeconds(delay);

            trailParent = GameObject.Find(TRAILSPARENTNAME);
            GameObject trail = Instantiate(trailObj, transform.position, Quaternion.identity, trailParent.transform);

            placeTrails = false;
            yield return new WaitForSeconds(delay);
            placeTrails = true;
            yield return null;
            StartCoroutine(PlaceTrailsLoop());
        }
    }
}
